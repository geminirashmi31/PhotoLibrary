using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoLibrary
{
    public class PhotoLibraryObj
    {
        private const string TEXT_FILE_NAME = "PhotoLibrary";
        public string Name { get; set; }
        public string CoverPhotoPath { get; set; }
        [JsonProperty]
        private Dictionary<string, Photo> photoLibrary = new Dictionary<string, Photo>();

        public PhotoLibraryObj(){}
        public PhotoLibraryObj(string name, string coverPhotoPath)
        {
            Name = name;
            CoverPhotoPath = coverPhotoPath;
        }

        public async Task AddPhotoPath(string photoPath)
        {
            if(!File.Exists(photoPath))
            {
                return;
            }

            Photo photoToAdd = new Photo
            {
                Name = System.IO.Path.GetFileName(photoPath),
                Path = photoPath
            };

            if (photoLibrary.ContainsKey(photoPath))
            {
                return;
            }

            photoLibrary.Add(photoPath, photoToAdd);
            await Save();
        } 

        public async Task RemovePhotoPath(string photoPath)
        {
            photoLibrary.Remove(photoPath);
            await Save();
        }

        public async Task Save()
        {
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            await FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
        }

        public static async Task<PhotoLibraryObj> LoadPhotoLibrary(string libraryName)
        {
            string fileContact = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME + libraryName + ".txt");
            PhotoLibraryObj library = JsonConvert.DeserializeObject<PhotoLibraryObj>(fileContact);
            return library;
        }

        public List<Photo> GetPhotos()
        {
            return this.photoLibrary.Values.ToList();
        }

        public async Task SelectCoverPhoto(string photoPath)
        {
            this.CoverPhotoPath = photoPath;
            await Save();
        }

    }
}

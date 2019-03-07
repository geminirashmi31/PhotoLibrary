using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public PhotoLibraryObj(string name)
        {
            Name = name;
        }

        public void AddPhotoPath(string photoPath)
        {
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
            Save();
        } 

        public void RemovePhotoPath(string photoPath)
        {
            photoLibrary.Remove(photoPath);
            Save();
        }

        public void Save()
        {
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
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

        public void SelectCoverPhoto(string photoPath)
        {
            this.CoverPhotoPath = photoPath;
            Save();
        }

    }
}

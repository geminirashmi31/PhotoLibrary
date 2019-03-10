using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace PhotoLibrary
{
    public class PhotoLibraryObj
    {
        private const string TEXT_FILE_NAME = "PhotoLibrary";
        public string Name { get; set; }
        public string CoverPhotoPath { get; set; }
        [JsonProperty]
        private Dictionary<string, Photo> photoLibrary = new Dictionary<string, Photo>();

        public static async Task<PhotoLibraryObj> CreatePhotoLibraryAsync(string name, string coverPicPath)
        {
            var library = new PhotoLibraryObj();
            library.Name = name;
            library.CoverPhotoPath = coverPicPath;
            await library.SaveAsync();
            return library;
        }
      
        /// <summary>
        /// Add photo to photo library
        /// </summary>
        /// <param name="photoPath">string representing the Path where the photo saved on the computer</param>
        public async Task AddPhotoPathAsync(string photoPath)
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
            await SaveAsync();
        }
      
        /// <summary>
        /// Delete photo from photo library
        /// </summary>
        /// <param name="photoPath">string representing the Path where the photo saved on the computer</param>
        public async Task RemovePhotoPathAsync(string photoPath)
        {
            photoLibrary.Remove(photoPath);
            await SaveAsync();
        }

        /// <summary>
        /// saving photolibrary to a txt file on disk
        /// </summary>
        public async Task SaveAsync()
        {
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            await FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
        }

        public static async Task<PhotoLibraryObj> LoadPhotoLibraryAsync(string libraryName)
        {
            string fileContact = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME + libraryName + ".txt");
            PhotoLibraryObj library = JsonConvert.DeserializeObject<PhotoLibraryObj>(fileContact);
            return library;
        }

        public List<Photo> GetPhotos()
        {
            return this.photoLibrary.Values.ToList();
        }

        public async Task SelectCoverPhotoAsync(string photoPath)
        {
            this.CoverPhotoPath = photoPath;
            await SaveAsync();
        }

    }
}

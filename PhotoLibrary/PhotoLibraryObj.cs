﻿using Newtonsoft.Json;
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

        public static async Task<PhotoLibraryObj> CreatePhotoLibrary(string name)
        {
            var library = new PhotoLibraryObj();
            library.Name = name;
            await library.Save();
            return library;
        }
      
        /// <summary>
        /// Add photo to photo library
        /// </summary>
        /// <param name="photoPath">string representing the Path where the photo saved on the computer</param>
        public Task AddPhotoPath(string photoPath)
        {
            Photo photoToAdd = new Photo
            {
                Name = System.IO.Path.GetFileName(photoPath),
                Path = photoPath
            };

            if (photoLibrary.ContainsKey(photoPath))
            {
                return Task.CompletedTask;
            }

            photoLibrary.Add(photoPath, photoToAdd);
            return Save();
        }
      
        /// <summary>
        /// Delete photo from photo library
        /// </summary>
        /// <param name="photoPath">string representing the Path where the photo saved on the computer</param>
        public Task RemovePhotoPath(string photoPath)
        {
            photoLibrary.Remove(photoPath);
            return Save();
        }

        /// <summary>
        /// saving photolibrary to a txt file on disk
        /// </summary>
        public Task Save()
        {
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            return FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
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

        public Task SelectCoverPhoto(string photoPath)
        {
            this.CoverPhotoPath = photoPath;
            return Save();
        }

    }
}

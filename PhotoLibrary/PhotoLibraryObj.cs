﻿using Newtonsoft.Json;
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
        public string Name { get; private set; }
        public string CoverPhotoPath { get; private set; }
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
            Save(this);
        } 

        public void RemovePhotoPath(string photoPath)
        {
            Photo photoToRemove = new Photo
            {
                Name = System.IO.Path.GetFileName(photoPath),
                Path = photoPath
            };
            photoLibrary.Remove(photoPath);
            Save(this);
        }

        public void Save(PhotoLibraryObj photoLibrary)
        {
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
        }

        public static async Task<PhotoLibraryObj> LoadPhotoLibrary(string libraryName)
        {
            string fileContact = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME + libraryName + ".txt");

            PhotoLibraryObj library = JsonConvert.DeserializeObject<PhotoLibraryObj>(fileContact);

            //var photoLibrary = new List<PhotoLibrary>();
            //photoLibrary.Add(library);
            return library;
        }

        public List<Photo> GetPhotos()
        {
            return this.photoLibrary.Values.ToList();
        }

    }
}

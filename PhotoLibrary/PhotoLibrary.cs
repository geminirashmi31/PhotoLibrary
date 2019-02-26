using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoLibrary
{
    class PhotoLibrary
    {
        private const string TEXT_FILE_NAME = "PhotoLibrary";
        public string Name { get; set; }
        public string CoverPhotoPath { get; set; }

        private Dictionary<string, Photo> photoLibrary = new Dictionary<string, Photo>();



        public void AddPhotoPath(string photoPath)
        {
            Photo photoToAdd = new Photo
            {
                Name = System.IO.Path.GetFileName(photoPath),
                Path = photoPath
            };
            photoLibrary.Add(photoPath, photoToAdd);
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
        } 

        public void RemovePhotoPath(string photoPath)
        {
            Photo photoToRemove = new Photo
            {
                Name = System.IO.Path.GetFileName(photoPath),
                Path = photoPath
            };
            photoLibrary.Remove(photoPath);
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this);
            FileHelper.WriteTextFileAsync(TEXT_FILE_NAME + Name + ".txt", jsonPhotoLibrary);
        }

        


    }
}

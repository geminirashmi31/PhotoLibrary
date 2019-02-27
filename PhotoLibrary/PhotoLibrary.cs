using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhotoLibraryApp
{
    public class PhotoLibrary
    {
        private const string TEXT_FILE_NAME = "PhotoLibrary";
        public string Name { get; private set; }
        public string CoverPhotoPath { get; private set; }
        [JsonProperty]
        private Dictionary<string, Photo> photoLibrary = new Dictionary<string, Photo>();

        public PhotoLibrary(){}
        public PhotoLibrary(string name)
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

        
        public async static Task<List<PhotoLibrary>> LoadPhotoLibrary(string libraryName)
        {
            string fileContact = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME + libraryName + ".txt");

            PhotoLibrary library = JsonConvert.DeserializeObject<PhotoLibrary>(fileContact);

            var photoLibrary = new List<PhotoLibrary>();
            photoLibrary.Add(library);
            return photoLibrary;
        }

        public static List<Photo> LoadPhotoes(string libraryName)
        {
            var photoes = new List<Photo>();
            List<PhotoLibrary> libraryList = LoadPhotoLibrary(libraryName).Result;
            PhotoLibrary currentLibrary = libraryList.First();

            Dictionary<string, Photo> dictionary = currentLibrary.photoLibrary;
            foreach (var pic in dictionary)
            {
                var photo = new Photo
                {
                    Name = System.IO.Path.GetFileName(pic.Key),
                    Path = pic.Key
                };
                photoes.Add(photo);
            }
            return photoes;
        }

    }
}

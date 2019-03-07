using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoLibrary
{
    public class PhotoLibraryManager
    {
        private const string LIBRARY_MANAGER_FILE_NAME = "PhotoLibraryManager.txt";
        private Dictionary<string, PhotoLibraryObj> libraryCollection;

        public PhotoLibraryManager()
        {
            this.libraryCollection = new Dictionary<string, PhotoLibraryObj>();
            this.PhotoLibraryManagerFile = FileHelper.GetFilePath(LIBRARY_MANAGER_FILE_NAME).Result.Path;
        }

        public string PhotoLibraryManagerFile { get; private set; }

        public void Initialize()
        {
            this.libraryCollection = LoadPhotoLibraries().Result;
        }

        public async Task AddPhotoLibraryAsync(PhotoLibraryObj photoLibrary)
        {
            this.libraryCollection.Add(photoLibrary.Name, photoLibrary);
            await UpdateFileAsync();
        }

        public async Task RemovePhotoLibraryAsync(string libraryName)
        {
            this.libraryCollection.Remove(libraryName);
            await UpdateFileAsync();
        }

        public async Task ClearAsync()
        {
            this.libraryCollection.Clear();
            await UpdateFileAsync();
        }

        private async Task UpdateFileAsync()
        {
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this.libraryCollection.Keys.ToList());
            await FileHelper.WriteTextFileAsync(LIBRARY_MANAGER_FILE_NAME, jsonPhotoLibrary);
        }

        private async static Task<Dictionary<string, PhotoLibraryObj>> LoadPhotoLibraries()
        {
            var libraryNames = await LoadPhotoLibraryNames();
            var libraries = new Dictionary<string, PhotoLibraryObj>();

            foreach (var lib in libraryNames)
            {
                var library = PhotoLibraryObj.LoadPhotoLibrary(lib).Result;

                if (library != null)
                {
                    libraries.Add(lib, library);
                }
            }
            return libraries;
        }

        private async static Task<List<string>> LoadPhotoLibraryNames()
        {
            string managerFileContent = await FileHelper.ReadTextFileAsync(LIBRARY_MANAGER_FILE_NAME);

            List<string> libraries = JsonConvert.DeserializeObject<List<string>>(managerFileContent);

            return libraries != null ? libraries : new List<string>();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoLibrary
{
    public class PhotoLibraryManager
    {
        private const string LIBRARY_MANAGER_FILE_NAME = "PhotoLibraryManager.txt";
        private Dictionary<string, string> libraryCollection;

        private static PhotoLibraryManager libraryInstance;
       
        public static PhotoLibraryManager GetInstance()
        {
            if(libraryInstance == null)
            {
                libraryInstance = new PhotoLibraryManager();
            }

            return libraryInstance;
        }

        private PhotoLibraryManager()
        {
            this.libraryCollection = new Dictionary<string, string>();
            this.PhotoLibraryManagerFile = FileHelper.GetFilePathAsync(LIBRARY_MANAGER_FILE_NAME).Result.Path;
        }

        public string PhotoLibraryManagerFile { get; private set; }

        public async Task Initialize()
        {
            this.libraryCollection = await LoadPhotoLibraryMetadataAsync();
        }

        public async Task<List<LibraryMetadata>> LoadPhotoLibraries()
        {
            this.libraryCollection = await LoadPhotoLibraryMetadataAsync();

            var libraryMetadata = new List<LibraryMetadata>();

            foreach (KeyValuePair<string, string> kv in this.libraryCollection)
            {
                libraryMetadata.Add(new LibraryMetadata(kv.Key, kv.Value));
            }

            return libraryMetadata;
        }

        public async Task AddPhotoLibraryAsync(PhotoLibraryObj library)
        {
            this.libraryCollection.Add(library.Name, library.CoverPhotoPath);

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
            string jsonPhotoLibrary = JsonConvert.SerializeObject(this.libraryCollection);
            await FileHelper.WriteTextFileAsync(LIBRARY_MANAGER_FILE_NAME, jsonPhotoLibrary);
        }

        private async static Task<Dictionary<string, string>> LoadPhotoLibraryMetadataAsync()
        {
            string managerFileContent = await FileHelper.ReadTextFileAsync(LIBRARY_MANAGER_FILE_NAME);
            var libraries = JsonConvert.DeserializeObject<Dictionary<string, string>>(managerFileContent);
            return libraries != null ? libraries : new Dictionary<string, string>();
        }
    }
}

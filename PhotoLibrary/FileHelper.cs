using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PhotoLibrary
{
    //class static, all methods static
    public static class FileHelper
    {
        /// <summary>
        /// Write content to the file
        /// </summary>
        /// <param name="filename">Name of the file</param>
        /// <param name="content">Content to write to the file</param>
        public static async Task WriteTextFileAsync(string filename, string content)
        {
            var textFile = await GetFilePath(filename);
            await FileIO.WriteTextAsync(textFile, content);
        }

        public static async Task<string> ReadTextFileAsync(string filename)
        {
            var textFile = await GetFilePath(filename);
            return await FileIO.ReadTextAsync(textFile);
            /*
            var textStream = await textFile.OpenReadAsync();
            var textReader = new DataReader(textStream);
            var textLength = textStream.Size;
            await textReader.LoadAsync((uint)textLength);

            return textReader.ReadString((uint)textLength);
            */
        }

        public static async Task<StorageFile> GetFilePath(string fileName)
        {
            var localFolder = ApplicationData.Current.LocalFolder;

            if(null == await localFolder.TryGetItemAsync(fileName))
            {
                await localFolder.CreateFileAsync(fileName);
            }

            var textFile = await localFolder.GetFileAsync(fileName);

            return textFile;
        }
    }
}

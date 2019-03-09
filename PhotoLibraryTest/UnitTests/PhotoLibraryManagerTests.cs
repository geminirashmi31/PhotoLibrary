using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PhotoLibrary;

namespace PhotoLibraryTest.UnitTests
{
    [TestClass]
    public class PhotoLibraryManagerTests
    {
        private const string LIBRARY_MANAGER_FILE_NAME = "PhotoLibraryManager.txt";

        [TestMethod]
        public void CanCreateAndInitializePhotoLibraryManager()
        {
            var photoLibraryManager = PhotoLibraryManager.GetInstance();
            photoLibraryManager.Initialize();

            Assert.IsTrue(File.Exists(photoLibraryManager.PhotoLibraryManagerFile));
            var libraries = GetPhotoLibraryNames().Result;
            Assert.AreEqual(0, libraries.Count);
        }

        [TestMethod]
        public void CanAddNewLibraryToLibraryManager()
        {
            var photoLibraryManager = PhotoLibraryManager.GetInstance();
            photoLibraryManager.Initialize();

            var libraryName1 = "Test1";
            var library1 = PhotoLibraryObj.CreatePhotoLibrary(libraryName1).Result;

            var libraryName2 = "Test2";
            var library2 = PhotoLibraryObj.CreatePhotoLibrary(libraryName2).Result;

            try
            {
                photoLibraryManager.AddPhotoLibraryAsync(library1).Wait();
                photoLibraryManager.AddPhotoLibraryAsync(library2).Wait();

                // Assert.IsTrue(File.Exists(photoLibraryManager.PhotoLibraryManagerFile));
                var libraries = GetPhotoLibraryNames().Result;
                Assert.AreEqual(2, libraries.Count);
                Assert.IsTrue(libraries.Contains(libraryName1));
                Assert.IsTrue(libraries.Contains(libraryName2));
            }
            finally
            {
                photoLibraryManager.ClearAsync().Wait();
            }
        }


        [TestMethod]
        public void CanRemoveLibraryFromLibraryManager()
        {
            var photoLibraryManager = PhotoLibraryManager.GetInstance();
            photoLibraryManager.Initialize();

            var libraryName1 = "TestA";
            var library1 = PhotoLibraryObj.CreatePhotoLibrary(libraryName1).Result;

            var libraryName2 = "TestB";
            var library2 = PhotoLibraryObj.CreatePhotoLibrary(libraryName2).Result;

            try
            {
                photoLibraryManager.AddPhotoLibraryAsync(library1).Wait();
                photoLibraryManager.AddPhotoLibraryAsync(library2).Wait();

                var libraries = GetPhotoLibraryNames().Result;
                Assert.AreEqual(2, libraries.Count);
                Assert.IsTrue(libraries.Contains(libraryName1));
                Assert.IsTrue(libraries.Contains(libraryName2));

                // remove library1
                photoLibraryManager.RemovePhotoLibraryAsync(library1.Name).Wait();

                var upadtedLibraries = GetPhotoLibraryNames().Result;
                Assert.AreEqual(1, upadtedLibraries.Count);
                Assert.IsFalse(upadtedLibraries.Contains(libraryName1));
                Assert.IsTrue(upadtedLibraries.Contains(libraryName2));
            }
            finally
            {
                photoLibraryManager.ClearAsync().Wait();
            }
        }

        private async static Task<List<string>> GetPhotoLibraryNames()
        {
            string managerFileContent = await FileHelper.ReadTextFileAsync(LIBRARY_MANAGER_FILE_NAME);

            List<string> libraries = JsonConvert.DeserializeObject<List<string>>(managerFileContent);

            return libraries != null ? libraries : new List<string>();
        }
    }
}

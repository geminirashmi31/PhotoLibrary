using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoLibrary;

namespace PhotoLibraryTest.UnitTests
{
    [TestClass]
    public class PhotoLibraryObjTests
    {
        [TestMethod]
        public async Task AddRemovePhotoPathTest()
        {
            PhotoLibraryObj library = await PhotoLibraryObj.CreatePhotoLibrary("eden");

            await library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\eden.jpg");
            await library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\karen.jpg");

            Photo photo1 = library.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\eden.jpg");
            Photo photo2 = library.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\karen.jpg");
            Assert.IsNotNull(photo1);
            Assert.IsNotNull(photo2);
            Assert.AreEqual(photo1.Path, "C:\\Users\\lentochka\\Desktop\\eden.jpg");
            Assert.AreEqual(photo2.Path, "C:\\Users\\lentochka\\Desktop\\karen.jpg");

            await library.RemovePhotoPath("C:\\Users\\lentochka\\Desktop\\karen.jpg");
            photo2 = library.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\karen.jpg");
            Assert.IsNull(photo2);


        }

        [TestMethod]
        public async Task SaveTestAsync()
        {
            PhotoLibraryObj library = await PhotoLibraryObj.CreatePhotoLibrary(Guid.NewGuid().ToString());

            await library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\eden.jpg");
            await library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\karen.jpg");
            await library.SelectCoverPhoto("C:\\Users\\lentochka\\Desktop\\karen.jpg");
            var loadLibrary = await PhotoLibraryObj.LoadPhotoLibrary(library.Name);

            Photo photo1 = library.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\eden.jpg");
            Photo photo2 = library.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\karen.jpg");
            Photo photo3 = loadLibrary.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\eden.jpg");
            Photo photo4 = loadLibrary.GetPhotos().FirstOrDefault(p => p.Path == "C:\\Users\\lentochka\\Desktop\\karen.jpg");
            var numberOfPhotosLibrary = library.GetPhotos().Count();
            var numberOfPhotosLoadLibrary = loadLibrary.GetPhotos().Count();
            Assert.AreEqual(library.Name, loadLibrary.Name);
            Assert.AreEqual(library.CoverPhotoPath, loadLibrary.CoverPhotoPath);
            Assert.IsNotNull(photo1);
            Assert.IsNotNull(photo2);
            Assert.IsNotNull(photo3);
            Assert.IsNotNull(photo4);
            Assert.AreEqual(photo1.Path, photo3.Path);
            Assert.AreEqual(photo2.Path, photo4.Path);
            Assert.AreEqual(numberOfPhotosLibrary, numberOfPhotosLoadLibrary);
        }
    }
}

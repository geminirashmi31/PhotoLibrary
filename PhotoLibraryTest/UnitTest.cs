
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoLibraryApp;

namespace PhotoLibraryApp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddPhotoPathTest()
        {
            PhotoLibrary library = new PhotoLibrary("eden");

            library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\eden.jpg");
            


        }
    }
}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoLibrary;

namespace PhotoLibrary.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddPhotoPathTest()
        {
            PhotoLibraryObj library = new PhotoLibraryObj("eden");

            library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\eden.jpg");
            //library.LoadPhotoLibrary("eden");


        }
    }
}

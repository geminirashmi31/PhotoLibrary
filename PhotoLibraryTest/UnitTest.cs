
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
            var coverPicPath = "C:\temp\rainbowKitties.jpg";
            PhotoLibraryObj library = new PhotoLibraryObj("eden", coverPicPath);
            library.Save().Wait();
            // library.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\eden.jpg");
            //library.LoadPhotoLibrary("eden");


        }
    }
}

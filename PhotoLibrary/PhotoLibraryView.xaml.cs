using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PhotoLibraryView : Page
    {
        public PhotoLibraryView()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // var img = new Image();


            //StorageFile f = StorageFile.GetFileFromPathAsync(@"C:\Users\lentochka\Pictures\Saved Pictures\eden.jpg").GetResults();

            //using (IRandomAccessStream fileStream = f.OpenAsync(Windows.Storage.FileAccessMode.Read).GetResults())
            //{
            //    BitmapImage bitmapImage = new BitmapImage();
            //    bitmapImage.SetSourceAsync(fileStream).GetResults();
            //    img.Source = bitmapImage;
            //}

            

            //foreach (Photo photo in photos)
            //{
                setImg(e.Parameter as string);
               // img.Source = new BitmapImage(new Uri, UriKind.Absolute));


            //}



        }


        private async void setImg(string p)
        {
            var photos = await PhotoLibraryObj.LoadPhotoes(p);

            Windows.Storage.Pickers.FileOpenPicker openPicker =
                new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.ViewMode =
                Windows.Storage.Pickers.PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            //Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            StorageFile file = await StorageFile.GetFileFromPathAsync(photos[0].Path);

            // 'file' is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                        new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    img.Source = bitmapImage;
                }
            }

        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(AddPhoto));
        }
    }
}

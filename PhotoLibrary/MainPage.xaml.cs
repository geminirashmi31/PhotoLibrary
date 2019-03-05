using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewPhotoLibrary));
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PhotoLibraryView));
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PhotoLibraryView), "L1");
            
        }
        //void Image_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Image img = sender as Image;
        //    if (img != null)
        //    {
        //        BitmapImage bitmapImage = new BitmapImage();
        //        img.Width = bitmapImage.DecodePixelWidth = 280;
        //        bitmapImage.UriSource = new Uri("eden.jpg");
        //        img.Source = bitmapImage;
        //    }
        //}

    }
}

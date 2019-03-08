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
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PhotoLibraryManager manager;

        public ObservableCollection<StackPanel> Items { get; set; } = new ObservableCollection<StackPanel>();

        public MainPage()
        {
            this.InitializeComponent();
            manager = PhotoLibrary.PhotoLibraryManager.GetInstance();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var libraryMetadata = await manager.LoadPhotoLibraries();
            DataContext = libraryMetadata;
            await ShowImages(libraryMetadata);
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewPhotoLibrary));
        }

        private async Task ShowImages(IList<LibraryMetadata> libraryMetadata)
        {
            foreach (var data in libraryMetadata)
            {
                var file = await StorageFile.GetFileFromPathAsync(data.CoverPicPath);
                await ShowPhoto(file);
            }
        }


        private async Task ShowPhoto(StorageFile file)

        {

            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))

            {

                StackPanel group = new StackPanel

                {

                    Orientation = Orientation.Vertical,

                    Margin = new Thickness(3)

                };



                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.SetSource(fileStream);

                Image image = new Image();

                image.Source = bitmapImage;

                image.MaxHeight = 100;

                image.MaxWidth = 80;



                group.Children.Add(image);

                group.Children.Add(new TextBlock { Text = file.DisplayName });

                group.Children.Add(new CheckBox { Name = file.Path });

                Items.Add(group);

            }

        }


        /*
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PhotoLibraryView));
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PhotoLibraryView), (sender as Button).Content);
        }
        */

    }
}

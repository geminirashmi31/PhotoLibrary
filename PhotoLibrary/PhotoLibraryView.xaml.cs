using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
        public string LibraryName { get; set; }

        public PhotoLibraryView()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LibraryName = e.Parameter as string;
            ShowImages();
        }


        private async void ShowImages()
        {
            if (!libraries.ContainsKey(LibraryName))
            {
                libraries.Add(LibraryName, await PhotoLibraryObj.LoadPhotoLibraryAsync(LibraryName));
            }

            var photos = libraries[LibraryName].GetPhotos();

            // Open the file picker.
            //Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();
            foreach (Photo photo in photos)
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(photo.Path);

                // 'file' is null if user cancels the file picker.
                if (file != null)
                {
                    // Open a stream for the selected file.
                    // The 'using' block ensures the stream is disposed
                    // after the image is loaded.
                    await ShowPhoto(file);
                }


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
                group.Children.Add(new TextBlock{Text = file.DisplayName});
                group.Children.Add(new CheckBox{Name=file.Path});

                Items.Add(group);
            }
        }


        public ObservableCollection<StackPanel> Items { get; set; } = new ObservableCollection<StackPanel>();

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
           this.Frame.Navigate(typeof(AddPhoto), libraries[LibraryName]);
        }
        
        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            
            for(var i=0; i<Items.Count; i++)
            {
                CheckBox checkbox = Items[i].Children.First(child => child is CheckBox) as CheckBox;
                if (checkbox.IsChecked ?? false)
                {
                    libraries[LibraryName].RemovePhotoPathAsync(checkbox.Name);
                    Items.RemoveAt(i);
                    i--;
                }
            }

        }

        private void SetCoverPhoto_Click(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                CheckBox checkbox = Items[i].Children.First(child => child is CheckBox) as CheckBox;
                if (checkbox.IsChecked ?? false)
                {
                    libraries[LibraryName].SelectCoverPhotoAsync(checkbox.Name);
                    checkbox.IsChecked = false;
                    break;
                }
            }

        }

        private static Dictionary<string, PhotoLibraryObj> libraries = new Dictionary<string, PhotoLibraryObj>();
    }
}

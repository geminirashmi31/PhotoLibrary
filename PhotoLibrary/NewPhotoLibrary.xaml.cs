using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewPhotoLibrary : Page
    {
        public NewPhotoLibrary()
        {
            this.InitializeComponent();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            var name = nameText.Text;
            var path = pathLabel.Text;

            var library = await PhotoLibraryObj.CreatePhotoLibraryAsync(name, path);
            
            var libraryManager = PhotoLibraryManager.GetInstance();
            await libraryManager.AddPhotoLibraryAsync(library);
            
            ReturnToHomePage();
        }

        private void ReturnToHomePage()
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void SelectImageFile_Click(object sender, RoutedEventArgs e)
        {
            var file = await PhotoPicker.PickFileAsync();

            if(file != null)
            {
                pathLabel.Text = file.Path;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

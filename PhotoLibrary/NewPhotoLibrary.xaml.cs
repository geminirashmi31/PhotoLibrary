using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var name = nameText.Text;
            var path = pathText.Text;

            var t1 = Task.Run(async () => await PhotoLibraryObj.CreatePhotoLibraryAsync(name, path));
            Task.WaitAny(t1);
            var library = t1.Result;
            

            
            var libraryManager = PhotoLibraryManager.GetInstance();
            var t2 = Task.Run(async () => await libraryManager.AddPhotoLibraryAsync(library));
            Task.WaitAny(t2);

            ReturnToHomePage();
        }

        private void ReturnToHomePage()
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}

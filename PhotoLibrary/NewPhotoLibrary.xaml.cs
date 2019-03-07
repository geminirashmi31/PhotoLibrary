using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private void AddLibrary_Click(object sender, RoutedEventArgs e)
        {
            PhotoLibraryObj.CreatePhotoLibrary("L4").ContinueWith(task =>
                {
                    task.Result.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\eden.jpg");
                    task.Result.AddPhotoPath("C:\\Users\\lentochka\\Desktop\\karen.jpg");
                });
        }
    }
}

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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace ImageViewer_Phase_2
{ 
    public sealed partial class ViewerPage : Page
    {
        public ViewerPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            await InitialLoad();
        }


        async Task InitialLoad()
        {
            var dummyfolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"ImageProvider\Dummies");
            
            foreach (var fs in await dummyfolder.GetFilesAsync())
            {
                TheFlipView.Items.Add(new BitmapImage(new Uri(fs.Path)));
                
            } 
        }

    }
}

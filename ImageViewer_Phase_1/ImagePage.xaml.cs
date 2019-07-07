using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ImageViewer_Phase_1
{

    public sealed partial class ImagePage : Page
    {

        public ScrollBarVisibility FitImageVertically
        {
            get { return (ScrollBarVisibility)GetValue(FitImageVerticallyProperty); }
            set { SetValue(FitImageVerticallyProperty, value); }
        }

        public static readonly DependencyProperty FitImageVerticallyProperty =
            DependencyProperty.Register("FitImageVertically", typeof(ScrollBarVisibility), typeof(ImagePage), new PropertyMetadata(ScrollBarVisibility.Visible));


        public ScrollBarVisibility FitImageHorizontally
        {
            get { return (ScrollBarVisibility)GetValue(FitImageHorizontallyProperty); }
            set { SetValue(FitImageHorizontallyProperty, value); }
        }

        public static readonly DependencyProperty FitImageHorizontallyProperty =
            DependencyProperty.Register("FitImageHorizontally", typeof(ScrollBarVisibility), typeof(ImagePage), new PropertyMetadata(ScrollBarVisibility.Visible));




        BitmapImage[] StoredImages = new BitmapImage[4];

        int CurrentImageIndex = -1;


        public ImagePage()
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
            var dummyfolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"DummyImages");

            int indx = 0;
            foreach (var fs in await dummyfolder.GetFilesAsync())
            {
                StoredImages[indx] = new BitmapImage(new Uri(fs.Path));
                indx++;
            }

            //initial image set
            CurrentImageIndex = 0;
            CoreImage.Source = StoredImages[0];
        }








        private void ChangeImageButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            CurrentImageIndex = (CurrentImageIndex + 1) % StoredImages.Length;//cicular incrementing
            CoreImage.Source = StoredImages[CurrentImageIndex];

            ZoomAndPanningScrollViewer.ChangeView(0, 0, 1);//reset zoom
        }


        private void LockVerticallyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FitImageVertically = LockVerticallyToggleButton.IsChecked == true ?
                                 ScrollBarVisibility.Visible : ScrollBarVisibility.Disabled;
            ZoomAndPanningScrollViewer.ChangeView(0, 0, 1);
        }

        private void LockHorizontallyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FitImageHorizontally = LockVerticallyToggleButton.IsChecked == true ?
                                   ScrollBarVisibility.Visible : ScrollBarVisibility.Disabled;
            ZoomAndPanningScrollViewer.ChangeView(0, 0, 1);
        }

        private void ZoomInButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var ZoomAmount = ZoomAndPanningScrollViewer.ZoomFactor - 0.15f;
            ZoomAndPanningScrollViewer.ChangeView(null, null, ZoomAmount);
        }

        private void ZoomOutButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var ZoomAmount = ZoomAndPanningScrollViewer.ZoomFactor + 0.15f;
            ZoomAndPanningScrollViewer.ChangeView(null, null, ZoomAmount);
        }


    }
}

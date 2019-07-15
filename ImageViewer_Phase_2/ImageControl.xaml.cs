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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ImageViewer_Phase_2
{
    public sealed partial class ImageControl : UserControl
    {

        public ScrollBarVisibility FitImageVertically
        {
            get { return (ScrollBarVisibility)GetValue(FitImageVerticallyProperty); }
            set { SetValue(FitImageVerticallyProperty, value); }
        }

        public static readonly DependencyProperty FitImageVerticallyProperty =
            DependencyProperty.Register("FitImageVertically", typeof(ScrollBarVisibility), typeof(ImageControl), new PropertyMetadata(ScrollBarVisibility.Disabled));


        public ScrollBarVisibility FitImageHorizontally
        {
            get { return (ScrollBarVisibility)GetValue(FitImageHorizontallyProperty); }
            set { SetValue(FitImageHorizontallyProperty, value); }
        }

        public static readonly DependencyProperty FitImageHorizontallyProperty =
            DependencyProperty.Register("FitImageHorizontally", typeof(ScrollBarVisibility), typeof(ImageControl), new PropertyMetadata(ScrollBarVisibility.Disabled));

        public ImageControl()
        {
            this.InitializeComponent();
        }


        private void LockVerticallyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FitImageVertically = LockVerticallyToggleButton.IsChecked == true ?
                                  ScrollBarVisibility.Disabled : 
                                  ScrollBarVisibility.Visible;
            ZoomAndPanningScrollViewer.ChangeView(0, 0, 1);
        }

        private void LockHorizontallyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            FitImageHorizontally = LockHorizontallyToggleButton.IsChecked == true ?
                                   ScrollBarVisibility.Disabled : 
                                   ScrollBarVisibility.Visible;
            ZoomAndPanningScrollViewer.ChangeView(0, 0, 1);
        }

        private void ZoomInButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var ZoomAmount = ZoomAndPanningScrollViewer.ZoomFactor + 0.15f;
            ZoomAndPanningScrollViewer.ChangeView(null, null, ZoomAmount);
        }

        private void ZoomOutButton_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var ZoomAmount = ZoomAndPanningScrollViewer.ZoomFactor - 0.15f;
            ZoomAndPanningScrollViewer.ChangeView(null, null, ZoomAmount);
        
        }
    }
}

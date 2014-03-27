using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Sikbug
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool isImagePressCompleted = true;
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnFacebook_Click(object sender, RoutedEventArgs e)
        {
            View.Login.loginType = "Facebook";
            NavigationService.Navigate(new Uri("/View/Login.xaml", UriKind.Relative));
        }

        private void btnTwitter_Click(object sender, RoutedEventArgs e)
        {
            View.Login.loginType = "Twitter";
            NavigationService.Navigate(new Uri("/View/Login.xaml", UriKind.Relative));
            //NavigationService.Navigate(new Uri("/View/TwitterAuthPage.xaml", UriKind.Relative));
        }

        private void btnGPS_Click(object sender, RoutedEventArgs e)
        {
            Utils.Location loc = new Utils.Location();
            loc.getLocation();
            loc.getLocationFinished += new Utils.Location.LocationDelegate(loc_getLocationFinished);
        }

        void loc_getLocationFinished(Model.Location location)
        {
            throw new NotImplementedException();
        }

        #region Image Press Events
        private void imgAroundMe_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = imgAroundMe;
            imagePressDown(image);
            //imagePressAnimation(image);
        }

        private void imgReport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = imgReport;
            imagePressDown(image);
        }

        private void imgFavourite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = imgFavourite;
            imagePressDown(image);
        }

        private void imgSettings_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = imgSettings;
            imagePressDown(image);
        }
        #endregion

        private void imagePressDown(Image image)
        {
            ((CompositeTransform)image.RenderTransform).TranslateX = 5;
            ((CompositeTransform)image.RenderTransform).TranslateY = 2;
        }

        private void imagePressUp(Image image)
        {
            ((CompositeTransform)image.RenderTransform).TranslateX = 0;
            ((CompositeTransform)image.RenderTransform).TranslateY = 0;
        }

        private void imgAroundMe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = imgAroundMe;
            imagePressUp(image);
            NavigationService.Navigate(new Uri("/View/MapPage.xaml", UriKind.Relative));
        }

        private void imgReport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = imgReport;
            imagePressUp(image);
            NavigationService.Navigate(new Uri("/View/ReportSicknessList.xaml", UriKind.Relative));
        }

        private void imgFavourite_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = imgFavourite;
            imagePressUp(image);
            NavigationService.Navigate(new Uri("/View/FavoriteLocation.xaml", UriKind.Relative));
        }

        private void imgSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = imgSettings;
            imagePressUp(image);
            NavigationService.Navigate(new Uri("/View/Settings.xaml", UriKind.Relative));
        }

        private void imgAroundMe_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = imgAroundMe;
            imagePressUp(image);
        }

        private void imgReport_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = imgReport;
            imagePressUp(image);
        }

        private void imgFavourite_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = imgFavourite;
            imagePressUp(image);
        }

        private void imgSettings_MouseLeave(object sender, MouseEventArgs e)
        {
            Image image = imgSettings;
            imagePressUp(image);
        }


        #region Image Press Animation
        //private void imagePressAnimation(Image image)
        //{
        //    if (isImagePressCompleted)
        //    {
        //        isImagePressCompleted = false;
        //        Storyboard storyboard = (Storyboard)Resources["imgPressAnimation"];
        //        Storyboard.SetTarget(storyboard.Children.ElementAt(0) as DoubleAnimationUsingKeyFrames, image);
        //        Storyboard.SetTarget(storyboard.Children.ElementAt(1) as DoubleAnimationUsingKeyFrames, image);
        //        storyboard.Completed += new EventHandler(storyboard_Completed);
        //        storyboard.Begin();
        //    }
        //}

        //private void storyboard_Completed(object sender, EventArgs e)
        //{
        //    Storyboard storyboard = (Storyboard)sender;
        //    storyboard.Stop();
        //    isImagePressCompleted = true;
        //}
        #endregion
    }
}
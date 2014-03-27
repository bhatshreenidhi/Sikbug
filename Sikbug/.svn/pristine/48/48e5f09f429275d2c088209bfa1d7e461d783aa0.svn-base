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

using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Sikbug.View
{
    public partial class MyAccount : PhoneApplicationPage
    {
        
        bool _isPageLoaded = false;
        public MyAccount()
        {
            InitializeComponent();      
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            checkSettings();
        }

        private void checkSettings()
        {
            if (App._LoginType != null)
            {
                _isPageLoaded = true;
                switch (App._LoginType.ToLower())
                {
                    case "facebook": tglFacebook.IsChecked = true;
                        break;
                    case "twitter": tglTwitter.IsChecked = true;
                        break;
                    case "google": tglGoogle.IsChecked = true;
                        break;
                    default: break;
                }
            }
            else
            {
                tglFacebook.IsChecked = false;
                tglTwitter.IsChecked = false;
                tglGoogle.IsChecked = false;
            }
        }

        private void tglFacebook_Checked(object sender, RoutedEventArgs e)
        {
            tglTwitter.IsChecked = false;
            tglGoogle.IsChecked = false;
            if (!_isPageLoaded)
            {
                //remove other auths
                
                View.Login.loginType = "Facebook";
                NavigationService.Navigate(new Uri("/View/Login.xaml", UriKind.Relative));
            }
        }

        private void tglFacebook_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_isPageLoaded)
            {
                Model.Login login = Model.Login.Instance();
                login = null;
              
                App.LoadAppSettings(login);
            }
        }

        private void tglTwitter_Checked(object sender, RoutedEventArgs e)
        {
            tglFacebook.IsChecked = false;
            tglGoogle.IsChecked = false;
            if (!_isPageLoaded)
            {
               
                View.Login.loginType = "Twitter";
                NavigationService.Navigate(new Uri("/View/Login.xaml", UriKind.Relative));
            }
        }

        private void tglTwitter_Unchecked(object sender, RoutedEventArgs e)
        {
            if (_isPageLoaded)
            {
                Model.Login login = Model.Login.Instance();
                login = null;

                App.LoadAppSettings(login);
            }
        }

    }
}
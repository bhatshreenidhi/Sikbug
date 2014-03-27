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

namespace Sikbug.View
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
            List<string> settingsNames = new List<string>();
            settingsNames.Add("MY ACCOUNT");
            settingsNames.Add("SHARE SIKBUG");
            settingsNames.Add("HELP & TIPS");
            settingsNames.Add("ABOUT");       
            lsbSettings.ItemsSource = settingsNames;
        }

        private void lsbSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedItem = lsbSettings.SelectedIndex;
            switch (selectedItem)
            {
                case 0: lsbSettings.SelectedIndex = -1;
                        NavigationService.Navigate(new Uri("/View/MyAccount.xaml", UriKind.Relative)); 
                        break;
                case 1: lsbSettings.SelectedIndex = -1;
                        NavigationService.Navigate(new Uri("/View/ShareSikbug.xaml", UriKind.Relative)); 
                        break;
                case 2: lsbSettings.SelectedIndex = -1; 
                        break;
                case 3: lsbSettings.SelectedIndex = -1; 
                        break;
            }
        }
    }
}
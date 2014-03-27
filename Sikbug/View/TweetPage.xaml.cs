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
using Microsoft.Phone.Shell;
using System.Windows.Navigation;
using Sikbug.Helpers;
using Sikbug.Utils;

namespace TweetApp.Views
{
    public partial class TweetPage : PhoneApplicationPage
    {
        private TwitterAccess _twitterSettings;
        public TweetPage()
        {
            InitializeComponent();
            CreateApplicationBar();
            UpdateRemainingCharacters();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _twitterSettings = IsolatedStorage.LoadSetting<TwitterAccess>(TwitterSettings.TwitterAccess);
            if (_twitterSettings == null) return;

            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = !String.IsNullOrEmpty(_twitterSettings.AccessToken) && !String.IsNullOrEmpty(_twitterSettings.AccessTokenSecret);
        }

        private void UpdateRemainingCharacters()
        {
            CharactersCountTextBlock.Text = String.Format("{0} characters remaining", 140 - TweetTextBox.Text.Length);
        }

        private void TweetTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRemainingCharacters();
        }

        private void MessageTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Focus();
            }
        }

        #region ApplicationBar

        private void CreateApplicationBar()
        {
            ApplicationBar = new ApplicationBar { IsMenuEnabled = true, IsVisible = true, Opacity = .9 };

            var save = new ApplicationBarIconButton(new Uri("/Resources/Images/check.png", UriKind.Relative));
            save.Text = "tweet";
            save.Click += SaveClick;
            save.IsEnabled = false;

            var cancel = new ApplicationBarIconButton(new Uri("/Resources/Images/cancel.png", UriKind.Relative));
            cancel.Text = "cancel";
            cancel.Click += CancelClick;

            ApplicationBar.Buttons.Add(save);
            ApplicationBar.Buttons.Add(cancel);
        }

        private void CancelClick(object sender, EventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
        private void SaveClick(object sender, EventArgs e)
        {
            PostTweet();
        }

        #endregion
        private void PostTweet()
        {
            if (String.IsNullOrEmpty(TweetTextBox.Text))
                return;

            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;

            var twitter = new TwitterHelper(_twitterSettings);

            // Successful event handler, navigate back if successful
            twitter.TweetCompletedEvent += (sender, e) =>
            {
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            };

            // Failed event handler, show error
            twitter.ErrorEvent += (sender, e) =>
            {
                ProgressBar.Visibility = Visibility.Collapsed;
                ProgressBar.IsIndeterminate = false;
                MessageBox.Show("There was an error connecting to Twitter");
            };

            twitter.NewTweet(TweetTextBox.Text);
        }
    }
}
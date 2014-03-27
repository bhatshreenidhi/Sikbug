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
using Sikbug.Helpers;
using Microsoft.Phone.Shell;
using Sikbug.Utils;
using Facebook;
using Microsoft.Phone.Tasks;

namespace Sikbug.View
{
    public partial class ShareSikbug : PhoneApplicationPage
    {
        private bool _isCommentOpen = false;
        private Sikbug.Helpers.TwitterAccess _twitterSettings;
        public ShareSikbug()
        {
            InitializeComponent();
            ApplicationBar.IsVisible = false;
            BackKeyPress += new EventHandler<System.ComponentModel.CancelEventArgs>(ShareSikbug_BackKeyPress);
        }

        void ShareSikbug_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_isCommentOpen)
            {
                return;
            }
            e.Cancel = true;
            commentVisible(false);
            ApplicationBar.IsVisible = false;
            CharactersCountTextBlock.Visibility = Visibility.Collapsed;
        }

        private void lsbShare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int selectedIndex = lsbShare.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    lsbShare.SelectedIndex = -1;
                    EmailComposeTask emailComposeTask = new EmailComposeTask();
                    emailComposeTask.Show();
                    break;
                case 1:
                    lsbShare.SelectedIndex = -1;
                    if (App._APIToken == null)
                    {

                        MessageBox.Show("You are not currently logged in to facebook", "Can't share", new MessageBoxButton());
                        
                        break;
                    }
                    if (App._LoginType != "facebook")
                    {
                        MessageBox.Show("You are not currently logged in to facebook", "Can't share", new MessageBoxButton());
                  
                        break;

                    }
                    CreateFacebookApplicationBar();
                    ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
                    imgShareType.Source = imgFacebook.Source;
                    commentVisible(true);
                    break;
                case 2:
                    lsbShare.SelectedIndex = -1;
                    if (App._APIToken == null)
                    {
                        MessageBox.Show("You are not currently logged in to twitter", "Can't share", new MessageBoxButton());
                        
                        break;
                    }
                    _twitterSettings = IsolatedStorage.LoadSetting<TwitterAccess>(TwitterSettings.TwitterAccess);
                    if (_twitterSettings != null)
                    {
                        imgShareType.Source = imgTwitter.Source;
                        commentVisible(true);
                        CharactersCountTextBlock.Visibility = Visibility.Visible;
                        CreateTwitterApplicationBar();
                        UpdateRemainingCharacters();
                        LoadTwitterUI();
                    }
                    else
                    {
                        MessageBox.Show("You are not currently logged in to twitter");
                    }
                    break;
                default: commentVisible(false);
                    break;
            }
        }



        private void commentVisible(bool isVisible)
        {
            if (isVisible)
            {
                lsbShare.Visibility = Visibility.Collapsed;
                imgShareType.Visibility = Visibility.Visible;
                txtComment.Visibility = Visibility.Visible;
                _isCommentOpen = true;
            }
            else
            {
                imgShareType.Visibility = Visibility.Collapsed;
                txtComment.Visibility = Visibility.Collapsed;
                lsbShare.Visibility = Visibility.Visible;
                _isCommentOpen = false;
            }
        }

        #region Share Twitter
        private void LoadTwitterUI()
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = !String.IsNullOrEmpty(_twitterSettings.AccessToken) && !String.IsNullOrEmpty(_twitterSettings.AccessTokenSecret);

        }

        private void UpdateRemainingCharacters()
        {
            CharactersCountTextBlock.Text = String.Format("{0} characters remaining", 140 - txtComment.Text.Length);
        }

        private void PostTweet()
        {
            if (String.IsNullOrEmpty(txtComment.Text))
            {
                MessageBox.Show("Enter message.");
                return;
            }

            //***Progress bar visible

            var twitter = new TwitterHelper(_twitterSettings);

            // Successful event handler, navigate back if successful
            twitter.TweetCompletedEvent += (sender, e) =>
            {
                CharactersCountTextBlock.Visibility = Visibility.Collapsed;
                //***Progress bar collapse
                if (NavigationService.CanGoBack)
                    NavigationService.GoBack();
            };

            // Failed event handler, show error
            twitter.ErrorEvent += (sender, e) =>
            {
                //***Progress bar collapse
                MessageBox.Show("There was an error connecting to Twitter");
            };

            twitter.NewTweet(txtComment.Text);
        }
        #endregion
        #region Twitter ApplicationBar

        private void CreateTwitterApplicationBar()
        {
            ApplicationBar = new ApplicationBar { IsMenuEnabled = true, IsVisible = true, Opacity = .9 };

            var save = new ApplicationBarIconButton(new Uri("/Resources/Images/check.png", UriKind.Relative));
            save.Text = "tweet";
            save.Click += SaveClick;
            save.IsEnabled = false;

            //var cancel = new ApplicationBarIconButton(new Uri("/Resources/Images/cancel.png", UriKind.Relative));
            //cancel.Text = "cancel";
            //cancel.Click += CancelClick;

            ApplicationBar.Buttons.Add(save);
            //ApplicationBar.Buttons.Add(cancel);
        }
        //private void CancelClick(object sender, EventArgs e)
        //{
        //    if (NavigationService.CanGoBack)
        //        NavigationService.GoBack();
        //}
        private void SaveClick(object sender, EventArgs e)
        {
            PostTweet();
        }

        #endregion

        #region Share Facebook
        string _lastMessageId;
        string _accessToken;
        private void PostToWall()
        {
            if (String.IsNullOrEmpty(txtComment.Text))
            {
                MessageBox.Show("Enter message.");
                return;
            }
            //Model.Login login = Model.Login.Instance();
            _accessToken = App._AccessToken;

            if (_accessToken != null)
            {
                var fb = new FacebookClient(_accessToken);

                // make sure to add event handler for PostCompleted.
                fb.PostCompleted += (o, args) =>
                {
                    // incase you support cancellation, make sure to check
                    // e.Cancelled property first even before checking (e.Error!=null).
                    if (args.Cancelled)
                    {
                        // for this example, we can ignore as we don't allow this
                        // example to be cancelled.

                        // you can check e.Error for reasons behind the cancellation.
                        var cancellationError = args.Error;
                    }
                    else if (args.Error != null)
                    {
                        // error occurred
                        Dispatcher.BeginInvoke(() =>
                        {
                            MessageBox.Show(args.Error.Message);
                        });
                    }
                    else
                    {
                        // the request was completed successfully

                        // now we can either cast it to IDictionary<string, object> or IList<object>
                        var result = (IDictionary<string, object>)args.GetResultData();
                        _lastMessageId = (string)result["id"];

                        // make sure to be on the right thread when working with ui.
                        Dispatcher.BeginInvoke(() =>
                        {
                            MessageBox.Show("Message Posted successfully");

                            txtComment.Text = string.Empty;
                            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = true;
                            //btnDeleteLastMessage.IsEnabled = true;
                        });
                    }
                };

                var parameters = new Dictionary<string, object>();
                parameters["message"] = txtComment.Text;

                fb.PostAsync("me/feed", parameters);
            }
            else
            {
                MessageBox.Show("Please Re-Login");
            }
        }

        private void DeleteLastMessage()
        {
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;

            var fb = new FacebookClient(_accessToken);

            // make sure to add event handler for DeleteCompleted.
            fb.DeleteCompleted += (o, args) =>
            {
                // incase you support cancellation, make sure to check
                // e.Cancelled property first even before checking (e.Error!=null).
                if (args.Cancelled)
                {
                    // for this example, we can ignore as we don't allow this
                    // example to be cancelled.

                    // you can check e.Error for reasons behind the cancellation.
                    var cancellationError = args.Error;
                }
                else if (args.Error != null)
                {
                    // error occurred
                    Dispatcher.BeginInvoke(
                        () =>
                        {
                            MessageBox.Show(args.Error.Message);
                        });
                }
                else
                {
                    // the request was completed successfully

                    // make sure to be on the right thread when working with ui.
                    Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show("Message deleted successfully");
                        ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
                    });
                }
            };

            fb.DeleteAsync(_lastMessageId);
        }
        #endregion

        #region Facebook ApplicationBar
        private void CreateFacebookApplicationBar()
        {
            ApplicationBar = new ApplicationBar { IsMenuEnabled = true, IsVisible = true, Opacity = .9 };

            var post = new ApplicationBarIconButton(new Uri("/Resources/Images/check.png", UriKind.Relative));
            post.Text = "Post";
            post.Click += PostClick;
            post.IsEnabled = false;

            var deletePost = new ApplicationBarIconButton(new Uri("/Resources/Images/cancel.png", UriKind.Relative));
            deletePost.Text = "Delete";
            deletePost.Click += DeleteLastClick;
            deletePost.IsEnabled = false;

            ApplicationBar.Buttons.Add(post);
            ApplicationBar.Buttons.Add(deletePost);
        }
        private void DeleteLastClick(object sender, EventArgs e)
        {
            DeleteLastMessage();
        }
        private void PostClick(object sender, EventArgs e)
        {
            PostToWall();
        }
        #endregion
        private void txtComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtComment.Text.Length >= 0)
            {
                UpdateRemainingCharacters();
            }
            else
            {

            }
        }

        private void MessageTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Focus();
            }
        }






    }
}
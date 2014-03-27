using System;
using System.Net;
using System.Windows;
using Hammock.Authentication.OAuth;
using Hammock;
using Hammock.Web;

namespace Sikbug.Helpers
{
    public class TwitterSettings
    {
        public static string ConsumerKey = "WWkzqivvkizfPsOyF5Mm2g";
        public static string ConsumerKeySecret = "fIzSoKbOMO1ubnu4vD0doY5ESXdMzrWYai77TkrZE5w";
        public static string RequestTokenUri = "https://api.twitter.com/oauth/request_token";
        public static string OAuthVersion = "1.0a";
        public static string CallbackUri = "http://www.bing.com";
        public static string AuthorizeUri = "https://api.twitter.com/oauth/authorize";
        public static string AccessTokenUri = "https://api.twitter.com/oauth/access_token";
        public static string TwitterAccess = "TwitterAccess";
        public static string QueryString;
    }

    public class TwitterAccess
    {
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string UserId { get; set; }
        public string ScreenName { get; set; }
    }

    public class TwitterHelper
    {
        private readonly TwitterAccess _twitterSettings;
        private readonly bool _authorized;
        private readonly OAuthCredentials _credentials;
        private readonly RestClient _client;
        public event EventHandler TweetCompletedEvent;
        public event EventHandler ErrorEvent;

        public TwitterHelper(TwitterAccess settings)
        {
            _twitterSettings = settings;

            if (_twitterSettings == null || String.IsNullOrEmpty(_twitterSettings.AccessToken) ||
               String.IsNullOrEmpty(_twitterSettings.AccessTokenSecret))
            {
                return;
            }

            _authorized = true;

            _credentials = new OAuthCredentials
            {
                Type = OAuthType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = TwitterSettings.ConsumerKey,
                ConsumerSecret = TwitterSettings.ConsumerKeySecret,
                Token = _twitterSettings.AccessToken,
                TokenSecret = _twitterSettings.AccessTokenSecret,
                Version = TwitterSettings.OAuthVersion,
            };

            _client = new RestClient
            {
                Authority = "http://api.twitter.com",
                HasElevatedPermissions = true
            };
        }

        public void NewTweet(string tweetText)
        {
            if (!_authorized)
            {
                if (ErrorEvent != null)
                    ErrorEvent(this, EventArgs.Empty);
                return;
            }

            var request = new RestRequest
            {
                Credentials = _credentials,
                Path = "/statuses/update.xml",
                Method = WebMethod.Post
            };

            request.AddParameter("status", tweetText);

            _client.BeginRequest(request, new RestCallback(NewTweetCompleted));
        }

        private void NewTweetCompleted(RestRequest request, RestResponse response, object userstate)
        {
            // We want to ensure we are running on our thread UI
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (TweetCompletedEvent != null)
                        TweetCompletedEvent(this, EventArgs.Empty);
                }
                else
                {
                    if (ErrorEvent != null)
                        ErrorEvent(this, EventArgs.Empty);
                }
            });
        }
    }
}

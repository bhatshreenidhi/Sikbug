using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Sikbug.Utils;

namespace Sikbug.Services
{
    public class LoginService
    {
        public delegate void LoginDelegate(bool LoginStatus);
        public event LoginDelegate LoginFinished;

        public void Authenticate(string LoginType, string userID)
        {
            //uid 198954737
            Model.Login login = Model.Login.Instance();
            string url = "http://ws.sikbug.com/authentications.json?provider=" + login.LoginType + "&uid=" +login.UserID + "&api_key="+App._APIKey;
            WebClient req = new WebClient();
            req.UploadStringAsync(new Uri(url), "POST", "");
            req.UploadStringCompleted += new UploadStringCompletedEventHandler(req_UploadStringCompleted);
        }

        void req_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {            
            try
            {
                string x = e.Result;
                Model.ServiceModel.Login loginService = Utils.Parser.Deserialize<Model.ServiceModel.Login>(x);
                if (loginService != null)
                {
                    Model.Login login = Model.Login.Instance();
                    login.SikbugAPIToken = loginService.api_token.token;
                    login.SikbugUserID = loginService.api_token.user.id;
                    login.SikbugUserEmail = loginService.api_token.user.email;
                    login.SikbugUserForeName = loginService.api_token.user.forename;
                    login.SikbugUserSurname = loginService.api_token.user.surname;
                    IsolatedStorage.SaveSetting("LoginDetails", login);
                    
                    App.LoadAppSettings(login);
                    
                    LoginFinished(true);
                }
                else
                {
                    LoginFinished(false);
                }
            }
            catch (Exception ex)
            {
                LoginFinished(false);
            }
        }
        //public delegate void (Resources.PreferencesDownload preferences);
        //public event downloadPreferencesDelegate downloadPreferencesFinished;

        //protected void Authenticate(string LoginType)
        //{
        //    preferences.UserId = Convert.ToInt32(App._ID);
        //    //Parsing into JSON
        //    Parsing parse = new Parsing();
        //    string parsedData = parse.ParsePreferences(preferences);

        //    IPAddress ip = new IPAddress();
        //    //Calling Authentication service

        //    string url = "http://" + ip.getIP() + ":8080/EventManagementService/EventApp/participant/preferences";
        //    Uri uri = new Uri(url);
        //    WebClient wc = new WebClient();
        //    wc.UploadStringAsync(uri, "POST", parsedData);
        //    wc.UploadStringCompleted += new UploadStringCompletedEventHandler(PreferencesUploadCompleted);
        //}


        //void PreferencesUploadCompleted(object sender, UploadStringCompletedEventArgs e)
        //{
        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Resources.PreferencesSuccess));

        //    Resources.PreferencesSuccess success = new Resources.PreferencesSuccess();
        //    success = (Resources.PreferencesSuccess)ser.ReadObject(ms);
        //    PreferencesUploadFinished(success.status);
        //}
    }
}

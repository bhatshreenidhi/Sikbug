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

namespace Sikbug.Model.ServiceModel
{
    //{"api_token":{"token":"e4162081-e1d3-49ec-8bd8-cee87aa174a2","user":{"email":null,"forename":null,"id":97,"surname":null}}}
    public class Login
    {
        public APIToken api_token { get; set; }
        public Login()
        {
            api_token = new APIToken();
        }

    }
    public class APIToken
    {
        public string token { get; set; }
        public User user { get; set; }
        public APIToken()
        {
            token = null;
            user = new User();
        }
    }

    public class User
    {
        public string id { get; set; }
        public string surname { get; set; }
        public string forename { get; set; }
        public string email { get; set; }
        public User()
        {
            id = null;
            surname = null;
            forename = null;
            email = null;
        }
    }
}

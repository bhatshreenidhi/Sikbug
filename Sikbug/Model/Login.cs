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

namespace Sikbug.Model
{
    public class Login
    {
        public string SikbugUserID { get; set; }
        public string SikbugUserSurname{ get; set; }
        public string SikbugUserForeName { get; set; }
        public string SikbugUserEmail { get; set; }
        public string SikbugAPIToken { get; set; }
        public string UserID { get; set; }
        public string LoginType { get; set; }
        public string AccessToken{ get; set; }
        private static Login _instance;

        // Constructor is 'protected'
        public Login()
        {
        }

        public static Login Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new Login();
            }

            return _instance;
        }
    }
}

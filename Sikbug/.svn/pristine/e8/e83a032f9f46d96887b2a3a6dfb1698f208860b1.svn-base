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
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Sikbug.Utils
{
    //public class C4FAbout
    //{

    //}
    public class Coding4FunAboutPrompt : AboutPrompt
    {
        public Coding4FunAboutPrompt()
        {
            WaterMark = new Coding4FunWaterMark();
            //Footer = new Coding4FunFooter();
        }
    }
    public class Coding4FunWaterMark : Control
    {
        public Coding4FunWaterMark()
        {
            DefaultStyleKey = typeof(Coding4FunWaterMark);
        }
    }
    public class Coding4FunFooter : Control
    {
        private const string C4FLogoName = "c4fLogo";
        protected Image C4FLogo;

        public Coding4FunFooter()
        {
            DefaultStyleKey = typeof(Coding4FunFooter);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (C4FLogo != null)
                C4FLogo.ManipulationCompleted -= c4fLogo_ManipulationCompleted;

            C4FLogo = GetTemplateChild(C4FLogoName) as Image;

            if (C4FLogo != null)
                C4FLogo.ManipulationCompleted += c4fLogo_ManipulationCompleted;
        }

        void c4fLogo_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            NavigateTo("http://www.coding4fun.com");
        }

        private static void NavigateTo(string uri)
        {
            var web = new WebBrowserTask { Uri = new Uri(uri) };

            web.Show();
        }
    }
}

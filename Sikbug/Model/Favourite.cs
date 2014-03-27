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
using System.ComponentModel;

namespace Sikbug.Model
{
    public class Favourite 
    {

        public int id { get; set; }
        public int user_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public string name { get; set; }



        public double latitude { get; set; }
        public double longitude { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged(String info)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //    }
        //}


    }
}

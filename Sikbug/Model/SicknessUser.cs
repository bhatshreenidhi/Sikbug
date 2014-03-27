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
using System.Collections.Generic;

namespace Sikbug.Model
{
    public class SicknessUser
    {
        public string id{get;set;}
        public string user_id{get;set;}
        public string zip_code{get;set;}
        public string sickness_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
       

        public Sickness sickness { get; set; }
        
        public SicknessUser()
        {
           sickness= new Sickness();
        }
    

        

    }
}

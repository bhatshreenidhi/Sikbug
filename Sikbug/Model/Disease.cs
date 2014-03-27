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
    public class Disease : INotifyPropertyChanged
    {
        private string diseaseName;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string DiseaseName
        {
            get
            {
                return this.diseaseName;
            }
            set
            {
                this.diseaseName = value;
                NotifyPropertyChanged(DiseaseName);
            }
        }
    }
}

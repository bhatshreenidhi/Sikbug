using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Sikbug.Model;
using Sikbug.Helpers;
using System.Windows.Media;

namespace Sikbug
{
    public partial class SicknessListPage : PhoneApplicationPage
    {

        List<Disease> diseaseList = new List<Disease>();

        // Constructor
        public SicknessListPage()
        {
            InitializeComponent();
            
            
        }

        private void imgCheckUnckeck_Click(object sender, RoutedEventArgs e)
        {
            if((sender as CheckBox).IsChecked==true)
            {
                Disease diseaseName = (Sikbug.Model.Disease)((sender as CheckBox).DataContext);
                diseaseList.Add(diseaseName);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string str = String.Empty;
            foreach (Disease disease in diseaseList)
                str =str+" "+disease.DiseaseName+",";
            TwitterSettings.QueryString = str;
            NavigationService.Navigate(new Uri("/View/PostSickness.xaml",UriKind.RelativeOrAbsolute));
        }

        private void imgCheckUncheck_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Image sourceImg = (Image)sender;
            if (sourceImg.Tag.Equals("unCheck"))
            {
                sourceImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("/Sikbug;component/Resources/Images/checkImg.png");
                sourceImg.Tag = "Check";
            }
            else
            {
                sourceImg.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("/Sikbug;component/Resources/Images/uncheckImg.jpg");
                sourceImg.Tag = "unCheck";
            }
        }

        private void imgPopUp_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            menuPopUp.IsOpen = true;
            menuPopUp.IsHitTestVisible = true;
            lstb.IsHitTestVisible = false;
        }

       
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //int x = lstb.Items.Count;
            //for (int count = 0; count < x; count++)
            //{

                
            //}
            //this.NavigationService.Navigate(new Uri("/View/ListMainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            menuPopUp.IsOpen = false;
            menuPopUp.IsHitTestVisible = false;
            lstb.IsHitTestVisible = true;
        }

        private void imgHome_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

       
    }
}
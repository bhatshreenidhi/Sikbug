﻿using System;
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
using System.Device.Location;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.ComponentModel;
using Microsoft.Phone.Controls.Maps;

namespace Sikbug.View
{
    public partial class AddFavourite : PhoneApplicationPage
    {

        BackgroundWorker bckgrndThread = new BackgroundWorker();
        public ObservableCollection<Model.Location> CurrentLocation = new ObservableCollection<Model.Location>();
        GeoCoordinate newCurrentLocn = new GeoCoordinate();

        //Adding a pushpin layer along with the popUP
        Microsoft.Phone.Controls.Maps.MapLayer pushPinLayer = new Microsoft.Phone.Controls.Maps.MapLayer();

        GeoCoordinate currentLoc;
        //Add a mapItemControl for binding pushpins on to the MAP
        Microsoft.Phone.Controls.Maps.MapItemsControl pushPinItemLoc = new Microsoft.Phone.Controls.Maps.MapItemsControl();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
       

        public AddFavourite()
        {
            InitializeComponent();

            //*** Hardcoding to be removed
            currentLoc = new GeoCoordinate(12.9, 77.6);
            renderMap(currentLoc);
            
          
        }

       public void renderMap(GeoCoordinate currentLoc)
        {
            CurrentLocation.Clear();
            Model.Location CurrentLoc = new Model.Location();
            CurrentLoc.Coordinate = currentLoc;
            CurrentLoc.type = "Loc";

            mapSikbug.Center = CurrentLoc.Coordinate;
            CurrentLocation.Add(CurrentLoc);

            if (mapSikbug.Children.Contains(pushPinItemLoc))
                mapSikbug.Children.Remove(pushPinItemLoc);

            mapSikbug.Children.Add(pushPinItemLoc);

           
            pushPinItemLoc.Visibility = Visibility.Visible;

            pushPinItemLoc.ItemTemplate = dataTemplatePushpin;
            pushPinItemLoc.ItemsSource = CurrentLocation;
        }

        private void txxSearchBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox txbAddress = sender as TextBox;
            txbAddress.Text = "";
        }

        private void txxSearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox txbAddress = sender as TextBox;
            if (txbAddress.Text.ToString().Trim().Equals(string.Empty) || txbAddress.Text.ToString().Trim().Equals(""))
            {
                txbAddress.Text = "Search City, Zip etc...";
            }

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
           GeoCoordinate cor = DraggablePushpin.newLocation;
           stkInput.Visibility = Visibility.Visible;
        }

       
        private void inputPrompt_Completed(object sender, Coding4Fun.Phone.Controls.PopUpEventArgs<string, Coding4Fun.Phone.Controls.PopUpResult> e)
        {
            if (inputPrompt.Value != "")
            {
                string strFavoriteName = inputPrompt.Value;
                

                HomePanaroma.AddFavfromForm = "AddFavorite";
                if (!DraggablePushpin.newLocation.Latitude.ToString().Equals("NaN"))
                    HomePanaroma.AddFavLatitude = DraggablePushpin.newLocation.Latitude.ToString();
                else
                    HomePanaroma.AddFavLatitude = currentLoc.Latitude.ToString();

                if (!DraggablePushpin.newLocation.Longitude.ToString().Equals("NaN"))
                    HomePanaroma.AddFavLongitude = DraggablePushpin.newLocation.Longitude.ToString();
                else
                    HomePanaroma.AddFavLongitude = currentLoc.Longitude.ToString();

                HomePanaroma.AddFavLocName = strFavoriteName;


                NavigationService.Navigate(new Uri("/View/HomePanaroma.xaml", UriKind.RelativeOrAbsolute));
            }

            stkInput.Visibility = Visibility.Collapsed;
            inputPrompt.Value = "";
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            Services.AroundMeService aroundMeService = new Services.AroundMeService();
            aroundMeService.GetLocationCoordinates(txxSearchBox.Text);
            aroundMeService.downloadCoordinateFinished += new Services.AroundMeService.GeoCodingDelegate(aroundMeService_downloadCoordinateFinished);
        }

        void aroundMeService_downloadCoordinateFinished(List<string> latLongList)
        {
            GeoCoordinate currentLoc = new GeoCoordinate(Double.Parse(latLongList.ElementAt(0)), Double.Parse(latLongList.ElementAt(1)));
            CurrentLocation.Clear();

            

            Model.Location CurrentLoc = new Model.Location();
            CurrentLoc.Coordinate = currentLoc;
            CurrentLoc.type = "Loc";

            mapSikbug.Center = CurrentLoc.Coordinate;
            CurrentLocation.Add(CurrentLoc);
        }

       public void aroundMeService_downloadCoordinateFinished(GeoCoordinate currentLocn)
        {
            newCurrentLocn = currentLocn;
            bckgrndThread.DoWork += new DoWorkEventHandler(bckgrndThread_DoWork);
            bckgrndThread.RunWorkerAsync();
           
        }

       void bckgrndThread_DoWork(object sender, DoWorkEventArgs e)
       {
           Dispatcher.BeginInvoke(() => { 
           GeoCoordinate currentLoc = new GeoCoordinate(newCurrentLocn.Latitude, newCurrentLocn.Longitude);
           CurrentLocation.Clear();

           
           Model.Location CurrentLoc = new Model.Location();
           CurrentLoc.Coordinate = currentLoc;
           CurrentLoc.type = "Loc";
           
           mapSikbug.Center = CurrentLoc.Coordinate;
           CurrentLocation.Add(CurrentLoc);

           mapSikbug.UpdateLayout();
           mapSikbug.ZoomLevel = mapSikbug.ZoomLevel + 0.00001;
           });
       }

       private void txxSearchBox_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.Key == Key.Enter)
           {
               if (txxSearchBox.Text.Trim() != "")
               {
                   Services.AroundMeService aroundMeService = new Services.AroundMeService();
                   aroundMeService.GetLocationCoordinates(txxSearchBox.Text);
                   aroundMeService.downloadCoordinateFinished += new Services.AroundMeService.GeoCodingDelegate(aroundMeService_downloadCoordinateFinished);
               }
           }
       }
    }
}
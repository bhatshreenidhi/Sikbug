using System;
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
using System.Collections.ObjectModel;
using System.Device.Location;

namespace Sikbug.View
{
    public partial class FavoriteLocation : PhoneApplicationPage
    {

        ObservableCollection<Model.Location> myFavLocations = new ObservableCollection<Model.Location>();

        public FavoriteLocation()
        {
            InitializeComponent();
            
            getFavPlaces();

            lsbSymptoms.ItemsSource = myFavLocations;

        }

        private void getFavPlaces()
        {
            //Get data from webService
            GeoCoordinate l1 = new GeoCoordinate(53, 73);
            GeoCoordinate l2 = new GeoCoordinate(14, 120);
            GeoCoordinate l3 = new GeoCoordinate(48, 133);
            GeoCoordinate l4 = new GeoCoordinate(2, 11);
            GeoCoordinate l5 = new GeoCoordinate(0, 40);

            Model.Location lq = new Model.Location();
            lq.Coordinate = l1;
            lq.FavoriteLocationName = "BANGALORE";
            Model.Location lw = new Model.Location();
            lw.Coordinate = l2;
            lw.FavoriteLocationName = "DELHI";
            Model.Location le = new Model.Location();
            le.FavoriteLocationName = "SINGAPORE";
            Model.Location lr = new Model.Location();
            lr.FavoriteLocationName = "MELBOURNE";
            Model.Location lt = new Model.Location();
            lt.FavoriteLocationName = "MANGALORE";

            myFavLocations.Add(lq);
            myFavLocations.Add(lw);
            myFavLocations.Add(le);
            myFavLocations.Add(lr);
            myFavLocations.Add(lt);
        }

        private void imgAddLocation_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgEditLocation_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            btnDone.Visibility = Visibility.Visible;
            
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            btnDone.Visibility = Visibility.Collapsed;
        }
    }
}
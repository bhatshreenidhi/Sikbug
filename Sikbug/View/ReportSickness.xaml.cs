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
using Sikbug.Helpers;

namespace Sikbug.View
{
    public partial class PostSickness : PhoneApplicationPage
    {
        Sikbug.Utils.Location locationObj = new Utils.Location();
        public PostSickness()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Retrive the user Name

            //Set the text of the welcome label
            txbWelcome.Text = txbWelcome.Text + " " +App._SikbugUserID;

            txtDescription.Text = txtDescription.Text + " with " + TwitterSettings.QueryString.ToString();

            
            locationObj.getLocation();
            locationObj.getLocationFinished += new Utils.Location.LocationDelegate(locationObj_getLocationFinished);
            //locationObj.getCurrentLocation();
            //locationObj.positionChangeDetermined += new Utils.Location.delegatePositionChange(locationObj_positionChangeDetermined);

            //Get the current location and set it
            


        }

        void locationObj_getLocationFinished(Model.Location location)
        {

            //locationObj.getLocationName(double.Parse(location.Coordinate.Latitude), double.Parse(location.Coordinate.Latitude));
            //txbLocation.Text = txbLocation.Text + " " + locationObj.address;
        }

        void locationObj_positionChangeDetermined(Utils.Location loc)
        {
            //loc.getLocationName(loc.Latitude, loc.Longitude);
            //txbLocation.Text = txbLocation.Text + " " + loc.Address;
        }

        
    }
}
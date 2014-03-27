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
using System.Device.Location;
using System.Collections.Generic;

namespace Sikbug.Model
{
    public class Location : INotifyPropertyChanged
    {


        private GeoCoordinate _coordinate = new GeoCoordinate();
        //private Microsoft.Maps.MapControl.Location _mapLocation = new Microsoft.Maps.MapControl.Location();
        public string type;
        private string _favoriteLocationName = string.Empty;
        private List<string> _locationDiseases = new System.Collections.Generic.List<string>();
        private string _diseaseString = string.Empty;

        #region Properties


        public GeoCoordinate Coordinate
        {
            get
            {
                return _coordinate;
            }
            set
            {
                this._coordinate = value;

                //this._mapLocation.Latitude = this.Coordinate.Latitude;
                //this._mapLocation.Longitude = this.Coordinate.Longitude;
                //this._mapLocation.Altitude = this.Coordinate.Altitude;

                OnPropertyChanged("Coordinate");
            }
        }


        public string DiseaseString
        {
            get
            {
                return _diseaseString;
            }
            set
            {
                this._diseaseString = value;
                OnPropertyChanged("DiseaseString");
            }
        }

        //public Microsoft.Maps.MapControl.Location MapLocation
        //{
        //    get
        //    {
        //        return _mapLocation;
        //    }
        //    set
        //    {
        //        this._mapLocation.Latitude = this.Coordinate.Latitude;
        //        this._mapLocation.Longitude = this.Coordinate.Longitude;
        //        this._mapLocation.Altitude = this.Coordinate.Altitude;

        //        OnPropertyChanged("MapLocation");
        //    }
        //}

        public string FavoriteLocationName
        {
            get
            {
                return _favoriteLocationName;
            }
            set
            {
                this._favoriteLocationName = value;
                OnPropertyChanged("Coordinate");
            }
        }

        public List<string> LocationDiseases
        {
            set
            {
                this._locationDiseases = value;
                OnPropertyChanged("LocationDiseases");
            }
            get
            {
                return this._locationDiseases;
            }

        }

        #endregion


        #region INotifyPropertyMethod

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}

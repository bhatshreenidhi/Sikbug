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
using googlemaps;
using Microsoft.Maps.MapControl;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Text;
using System.IO;
using Sikbug.Model;
using Sikbug.Utils;
using System.Windows.Navigation;
using Sikbug.Services;
using System.ComponentModel;

namespace Sikbug.View
{
    public partial class MapPage : PhoneApplicationPage
    {
        BackgroundWorker worker = new BackgroundWorker();
        AroundMeService aroundMeSrvice;
        public static string _myLatitude;
        public static string _mylongitide;
        //List to bind the loactions
        public ObservableCollection<Model.Location> DiseaseLocations = new ObservableCollection<Model.Location>();

        public ObservableCollection<Model.Location> CurrentLocation = new ObservableCollection<Model.Location>();

        //Adding a pushpin layer along with the popUP
        Microsoft.Phone.Controls.Maps.MapLayer pushPinLayer = new Microsoft.Phone.Controls.Maps.MapLayer();
        

        //Add a mapItemControl for binding pushpins on to the MAP
        Microsoft.Phone.Controls.Maps.MapItemsControl pushPinItemLoc = new Microsoft.Phone.Controls.Maps.MapItemsControl();

        //Add a mapItemControl for binding pushpins on to the MAP
        Microsoft.Phone.Controls.Maps.MapItemsControl pushPinItemDisease = new Microsoft.Phone.Controls.Maps.MapItemsControl();

        //Add a mapItemControl for binding PopUP over the Pushpins
        Microsoft.Phone.Controls.Maps.MapItemsControl pushPinItemPopUp = new Microsoft.Phone.Controls.Maps.MapItemsControl();

        enum MapPageType
        {
            addFavouritePage = 0,
            sicknessAroundMe
        };

        MapPageType type;

        public MapPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
               
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            getCoordinates();
            addItemToMap();
        }
        

        private void addItemToMap()
        {
            Dispatcher.BeginInvoke(() =>
            {
                mapSikbug.Children.Add(pushPinLayer);
                mapSikbug.Children.Add(pushPinItemDisease);
                mapSikbug.Children.Add(pushPinItemLoc);

                pushPinLayer.Visibility = Visibility.Collapsed;


                pushPinItemLoc.ItemTemplate = dataTemplatePushpin;
                pushPinItemLoc.ItemsSource = DiseaseLocations;

                pushPinItemDisease.ItemTemplate = dataTemplatePushPinCurrentLoc;
                pushPinItemDisease.ItemsSource = CurrentLocation;
            });


        }


        #region Get COORDINATES

        public void getCoordinates()
        {            
            Utils.Location location = new Utils.Location();
            location.getLocation();
            location.getLocationFinished += new Utils.Location.LocationDelegate(location_getLocationFinished);
        }

        void location_getLocationFinished(Model.Location location)
        {
            if (location != null)
            {
                _myLatitude = location.Coordinate.Latitude.ToString();
                _mylongitide = location.Coordinate.Longitude.ToString();
                aroundMeSrvice = new AroundMeService();
                double lat = 12.92;
                double lon = 77.61;
                double radius = 10;
                //aroundMeSrvice.GetSicknesses(location.Coordinate.Latitude, location.Coordinate.Longitude, 0);
                aroundMeSrvice.GetSicknesses(lat, lon, radius);
                aroundMeSrvice.SicknessWithCoordinatesFinished += new AroundMeService.SicknessWithCoordinatesDelegate(aroundMeSrvice_SicknessWithCoordinatesFinished);
            }
        }

        void aroundMeSrvice_SicknessWithCoordinatesFinished(List<JsonSickness> sicknessList)
        {
            if (sicknessList != null)
            {
                
                Dispatcher.BeginInvoke(() => {
                    mapSikbug.Visibility = Visibility.Visible;
                    stkLoading.Visibility = Visibility.Collapsed;
                    foreach (JsonSickness usr in sicknessList)
                    {
                        double latitude = Double.Parse(usr.sicknesses_user.latitude);
                        double longitude = Double.Parse(usr.sicknesses_user.longitude);

                        GeoCoordinate coordinate = new GeoCoordinate(latitude, longitude);
                        Model.Location location = new Model.Location();
                        location.Coordinate = coordinate;

                        location.DiseaseString = usr.sicknesses_user.sickness.sick;
                        string[] sickness = usr.sicknesses_user.sickness.sick.Split(',');
                        foreach (string strDisease in sickness)
                            location.LocationDiseases.Add(strDisease);
                        DiseaseLocations.Add(location);
                    }

                    GeoCoordinate currentLoc = new GeoCoordinate(12.9, 77.6);
                    //***Use this instead
                    //GeoCoordinate currentLoc = new GeoCoordinate(_myLatitude, _mylongitide);
                    Model.Location CurrentLoc = new Model.Location();
                    CurrentLoc.Coordinate = currentLoc;
                    CurrentLoc.type = "Loc";
                    Dispatcher.BeginInvoke(() => { mapSikbug.Center = CurrentLoc.Coordinate; });

                    CurrentLocation.Add(CurrentLoc);
                });
            }
            
        }
        #endregion

        private void Pushpin_MouseEnter(object sender, MouseEventArgs e)
        {
            Microsoft.Phone.Controls.Maps.Pushpin selectedPushpin = (sender as Microsoft.Phone.Controls.Maps.Pushpin);
            Model.Location locn = selectedPushpin.DataContext as Model.Location;


            if (locn.type == null)
            {
                var about = new Coding4FunAboutPrompt();
                about.Show(locn.DiseaseString.ToString());
                about.VersionNumber = "";
            }
            else
            {
                //if (pushPinLayer.Children.Contains(pushPinItemPopUp))
                //    pushPinLayer.Children.Remove(pushPinItemPopUp);
            
            }
        }
       
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SicknessAroundMe.xaml", UriKind.RelativeOrAbsolute));
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

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
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

    //#region GOOGLE MAP DISPLAY

    //public enum GoogleTileTypes
    //{
    //    Hybrid,
    //    Physical,
    //    Street,
    //    Satellite,
    //    WaterOverlay
    //}

    //public class GoogleTile : Microsoft.Phone.Controls.Maps.TileSource
    //{
    //    private int _server;
    //    private char _mapmode;
    //    private GoogleTileTypes _tiletypes;

    //    public GoogleTileTypes TileTypes
    //    {
    //        get { return _tiletypes; }
    //        set
    //        {
    //            _tiletypes = value;
    //            MapMode = MapModeConverter(value);
    //        }
    //    }

    //    public char MapMode
    //    {
    //        get { return _mapmode; }
    //        set { _mapmode = value; }
    //    }

    //    public int Server
    //    {
    //        get { return _server; }
    //        set { _server = value; }
    //    }

    //    public GoogleTile()
    //    {
    //        UriFormat = @"http://mt{0}.google.com/vt/lyrs={1}&z={2}&x={3}&y={4}";
    //        Server = 0;
    //    }

    //    public override Uri GetUri(int x, int y, int zoomLevel)
    //    {
    //        if (zoomLevel > 0)
    //        {
    //            var Url = string.Format(UriFormat, Server, MapMode, zoomLevel, x, y);
    //            return new Uri(Url);
    //        }
    //        return null;
    //    }

    //    private char MapModeConverter(GoogleTileTypes tiletype)
    //    {
    //        switch (tiletype)
    //        {
    //            case GoogleTileTypes.Hybrid:
    //                {
    //                    return 'y';
    //                }
    //            case GoogleTileTypes.Physical:
    //                {
    //                    return 't';
    //                }
    //            case GoogleTileTypes.Satellite:
    //                {
    //                    return 's';
    //                }
    //            case GoogleTileTypes.Street:
    //                {
    //                    return 'm';
    //                }
    //            case GoogleTileTypes.WaterOverlay:
    //                {
    //                    return 'r';
    //                }
    //        }
    //        return ' ';
    //    }
    //}
    //#endregion
} 
    
   
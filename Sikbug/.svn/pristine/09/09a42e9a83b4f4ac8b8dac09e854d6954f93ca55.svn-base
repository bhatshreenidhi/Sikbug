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
using System.Windows.Interactivity;
using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.Core;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using System.Collections.Generic;

namespace Sikbug.View
{
    /// <summary>
    /// This behaviour adds drag capabilities to a Bing Maps <see cref="Pushpin"/> control.
    /// </summary>
    public class DraggablePushpin : Behavior<Microsoft.Phone.Controls.Maps.Pushpin>
    {
        #region Members
        private bool _isdragging = false;
        private Microsoft.Phone.Controls.Maps.Map _map = null;
        private EventHandler<Microsoft.Phone.Controls.Maps.MapDragEventArgs> ParentMapMousePanHandler;
        private MouseButtonEventHandler ParentMapMouseLeftButtonEventHandler;
        private MouseEventHandler ParentMapMouseMoveHandler;
        private GeoCoordinate _location;
        public static GeoCoordinate newLocation = new GeoCoordinate();
        Sikbug.View.AddFavourite addFav = new AddFavourite();
        
        #endregion
        /// <summary>
        /// Hook the event handlers to this instance.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown +=
      new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown);
        }
        /// <summary>
        /// Unhook the events when this is detaching.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -=
      new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown);
            DetachEvents();
        }

        /// <summary>
        /// Make sure that no mouse events are left dangling.
        /// </summary>
        private void DetachEvents()
        {
            if (_map == null) return;
            if (this.ParentMapMousePanHandler != null)
            {
                
                _map.MapPan -= ParentMapMousePanHandler;
                ParentMapMousePanHandler = null;
            }
            if (ParentMapMouseLeftButtonEventHandler != null)
            {
                _map.MouseLeftButtonUp -= ParentMapMouseLeftButtonEventHandler;
                ParentMapMouseLeftButtonEventHandler = null;
            }
            if (ParentMapMouseMoveHandler != null)
            {
                _map.MouseMove -= ParentMapMouseMoveHandler;
                ParentMapMouseMoveHandler = null;
            }
        }
        /// <summary>
        /// Only move the poi if the user accepts the change
        /// (at which point, it's saved to the database).
        /// </summary>
        private void ConfirmMoveLocation()
        {
            _isdragging = false;
            if (_location == null) return;

            DetachEvents();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to move here?",
      "Move location?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                AssociatedObject.Location = _location;
                newLocation = _location;
                addFav.aroundMeService_downloadCoordinateFinished(newLocation);
            }
            _location = null;
        }
        /// <summary>
        /// Called when the left mouse button is pressed on a
        /// Point of Interest.
        /// </summary>
        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_map == null)
                // Find the map this pushpin is attached to.
                _map = FindParent<Microsoft.Phone.Controls.Maps.Map>(AssociatedObject);
            if (_map != null)
            {
                if (this.ParentMapMousePanHandler == null)
                {
                    this.ParentMapMousePanHandler = new EventHandler<MapDragEventArgs>
                    ((s, epan) => epan.Handled = this._isdragging);
                    _map.MapPan += ParentMapMousePanHandler;
                }
                if (this.ParentMapMouseLeftButtonEventHandler == null)
                {
                    this.ParentMapMouseLeftButtonEventHandler = new MouseButtonEventHandler
                    ((s, args) => ConfirmMoveLocation());
                    _map.MouseLeftButtonUp += this.ParentMapMouseLeftButtonEventHandler;
                }
                if (this.ParentMapMouseMoveHandler == null)
                {
                    // If the mouse is performing a drag operation, convert the
                    // underlying location based on the position of the mouse
                    // on the viewport to a map based location.
                    this.ParentMapMouseMoveHandler = new MouseEventHandler(
                    (s, mouseargs) =>
                    {
                        if (_isdragging)
                            _location = _map.ViewportPointToLocation(mouseargs.GetPosition(_map));
                    });
                    _map.MouseMove += this.ParentMapMouseMoveHandler;
                }
            }
            _isdragging = true;
        }
        /// <summary>
        /// Find the relevant parent item for a particular dependency object.
        /// </summary>
        /// <typeparam name="T">The type of object to search for.</typeparam>
        /// <param name="child">The <see cref="DependencyObject"/> to start searching from.</param>
        /// <returns>The parent object if found, null otherwise.</returns>
        public static T FindParent<T>(DependencyObject child) where T : FrameworkElement
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            // If parentObject is null, we've reached the top of the tree without finding the item we were looking for.
            if (parentObject == null) return null;
            T parent = parentObject as T;
            // If parent is null, recursively call this method.
            if (parent == null)
                return FindParent<T>(parentObject);
            // If we reach this point, we have found the parent item we are looking for.
            return parent;
        }
    }
}

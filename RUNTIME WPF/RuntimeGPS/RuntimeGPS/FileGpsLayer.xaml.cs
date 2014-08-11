using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Local.Gps;
using ESRI.ArcGIS.Client.Projection;
using ESRI.ArcGIS.Client.Toolkit.DataSources;
using Geometry = ESRI.ArcGIS.Client.Geometry.Geometry;

namespace RuntimeGPS
{
    public partial class FileGpsLayer : UserControl
    {
        readonly GpsLayer _gpsLayer;

        public FileGpsLayer()
        {
            InitializeComponent();

            Application.Current.Exit += Current_Exit;

            FileGpsCoordinateWatcher geoPositionWatcher = new FileGpsCoordinateWatcher
            {
                Path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"data\flight.nmea"),
                LoopPlayback = true,
                PlaybackRate = 1
            };

            _gpsLayer = new GpsLayer { GeoPositionWatcher = geoPositionWatcher };
            MyMap.Layers.Add(_gpsLayer);

            MyMap.Loaded += MyMap_Loaded;
            geoPositionWatcher.PositionChanged += geoPositionWatcher_PositionChanged;
        }

        void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            Envelope envelope = new Envelope(-8805113.07760239, 3998145.25315465, -8790906.62187389, 4007897.14225642);
            MyMap.Extent = envelope;
        }

        void geoPositionWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (MyMap.Extent == null) return;
            double x = e.Position.Location.Longitude;
            double y = e.Position.Location.Latitude;

            if (double.IsNaN(x) || double.IsNaN(y)) return;

            MapPoint p = new MapPoint {X = x, Y = y, SpatialReference = new SpatialReference(4326)}; // This comes in as decimal degrees.

            WebMercator wm = new WebMercator();
            Geometry g = wm.FromGeographic(p);

            Envelope extent = MyMap.Extent.Expand(0.9);
            MapPoint[] polygon = 
                {
                    new MapPoint(extent.XMin, extent.YMin, MyMap.SpatialReference),
                    new MapPoint(extent.XMin, extent.YMax, MyMap.SpatialReference),
                    new MapPoint(extent.XMax, extent.YMax, MyMap.SpatialReference),
                    new MapPoint(extent.XMax, extent.YMin, MyMap.SpatialReference)
                };

            if (PointInPolygon(polygon, g as MapPoint)) return;

            int wkid = MyMap.SpatialReference.WKID;

            MyMap.PanTo(g as MapPoint);
            MyMap.UpdateLayout();
        }

        static bool PointInPolygon(IList<MapPoint> polygon, MapPoint point)
        {
            bool isInside = false;

            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / 
                    (polygon[j].Y - polygon[i].Y) + polygon[i].X))

                    isInside = !isInside;
            }

            return isInside;
        }

        void Current_Exit(object sender, ExitEventArgs e)
        {
            IDisposable fileGpsWatcher = _gpsLayer.GeoPositionWatcher as IDisposable;
            if (fileGpsWatcher != null) fileGpsWatcher.Dispose();
        }
    }
}

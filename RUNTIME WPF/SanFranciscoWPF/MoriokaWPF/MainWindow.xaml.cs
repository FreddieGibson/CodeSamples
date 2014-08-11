using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Local;
using ESRI.ArcGIS.Client.Symbols;
using ESRI.ArcGIS.Client.Tasks;

namespace MoriokaWPF
{
    public partial class MainWindow : UserControl
    {
        Draw _bDraw;
        List<Graphic> _stops = new List<Graphic>();
        List<Graphic> _block = new List<Graphic>();
        GraphicsLayer _stopsLayer = null;
        GraphicsLayer _blockLayer = null;
        GraphicsLayer _routeLayer = null;
        Geoprocessor _gpTask = null;
        LocalGeoprocessingService _localGPService = null;

        private static readonly String _bin = System.IO.Path.GetDirectoryName(System.IO.Directory.GetParent(
                    System.Reflection.Assembly.GetExecutingAssembly().Location.ToString()).FullName);
        public static readonly String _gpk = System.IO.Path.Combine(_bin, @"Data\Routing.gpk");
        public static readonly String _mpk = System.IO.Path.Combine(_bin, @"Data\morioka_jp.mpk");
        
        public MainWindow()
        {
            InitializeComponent();
            _stopsLayer = MyMap.Layers["MyStopsGraphicsLayer"] as GraphicsLayer;
            _blockLayer = MyMap.Layers["MyBlockGraphicsLayer"] as GraphicsLayer;
            _routeLayer = MyMap.Layers["MyRouteGraphicsLayer"] as GraphicsLayer;

            _bDraw = new Draw(MyMap)
            {
                FillSymbol = LayoutRoot.Resources["BlockSymbol"] as FillSymbol,
                DrawMode = DrawMode.Polygon,
                IsEnabled = false
            };

            _bDraw.DrawComplete += _bDraw_DrawComplete;
            _localGPService = new LocalGeoprocessingService(_gpk, GPServiceType.Execute);
            _localGPService.StartAsync((callback) =>
                {
                    if (callback.Error == null)
                    {
                        _gpTask = new Geoprocessor(_localGPService.UrlGeoprocessingService + "/routing");
                        MessageBox.Show("Started Local GP Service");
                    }
                    else
                    {
                        MessageBox.Show("Error starting routing service");
                    }
                });
        }

        private void _bDraw_DrawComplete(object sender, DrawEventArgs e)
        {
            Graphic block = new Graphic() { Geometry = e.Geometry, Symbol = LayoutRoot.Resources["BlockSymbol"] as FillSymbol };
            _blockLayer.Graphics.Add(block);
            _block.Add(block);
        }

        private void StopsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this._bDraw != null) { this._bDraw.IsEnabled = false; }
        }

        private void BarriersRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this._bDraw.IsEnabled = true;
        }

        private void MyMap_MouseClick(object sender, Map.MouseEventArgs e)
        {
            if (StopsRadioButton.IsChecked.Value)
            {
                Graphic stop = new Graphic
                {
                    Geometry = e.MapPoint,
                    Symbol = LayoutRoot.Resources["StopsSymbol"] as Symbol
                };
                stop.Attributes.Add("StopNumber", _stopsLayer.Graphics.Count + 1);
                _stops.Add(stop);
                _stopsLayer.Graphics.Add(stop);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _stops.Clear();
            _block.Clear();
            foreach (Layer layer in MyMap.Layers)
            {
                if (layer is GraphicsLayer) { (layer as GraphicsLayer).ClearGraphics(); }
            }
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            _gpTask.CancelAsync();

            List<GPParameter> gpParams = new List<GPParameter>();

            var fs0 = new FeatureSet(_stops) { SpatialReference = new SpatialReference(4326) };
            var fs1 = new FeatureSet(_block) { SpatialReference = new SpatialReference(4326) };

            // http://resources.arcgis.com/en/help/rest/apiref/pcs.html
            // 2452 JGD_2000_Japan_Zone_10 
            //    PROJCS["JGD_2000_Japan_Zone_10",GEOGCS["GCS_JGD_2000", 

            //var fs0 = new FeatureSet(_stops) { SpatialReference = new SpatialReference(2452) };
            //var fs1 = new FeatureSet(_block) { SpatialReference = new SpatialReference(2452) };
            //gpParams.Add(new GPFeatureRecordSetLayer("Input Locations", fs0));
            //gpParams.Add(new GPFeatureRecordSetLayer("Input Barriers", fs1));
            
            gpParams.Add(new GPFeatureRecordSetLayer("Input_Stops", fs0));
            gpParams.Add(new GPFeatureRecordSetLayer("Input_Block", fs1));

            

            GraphicsLayer routeLayer = MyMap.Layers["MyRouteGraphicsLayer"] as GraphicsLayer;

            _gpTask.ExecuteCompleted += (s, e1) =>
            {
                _routeLayer.Graphics.Clear();
                GPExecuteResults results = e1.Results;
                GPFeatureRecordSetLayer rs = results.OutParameters[0] as GPFeatureRecordSetLayer;
                foreach (Graphic route in rs.FeatureSet.Features)
                {
                    route.Symbol = LayoutRoot.Resources["RouteSymbol"] as ESRI.ArcGIS.Client.Symbols.Symbol;
                    _routeLayer.Graphics.Add(route);
                }
            };

            _gpTask.Failed += (s2, e2) => { MessageBox.Show(e2.Error.Message); };

            _gpTask.ExecuteAsync(gpParams);
            
        }
    }
}
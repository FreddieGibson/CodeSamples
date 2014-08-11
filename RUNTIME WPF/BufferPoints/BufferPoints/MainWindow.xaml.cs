using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Local;
using ESRI.ArcGIS.Client.Symbols;
using ESRI.ArcGIS.Client.Tasks;
using System;
using System.Collections.Generic;
using System.Windows;

namespace BufferPoints
{

    public partial class MainWindow : Window
    {
        private readonly string[] _bufferColors = {"DashSymbol", "DashDotSymbol", "DashDotDotSymbol", "DotSymbol", "SolidSymbol"};
        private int _colorIndex = 0;

        private readonly string _src = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private bool oddClick = false;

        public MainWindow()
        {
            // License setting and ArcGIS Runtime initialization is done in Application.xaml.cs.

            InitializeComponent();
            
        }

        private void MyMap_MouseClick(object sender, Map.MouseEventArgs e)
        {
            GraphicsLayer graphicsLayer = MyMap.Layers["MyGraphicsLayer"] as GraphicsLayer;
            graphicsLayer.Graphics.Clear();

            e.MapPoint.SpatialReference = MyMap.SpatialReference;
            Graphic graphic = new Graphic
            {
                Geometry = e.MapPoint,
                Symbol = LayoutRoot.Resources["DefaultClickSymbol"] as Symbol
            };
            graphic.SetZIndex(1);
            graphicsLayer.Graphics.Add(graphic);

            GeometryService geometryService = new GeometryService("http://tasks.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");
            geometryService.BufferCompleted += GeometryService_BufferCompleted;
            geometryService.Failed += GeometryService_Failed;

            // If buffer spatial reference is GCS and unit is linear, geometry service will do geodesic buffering
            BufferParameters bufferParams = new BufferParameters()
            {
                Unit = LinearUnit.Kilometer,
                BufferSpatialReference = MyMap.SpatialReference,
                OutSpatialReference = MyMap.SpatialReference
            };

            bufferParams.Features.Add(graphic);
            foreach (var length in new []{5, 10, 15, 20, 25})
            {
                bufferParams.Distances.Add(length);    
            }

            geometryService.BufferAsync(bufferParams);
        }

        void GeometryService_BufferCompleted(object sender, GraphicsEventArgs args)
        {
            IList<Graphic> results = args.Results;
            GraphicsLayer graphicsLayer = MyMap.Layers["MyGraphicsLayer"] as GraphicsLayer;
            
            if (!oddClick)
            {
                foreach (Graphic graphic in results)
                {
                    _colorIndex++;
                    if (_colorIndex == _bufferColors.Length) _colorIndex = 0;

                    graphic.Symbol = LayoutRoot.Resources[_bufferColors[_colorIndex]] as Symbol;
                    graphicsLayer.Graphics.Add(graphic);
                }
            }
            else
            {
                ArcGISLocalFeatureLayer fl = MyMap.Layers["fl"] as ArcGISLocalFeatureLayer;
                SimpleRenderer r = fl.Renderer as SimpleRenderer;
                Symbol s = r.Symbol;

                Graphic graphic = results[results.Count - 1];
                graphic.Symbol = s;
                graphicsLayer.Graphics.Add(graphic);
            }

            oddClick = !oddClick;
        }

        private void GeometryService_Failed(object sender, TaskFailedEventArgs e)
        {
            MessageBox.Show("Geometry Service error: " + e.Error);
        }

    }
}

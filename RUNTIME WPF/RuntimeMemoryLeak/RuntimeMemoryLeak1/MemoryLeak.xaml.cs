using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.FeatureService.Symbols;
using ESRI.ArcGIS.Client.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace RuntimeMemoryLeak1
{
    public partial class MemoryLeak
    {
        private static bool removed = false;

        public MemoryLeak()
        {
            InitializeComponent();

            // Get Graphics Layer from Map and assign Renderer
            GraphicsLayer graphicsLayer = MyMap.Layers["MyGraphicsLayer"] as GraphicsLayer;
            graphicsLayer.Renderer = new SimpleRenderer
            {
                Symbol = new SimpleMarkerSymbol { Color = new SolidColorBrush(Colors.Black), Size = 8, 
                 Style = SimpleMarkerSymbol.SimpleMarkerStyle.Circle, }
            };

            this.NumberOfSatellites.Loaded += NumberOfSatellites_Loaded;
        }

        static void NumberOfSatellites_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Add items to Combo Box for use
            int[] numberOfSatellites = {10, 10000, 100000};
            ComboBox cbx = sender as ComboBox;
            cbx.ItemsSource = numberOfSatellites;
            cbx.SelectedIndex = 0;
        }

        private void NumberOfSatellites_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When the ComboBox Selected Index is changed, display the selected number of graphics in the map.
            GraphicsLayer graphicsLayer = MyMap.Layers["MyGraphicsLayer"] as GraphicsLayer;

            int numberOfSatellites = 0;
            int.TryParse(this.NumberOfSatellites.SelectedItem.ToString(), out numberOfSatellites);
            graphicsLayer.GraphicsSource = UpdateSatellites(numberOfSatellites);
        }

        private static IEnumerable<Graphic> UpdateSatellites(int numberOfSatellites)
        {
            // Create the specified number of features and return them as a collection
            List<Graphic> graphics = new List<Graphic>();

            Random random = new Random();
            for (int i = 0; i < numberOfSatellites; i++)
            {
                var x = (random.NextDouble() * 40000000) - 20000000;
                var y = (random.NextDouble() * 40000000) - 20000000;
                var graphic = new Graphic { Geometry = new MapPoint(x, y) };
                graphics.Add(graphic);
            }

            return graphics;
        }

        private void GcButton_OnClick(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!removed)
            {
                LayoutRoot.Children.Remove(MyMap);
                removed = true;
            }
        }
    }
}

using ESRI.ArcGIS.Client.Local;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Controls;

namespace RuntimeMemoryLeak2
{
    public partial class MemoryLeak
    {
        private static readonly string _mpk = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"MapPackages\MapPackage.mpk");
        private static readonly string[] _mpkLayerNames = { "squares1", "squares2", "squares3" };

        public MemoryLeak()
        {
            InitializeComponent();
            this.NumberOfVertices.Loaded += NumberOfVertices_Loaded;
        }

        static void NumberOfVertices_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Add items to Combo Box for use
            int[] numberOfVertices = { 5, 5125, 51227 };
            ComboBox cbx = sender as ComboBox;
            cbx.ItemsSource = numberOfVertices;
            cbx.SelectedIndex = 0;
        }

        private void NumberOfVertices_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // When the ComboBox Selected Index is changed, display the feature with the selected number of Vertices.
            int index = this.NumberOfVertices.SelectedIndex;
            if (index < 0) return;

            ArcGISLocalFeatureLayer localFeatureLayer = new ArcGISLocalFeatureLayer(_mpk, _mpkLayerNames[index]);
            MyMap.Layers.Clear();
            MyMap.Layers.Add(localFeatureLayer);

            localFeatureLayer.Initialized += localFeatureLayer_Initialized;
        }

        private void localFeatureLayer_Initialized(object sender, EventArgs e)
        {
            MyMap.ZoomTo(MyMap.Layers[0].FullExtent);
        }
    }
}

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Color = System.Drawing.Color;
using Path = System.IO.Path;

namespace WPFMapViewer
{
	/// <summary>
	/// Interaction logic
	/// </summary>
    public partial class MapWindow : Window
	{
		AxMapControl _mapControl;
		AxToolbarControl _toolbarControl;
		AxTOCControl _tocControl;

		public MapWindow ()
		{
			InitializeComponent ();
            this.Loaded += new RoutedEventHandler(MapWindow_Loaded);
		}

        void MapWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateEngineControls();
            LoadMap();

            //ILayerFile layerFile = new LayerFileClass();
            //foreach (var lpk in new[] { MiscClass.Lpk1, MiscClass.Lpk0 })
            //{
            //    layerFile.Open(lpk);
            //    _mapControl.AddLayer(layerFile.Layer);
            //}

            _mapControl.LoadMxFile(MiscClass.Mxd, null, null);
        }

		// Create ArcGIS Engine Controls and set them to be child of each WindowsFormsHost elements
		void CreateEngineControls ()
		{
			//set Engine controls to the child of each hosts 
		    _mapControl = new AxMapControl();
			MapHost.Child = _mapControl;

			_toolbarControl = new AxToolbarControl ();
			ToolbarHost.Child = _toolbarControl;

			_tocControl = new AxTOCControl ();
			TocHost.Child = _tocControl;

            _mapControl.CreateControl();
            _toolbarControl.CreateControl();
            _tocControl.CreateControl();
		}

		private void LoadMap ()
		{
			//Buddy up controls
			_tocControl.SetBuddyControl (_mapControl);
			_toolbarControl.SetBuddyControl (_mapControl);

			//add command and tools to the toolbar
			_toolbarControl.AddItem ("esriControls.ControlsOpenDocCommand");
			_toolbarControl.AddItem ("esriControls.ControlsAddDataCommand");
			_toolbarControl.AddItem ("esriControls.ControlsSaveAsDocCommand");
			_toolbarControl.AddItem ("esriControls.ControlsMapNavigationToolbar");
			_toolbarControl.AddItem ("esriControls.ControlsMapIdentifyTool");
			
			//set controls' properties
			_toolbarControl.BackColor = Color.FromArgb (245, 245, 220);

			//wire up events
			_mapControl.OnMouseMove += mapControl_OnMouseMove;
		    _mapControl.OnDoubleClick += mapControl_OnDoubleClick;
		}

	    private void mapControl_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
	    {
	        MessageBox.Show("Detected Dbl Click...GP Will Start Soon. Messages at bottom of window.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

	        MyWindow.TextBlock1.Text = "Started Geoprocessing...";
	        TextBlock txtBlock = TextBlock1;
	        string outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Results");

	        if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

	        IFeatureClass pointsClass = MiscClass.GetLayer(_mapControl.Map, "World Cities").FeatureClass;
	        IFeatureLayer barrierLayer = MiscClass.GetLayer(_mapControl.Map, "State Plane Zones (NAD 27)");

	        try
	        {
                string splineRaster = MiscClass.ExecuteSplineWithBarriers(outputPath, "SplineResult", pointsClass, barrierLayer, "POP", ref txtBlock);
                IRasterLayer rasLayer = new RasterLayerClass();
                rasLayer.CreateFromFilePath(splineRaster);
                rasLayer.Name = "Spline Result";

                _mapControl.AddLayer(rasLayer);
                _mapControl.MoveLayerTo(0, _mapControl.LayerCount - 1);
                _mapControl.ActiveView.Refresh();
                _mapControl.ActiveView.ContentsChanged();
	            MyWindow.TextBlock1.Text = "Process complete...";
	        }
	        catch (Exception ex)
	        {
	            TextBlock1.Text = "Exception: " + ex.Message;
	        }
	    }

		private void mapControl_OnMouseMove (object sender, IMapControlEvents2_OnMouseMoveEvent e)
		{
			//TextBlock1.Text = " X,Y Coordinates on Map: " + string.Format ("{0}, {1}  {2}", e.mapX.ToString ("#######.##"), e.mapY.ToString ("#######.##"), _mapControl.MapUnits.ToString ().Substring (4));
		}

	    private void OpenMapButton_OnClick(object sender, RoutedEventArgs e)
	    {
            CreateEngineControls();
            LoadMap();

            //ILayerFile layerFile = new LayerFileClass();
            //foreach (var lpk in new[] { MiscClass.Lpk1, MiscClass.Lpk0 })
            //{
            //    layerFile.Open(lpk);
            //    _mapControl.AddLayer(layerFile.Layer);
            //}

            _mapControl.LoadMxFile(MiscClass.Mxd, null, null);
	    }
	}
}

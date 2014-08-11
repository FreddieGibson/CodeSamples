using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.SpatialAnalystTools;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace WPFMapViewer
{
    public class MiscClass
    {
        public const esriLicenseProductCode LicenseLevel = esriLicenseProductCode.esriLicenseProductCodeBasic;
        private static readonly string Src = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static readonly string Mxd = @"C:\Users\fred6410\Desktop\Updated_WPF\Updated_WPF\WPF_OpenMap\data\MapDocument.mxd";
        //public static readonly string Mxd = Path.Combine(Src, @"WPF_OpenMap\data\MapDocument.mxd");
        public static readonly string Lpk0 = Path.Combine(Src, @"data\World Cities.lpk");
        public static readonly string Lpk1 = Path.Combine(Src, @"data\USA State Plane Zones.lpk");

        private static readonly AoInitializeClass _aoInit;
        private bool _mapLoaded = false;
        private IMapDocument _mapDoc;

        public MiscClass()
        {
            
        }

        public static bool CheckOutLicense(esriLicenseExtensionCode extCode)
        {
            return _aoInit.CheckOutExtension(extCode) == esriLicenseStatus.esriLicenseCheckedOut;
        }

        public static bool CheckInLicense(esriLicenseExtensionCode extCode)
        {
            _aoInit.CheckInExtension(extCode);
            return true;
        }

         public IMap LoadFromFile(String mapPath)
        {
            IMapDocument mapDocument = new MapDocumentClass();
            _mapDoc.Open(mapPath);

            return LoadMap(mapDocument);
        }

        private IMap LoadMap(IMapDocument mapDocument)
        {
            if (_mapLoaded) return null;
            _mapDoc = mapDocument;

            IMap map = mapDocument.Map[0];
            
            _mapLoaded = true;
            return map;
        }

        public string GetLayerNames(IMap map)
        {
            StringBuilder sb = new StringBuilder();
            IEnumLayer layers = map.Layers[null, true];

            ILayer layer;
            int i = 0;

            while ((layer = layers.Next()) != null)
            {
                sb.AppendLine(string.Format("Index: {0} Name: {1}", i, layer.Name));
                i++;
            }

            return sb.ToString();
        }

        public static IFeatureLayer GetLayer(IMap map, string layerName)
        {
            IEnumLayer layers = map.Layers[null, true];
            ILayer layer;

            while ((layer = layers.Next()) != null)
            {
                if (layer is FeatureLayer && layer.Name.Equals(layerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return layer as IFeatureLayer;
                }
            }

            return null;
        }

        public static string ExecuteSplineWithBarriers(string path, string layerName, IFeatureClass dataPoints, IFeatureLayer barrierLayer, string zValueColumn, ref TextBlock textBlock)
        {
            StringBuilder buffer = new StringBuilder();
            Geoprocessor gp = new Geoprocessor { OverwriteOutput = true, AddOutputsToMap = true };
            gp.SetEnvironmentValue("Extent", "MAXOF");
            gp.SetEnvironmentValue("scratchWorkspace", path);

            string tempPath = Path.Combine(path, "raw");
            string outputPath = Path.Combine(path, layerName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            IEnvelope extents = ((IGeoDataset) dataPoints).Extent;
            double cellsize = Math.Min(extents.Height, extents.Width);
            cellsize /= 250.0;

            try
            {
                SplineWithBarriers splineWithBarriers = new SplineWithBarriers
                {
                    Input_point_features = dataPoints,
                    Z_value_field = zValueColumn,
                    Output_raster = outputPath,
                    Input_barrier_features = barrierLayer,
                    Smoothing_Factor = 0,
                    Output_cell_size = 0.531968004127 //cellsize
                };

                try
                {
                    textBlock.Text = "Calling Spline with Barriers GP Tool...";
                    IGeoProcessorResult2 result = gp.Execute(splineWithBarriers, null) as IGeoProcessorResult2;
                    return result.ReturnValue.ToString();
                }
                catch (COMException ex)
                {
                    Debug.WriteLine(ex.ErrorCode + "\n" + ex.Message);
                    textBlock.Text = "Exception thrown";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    textBlock.Text = "Exception thrown";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }
    }
}

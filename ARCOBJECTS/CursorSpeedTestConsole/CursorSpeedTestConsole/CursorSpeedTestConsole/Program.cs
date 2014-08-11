using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Cursor = ESRI.ArcGIS.Geodatabase.Cursor;
using Path = System.IO.Path;

namespace CursorSpeedTestConsole
{
    class Program
    {
        private static readonly LicenseInitializer _aoLicenseInitializer = new LicenseInitializer();
        private static readonly Stopwatch _stopWatch = new Stopwatch();
        private static readonly string _src = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string Txt =  "CursorLog_{0:hhmmsstt}.txt";
        private static StreamWriter _writer;

        private static readonly Double[] _lon = { -117.040847, -117.040847, -117.040847, -117.040847, -117.040847, -117.162903, -117.179687, -117.180377, -117.179, -117.179, -117.179468, -117.179603, -117.179656, -117.179656, -117.179656, -117.179656, -117.169967, -117.179656, -117.200649, -117.162903, -117.162903, -117.162903, -117.162903, -117.184089, -117.089873, -117.252433, -117.030779, -117.155563 };
        private static readonly Double[] _lat = { 32.953804, 32.954196, 32.954196, 32.954196, 32.954196, 32.715805, 32.898426, 32.904686, 32.899284, 32.899284, 32.899018, 32.898849, 32.898708, 32.898708, 32.898708, 32.898708, 32.922763, 32.898708, 32.889838, 32.715805, 32.715805, 32.715805, 32.715805, 32.853754, 32.966804, 32.847858, 33.018585, 32.711521 };
        private const Double Tolerance = .001;

        private static Dictionary<int, int> _pntCount;
        private const bool DebugMode = true;
        private const int Repeat = 2;

        [STAThread]
        static void Main()
        {
            if (!AppIsLicensed()) return;

            if (!DebugMode) Console.WriteLine("Watch for dialog to select *.shp file.");
            string shp = (!DebugMode) ? MiscClass.SelectShapefile() : Path.Combine(_src, @"data\streets.shp");

            if (!DebugMode)
            {
                double xCoord, yCoord, snapTolerance;

                while (true)
                {
                    Console.WriteLine("\nInput a valid x coordinate:");
                    if (double.TryParse(Console.ReadLine(), out xCoord))
                        break;
                }

                while (true)
                {
                    Console.WriteLine("\nInput a valid y coordinate:");
                    if (double.TryParse(Console.ReadLine(), out yCoord))
                        break;
                }

                while (true)
                {
                    Console.WriteLine("\nInput a valid snap tolerance:");
                    if (double.TryParse(Console.ReadLine(), out snapTolerance))
                        break;
                }

                Console.WriteLine();
                SnapPointToClosestPolyline(shp, xCoord, yCoord, snapTolerance);
                
                Console.WriteLine("Press Enter to Exit...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Preparing data...");
                _pntCount = GetFeatureVertexCount(shp);
                
                string txtPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Cursor LogFiles");

                if (!Directory.Exists(txtPath)) Directory.CreateDirectory(txtPath);

                for (int h = 0; h < Repeat; h++)
                {

                    using (_writer = new StreamWriter(string.Format(Path.Combine(txtPath, Txt), DateTime.Now), false))
                    {
                        _writer.WriteLine("Shapefile: {0}", shp);

                        for (int i = 0; i < _lon.Length; i++)
                        {
                            Console.WriteLine("Processing feature {0:00} of {1:00}...", i + 1, _lon.Length);
                            SnapPointToClosestPolylineUserMod(shp, _lon[i], _lat[i], Tolerance);
                        }
                    }

                    if (h == Repeat - 1) continue;

                    Console.WriteLine("Waiting 10 seconds before next run...\n\n");
                    Thread.Sleep(TimeSpan.FromSeconds(10.0));
                }

                CombineLogFiles(txtPath);
            }

            _aoLicenseInitializer.ShutdownApplication();
        }

        static void CombineLogFiles(string logPath)
        {
            string[] logFiles = Directory.GetFiles(logPath + "\\");
            string master = Path.Combine(Path.GetDirectoryName(logPath), "MasterLog.txt");
            List<string[]> lines = logFiles.Select(log => File.ReadAllLines(Path.Combine(logPath, log))).ToList();

            using (_writer = new StreamWriter(master, false))
            {
                for (int i = 0; i < logFiles[0].Length; i++)
                {
                    for (int j = 0; j < logFiles.Length; j++)
                    {
                        _writer.WriteLine(lines[j][i]);
                    }

                    _writer.WriteLine("... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ... ...\n");
                }
            }

            Process.Start(master);
        }

        static void DisplayTime(Stopwatch sw, string message)
        {
            Console.WriteLine("{0:000000} ms | Task: {1}", sw.ElapsedMilliseconds, message);
        }

        static Dictionary<int, int> GetFeatureVertexCount(string fcPath)
        {
            Dictionary<int, int> pntCntDict = new Dictionary<int, int>();
            
            Geoprocessor gp = new Geoprocessor {OverwriteOutput = true};

            try
            {
                AddField addField = new AddField { in_table = fcPath, field_name = "PointCount", field_type = "LONG" };
                gp.Execute(addField, null);

                CalculateField calcField = new CalculateField
                {
                    in_table = fcPath,
                    field = "PointCount",
                    expression = "!Shape!.pointcount",
                    expression_type = "PYTHON"
                };

                gp.Execute(calcField, null);
            }
            catch (COMException)
            {
                object sev = 2;
                Console.WriteLine(gp.GetMessages(ref sev));
            }
            

            IGPUtilities gpUtilities = new GPUtilitiesClass();
            IFeatureClass featClass = gpUtilities.OpenFeatureClassFromString(fcPath);
            int fieldIndex = featClass.FindField("PointCount");
            IQueryFilter qf = new QueryFilterClass {SubFields = "PointCount"};
            ICursor cursor = featClass.Search(qf, true) as Cursor;

            IRow row;

            while ((row = cursor.NextRow()) != null)
            {
                pntCntDict.Add(row.OID, (int) row.Value[fieldIndex]);
            }

            return pntCntDict;
        }

        static void SnapPointToClosestPolyline(string shpPath, double x, double y, double tolerance)
        {
            string path = Path.GetDirectoryName(shpPath);
            string name = Path.GetFileNameWithoutExtension(shpPath);

            _stopWatch.Restart();
            IWorkspaceFactory wsf = new ShapefileWorkspaceFactoryClass();
            IWorkspace ws = wsf.OpenFromFile(path, 0);
            IFeatureClass fClass = ((IFeatureWorkspace) ws).OpenFeatureClass(name);
            _stopWatch.Stop();
            DisplayTime(_stopWatch, string.Format("Opening {0}", Path.GetFileName(shpPath)));

            ISpatialReference sRef = ((IGeoDataset) fClass).SpatialReference;
            IPoint point = new Point {X = x, Y = y, SpatialReference = sRef};
            IFeatureCursor cursor = null;

            try
            {
                IGeometry buffer = ((ITopologicalOperator) point).Buffer(tolerance);
                ISpatialFilter filter = new SpatialFilter
                {
                    Geometry = buffer,
                    GeometryField = fClass.ShapeFieldName,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };

                _stopWatch.Restart();
                cursor = fClass.Search(filter, true);
                _stopWatch.Stop();
                DisplayTime(_stopWatch, "Hydrating Cursor");

                _stopWatch.Restart();
                IFeature feature = cursor.NextFeature();
                _stopWatch.Stop();

                DisplayTime(_stopWatch, string.Format("...Cursor for 1st record. [Null Row: {0}]",  feature == null));
                
                IPoint outPoint = new PointClass();
                double distanceAlongurve = 0;
                double distanceFromCurve = 0;
                IPolyline6 polyline = (IPolyline6) feature.Shape;
                bool rightSide = false;

                polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, point, true, outPoint, ref distanceAlongurve, ref distanceFromCurve, ref rightSide);
                Marshal.FinalReleaseComObject(outPoint);
                Marshal.FinalReleaseComObject(polyline);

                _stopWatch.Restart();
                feature = cursor.NextFeature();
                _stopWatch.Stop();

                DisplayTime(_stopWatch, string.Format("...Cursor for eof. [Null Row: {0}]", feature == null));
                
            }
            catch (Exception ex) { Console.WriteLine("Exception: {0}", ex.Message); }
            Marshal.FinalReleaseComObject(cursor);
        }

        static void SnapPointToClosestPolylineUserMod(string shpPath, double x, double y, double tolerance)
        {
            string path = Path.GetDirectoryName(shpPath);
            string name = Path.GetFileNameWithoutExtension(shpPath);

            const string fmt1 = "  Latitude : {0,13}";
            const string fmt2 = "  Longitude: {0,13}";
            const string fmt3 = "    ...Open Feature Class:   {0:0000} ms";
            const string fmt4 = "    ...Get Polygon:          {0:0000} ms";
            const string fmt5 = "    ...Search Feature Class: {0:0000} ms";
            const string fmt6 = "    ...Next Feature:         {0:0000} ms [OID: {1,8} | Vertices: {2,10}]";
            const string fmt7 = "    ...Done.";
            const int noOID = -1;
            const string fmt8 = "0000000";

            _writer.WriteLine(fmt1, x.ToString("000.0000000"));
            _writer.WriteLine(fmt2, y.ToString("000.0000000"));

            _stopWatch.Restart();
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesFile.ShapefileWorkspaceFactory");
            IWorkspaceFactory wsf = (IWorkspaceFactory) Activator.CreateInstance(factoryType);
            IWorkspace ws = wsf.OpenFromFile(path, 0);
            IFeatureClass fClass = ((IFeatureWorkspace)ws).OpenFeatureClass(name);
            _stopWatch.Stop();
            
            _writer.WriteLine(fmt3, _stopWatch.ElapsedMilliseconds);

            ISpatialReference sRef = ((IGeoDataset)fClass).SpatialReference;
            IPoint point = new Point { X = x, Y = y, SpatialReference = sRef };
            IFeatureCursor cursor = null;

            try
            {
                _stopWatch.Restart();
                IGeometry buffer = ((ITopologicalOperator)point).Buffer(tolerance);
                _stopWatch.Stop();

                _writer.WriteLine(fmt4, _stopWatch.ElapsedMilliseconds);

                ISpatialFilter filter = new SpatialFilter
                {
                    Geometry = buffer,
                    GeometryField = fClass.ShapeFieldName,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };

                _stopWatch.Restart();
                cursor = fClass.Search(filter, true);
                _stopWatch.Stop();
                _writer.WriteLine(fmt5, _stopWatch.ElapsedMilliseconds);

                _stopWatch.Restart();
                IFeature feature = cursor.NextFeature();
                _stopWatch.Stop();

                _writer.WriteLine(fmt6, _stopWatch.ElapsedMilliseconds, feature != null ? feature.OID.ToString(fmt8) : noOID.ToString(fmt8),
                    feature != null ? _pntCount[feature.OID].ToString(fmt8) : noOID.ToString(fmt8));

                while (feature != null)
                {
                    IPoint outPoint = new PointClass();
                    double distanceAlongurve = 0;
                    double distanceFromCurve = 0;
                    IPolyline6 polyline = (IPolyline6) feature.Shape;
                    bool rightSide = false;

                    polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, point, true, outPoint, ref distanceAlongurve, ref distanceFromCurve, ref rightSide);
                    Marshal.FinalReleaseComObject(outPoint);
                    Marshal.FinalReleaseComObject(polyline);

                    _stopWatch.Restart();
                    feature = cursor.NextFeature();
                    _stopWatch.Stop();

                    _writer.WriteLine(fmt6, _stopWatch.ElapsedMilliseconds, feature != null ? feature.OID.ToString(fmt8) : noOID.ToString(fmt8),
                    feature != null ? _pntCount[feature.OID].ToString(fmt8) : noOID.ToString(fmt8));
                }

                _writer.WriteLine(fmt7);

            }
            catch (Exception ex) { Console.WriteLine("Exception: {0}", ex.Message); }
            Marshal.FinalReleaseComObject(cursor);
        }

        private static bool AppIsLicensed()
        {
            if (_aoLicenseInitializer.InitializeApplication(
                    new [] {esriLicenseProductCode.esriLicenseProductCodeAdvanced},
                    new esriLicenseExtensionCode[] {})) return true;

            Console.WriteLine(_aoLicenseInitializer.LicenseMessage());
            Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.");
            _aoLicenseInitializer.ShutdownApplication();
            return false;
        }
    }
}

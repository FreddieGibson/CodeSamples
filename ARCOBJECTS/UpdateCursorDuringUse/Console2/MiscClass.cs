using System.Collections.Generic;
using System.Globalization;
using ESRI.ArcGIS;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Console2
{
    class MiscClass
    {
        private static readonly string Data = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Data";
        private static readonly string[] Mpks = { System.IO.Path.Combine(Data, "POINTS_010K.mpk"),
                                                  System.IO.Path.Combine(Data, "POINTS_050K.mpk"),
                                                  System.IO.Path.Combine(Data, "POINTS_100K.mpk") };
        public const string FieldA = "TCount";
        public const string FieldB = "ICount";
        public const string Percent = "000.00%";
        public const string Bkspace = "\b\b\b\b\b\b\b";


        public static IApplication GetActiveArcMapSession()
        {
            KillProcess("ArcMap");

            Console.Write("Enter a number 1-3 and press enter. (1=10K 2=50K 3=100K) ");
            string line = Console.ReadLine();

            string mpk;
            switch (int.Parse(line[line.Length - 1].ToString(CultureInfo.InvariantCulture)))
            {
                case 2:
                    mpk = Mpks[1];
                    break;
                case 3:
                    mpk = Mpks[2];
                    break;
                default:
                    mpk = Mpks[0];
                    break;
            }

            StartMpkInArcMapSession(mpk);
            IAppROT pAppRot = new AppROTClass();
            try
            {
                for (int i = 0; i < pAppRot.Count; i++)
                {
                    AppRef pAppRef = pAppRot.Item[i];
                    if (pAppRef is IMxApplication) return pAppRef;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { Marshal.FinalReleaseComObject(pAppRot); }
            return null;
        }

        public static int GetCountUsingGP(IFeatureClass featureClass)
        {
            Geoprocessor gp = new Geoprocessor();
            GetCount getCount = new GetCount { in_rows = featureClass };
            object returnValue = null;
            try
            {
                IGeoProcessorResult2 pResult = (IGeoProcessorResult2)gp.Execute(getCount, null);
                returnValue = pResult.ReturnValue;
            }
            catch (Exception ex) { Messages(ex, ref gp); }
            return ((returnValue) is int) ? (int)returnValue : -1;
        }

        public static void KillProcess(string processName)
        {
            foreach (var exe in Process.GetProcessesByName(processName)) { exe.Kill(); }
        }

        public static ILayer OpenLayerPackageFromFile(string lpkPath)
        {
            LayerFileClass pLayerFile = new LayerFileClass();
            pLayerFile.Open(lpkPath);
            return pLayerFile.Layer;
        }

        public static void Messages(Exception ex, ref Geoprocessor gp)
        {
            Console.WriteLine("EXCEPTION:\n... " + ex.Message);
            if (gp.MessageCount <= 0) return;
            Console.WriteLine("GP MESSAGE:");
            for (var i = 1; i < gp.MessageCount; i++) { Console.WriteLine("... " + gp.GetMessage(i)); }
            Console.WriteLine();
        }

        public static IWorkspace OpenGDBWorkspaceFromFile(string workspacePath)
        {
            /* References
             * using ESRI.ArcGIS.Geodatabase
             * 
             * Dependencies:
             * n/a
             * 
             * Sample Usage:
             * IWorkspace pWorkspace = OpenGDBWorkspaceFromFile(@"C:\Temp\connection.sde")
             */

            string extension = System.IO.Path.GetExtension(workspacePath);
            string programID = null;
            switch (extension)
            {
                case ".gdb":
                    programID = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
                    break;
                case ".mdb":
                    programID = "esriDataSourcesGDB.AccessWorkspaceFactory";
                    break;
                case ".sde":
                    programID = "esriDataSourcesGDB.SdeWorkspaceFactory";
                    break;
            }

            if (programID == null) return null;

            Type factoryType = Type.GetTypeFromProgID(programID);
            IWorkspaceFactory pWorkspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return pWorkspaceFactory.OpenFromFile(workspacePath, 0);
        }

        public static void SetFieldToNull(IFeatureClass featureClass, string fieldName)
        {
            Geoprocessor gp = new Geoprocessor();
            CalculateField calculateField = new CalculateField
            {
                in_table = featureClass,
                field = fieldName,
                expression = "NULL",
                expression_type = "VB"
            };

            try { gp.Execute(calculateField, null); }
            catch (Exception ex) { Messages(ex, ref gp); }
        }

        public static void StartMpkInArcMapSession(string mapPackage)
        {
            IEnumerable<RuntimeInfo> runtimes = RuntimeManager.InstalledRuntimes;
            if (runtimes.All(runtime => runtime.Product.ToString() != "Desktop")) return;
            ProcessStartInfo startInfo = new ProcessStartInfo { FileName = mapPackage };
            Process.Start(startInfo);
            Console.WriteLine("Press enter after ArcMap session has started completely.");
            Console.ReadLine();
        }
    }
}

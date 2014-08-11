using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using System.Runtime.InteropServices;
using ESRI.ArcGIS;
//using ESRI.ArcGIS.ArcMapUI;
//using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataManagementTools;
//using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace Console3
{
    class MiscClass
    {
        private static readonly String Data = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Data";
        public static string[] Feats = { System.IO.Path.Combine(Data, @"Data.gdb\POINTS_010K"), 
                                         System.IO.Path.Combine(Data, @"Data.gdb\POINTS_050K"), 
                                         System.IO.Path.Combine(Data, @"Data.gdb\POINTS_100K") };
        public const string FieldA = "TCount";
        public const string FieldB = "ICount";
        public const string Percent = "000.00%";
        public const string Bkspace = "\b\b\b\b\b\b\b";


        //public static IApplication GetActiveArcMapSession()
        //{
        //    KillProcess("ArcMap");
        //    StartMpkInArcMapSession(Mpk);
        //    IAppROT pAppRot = new AppROTClass();
        //    try
        //    {
        //        for (int i = 0; i < pAppRot.Count; i++)
        //        {
        //            AppRef pAppRef = pAppRot.Item[i];
        //            if (pAppRef is IMxApplication) return pAppRef;
        //        }
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); }
        //    finally { Marshal.FinalReleaseComObject(pAppRot); }
        //    return null;
        //}

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

        //public static ILayer OpenLayerPackageFromFile(string lpkPath)
        //{
        //    LayerFileClass pLayerFile = new LayerFileClass();
        //    pLayerFile.Open(lpkPath);
        //    return pLayerFile.Layer;
        //}

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

        //public static void StartMpkInArcMapSession(string mapPackage)
        //{
        //    IEnumerable<RuntimeInfo> runtimes = RuntimeManager.InstalledRuntimes;
        //    if (runtimes.All(runtime => runtime.Product.ToString() != "Desktop")) return;
        //    ProcessStartInfo startInfo = new ProcessStartInfo { FileName = mapPackage };
        //    Process.Start(startInfo);
        //    Console.WriteLine("Press enter after ArcMap session has started completely.");
        //    Console.ReadLine();
        //}

        public static Process StartArcGISDesktopSession(string productName)
        {
            IEnumerable<RuntimeInfo> runtimes = RuntimeManager.InstalledRuntimes;
            string productPath = (from runtime in runtimes where runtime.Product.ToString() == "Desktop" select runtime.Path).FirstOrDefault();

            if (productPath == null) return null;
            string exe;
            switch (productName)
            {
                case "ArcCatalog":
                    exe = System.IO.Path.Combine(productPath, @"bin\ArcCatalog.exe");
                    break;
                case "ArcScene":
                    exe = System.IO.Path.Combine(productPath, @"bin\ArcScene.exe");
                    break;
                case "ArcGlobe":
                    exe = System.IO.Path.Combine(productPath, @"bin\ArcGlobe.exe");
                    break;
                default:
                    exe = System.IO.Path.Combine(productPath, @"bin\ArcMap.exe");
                    break;
            }

            Process process = Process.Start(exe);
            return process;
        }


    }
}

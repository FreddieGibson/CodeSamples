using System.Collections.Generic;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;

namespace EngineConsoleSimpleArcObjectsRepro
{
    class Program
    {
        private static readonly LicenseInitializer _aoLicenseInitializer = new LicenseInitializer();
    
        [STAThread]
        static void Main()
        {
            if (!AppIsLicensed()) return;

            ReproCase();

            Console.WriteLine("\n\nPress enter to exit.");
            Console.ReadLine();

            _aoLicenseInitializer.ShutdownApplication();
        }

        private static void ReproCase()
        {
            Geoprocessor gp = new Geoprocessor { OverwriteOutput = true }; // Instantiate the geoprocessor using the managed assembly.

            string tbx = GetArcPadExePath().Replace("ArcPad.exe", @"DesktopTools10.0\Toolboxes\ArcPad Tools.tbx"); // Get path to ArcPadTools toolbox.
            string gdb = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"data\Riverside.gdb");
            string axf = gdb.Replace(".gdb", ".axf");
            string tool = "ArcPadCheckout_ArcPad";
            string feat = string.Join(";", GetFeatures(OpenGeodatabaseWorkspace(gdb), ref gp)); // Get the features in the included gdb.

            Console.WriteLine("Will attempt to run the {0} tool...\n", tool);
            
            try
            {
                gp.AddToolbox(tbx); // Add the toolbox
                
                IVariantArray parameters = new VarArrayClass();
                parameters.Add(feat);  // Features
                parameters.Add("");    // Schema Only
                parameters.Add("");    // Password
                parameters.Add("");    // Encrypt
                parameters.Add(axf);   // Axf

                if (File.Exists(axf)) File.Delete(axf); // Delete the axf if it exists. Tool will not allow you to overwrite
                var result = (IGeoProcessorResult2) gp.Execute(tool, parameters, null); // Execute the gp tool
                Messages(result); // Display the GP Messages for the successful tool.
            }
            catch (Exception e) { Messages(e, ref gp); } // Display the Exception/GP Messages for the failing tool
            finally { gp.RemoveToolbox(tbx); }
        }

        #region Ignore this area. This is just supplemental code.
        private static bool AppIsLicensed()
        {
            if (_aoLicenseInitializer.InitializeApplication(
                    new esriLicenseProductCode[] {esriLicenseProductCode.esriLicenseProductCodeAdvanced},
                    new esriLicenseExtensionCode[] {})) return true;
            Console.WriteLine(_aoLicenseInitializer.LicenseMessage());
            Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.");
            _aoLicenseInitializer.ShutdownApplication();
            return false;
        }

        private static string GetArcPadExePath()
        {
            try
            {
                string installDirectory;
                if (Environment.Is64BitOperatingSystem)
                {
                    Registry.LocalMachine.OpenSubKey(@"Software\Wow6432Node\ESRI\ArcPad");
                    installDirectory =
                        Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Wow6432Node\ESRI\ArcPad", "InstallDir", String.Empty)
                            .ToString().Replace("Program Files", "Program Files (x86)");
                }
                else
                {
                    Registry.LocalMachine.OpenSubKey(@"Software\ESRI\ArcPad");
                    installDirectory =
                        Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\ESRI\ArcPad", "InstallDir",
                            String.Empty).ToString();
                }

                return Path.Combine(installDirectory, "ArcPad.exe");
            }
            catch (Exception) { return string.Empty; }
            finally { Registry.LocalMachine.Close(); }
        }

        public static String[] GetFeatures(IWorkspace workspace, ref Geoprocessor gp, String wildCard = "", String featureType = "")
        {
            try
            {
                gp.SetEnvironmentValue("workspace", workspace.PathName);
                wildCard = (string.IsNullOrEmpty(wildCard) ? wildCard : wildCard + "*");
                var features = gp.ListFeatureClasses(wildCard, featureType, "");

                String feature;
                var featList = new List<String>();
                while ((feature = features.Next()) != string.Empty) { featList.Add(Path.Combine(workspace.PathName, feature)); }

                return featList.ToArray();
            }
            catch (Exception) { return GetRootFeatures(workspace); }
        }

        public static String[] GetRootFeatures(IWorkspace workspace)
        {
            var features = new List<string>();
            IDataset pDataset;

            var pEnumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureClass];
            pEnumDataset.Reset();

            while ((pDataset = pEnumDataset.Next()) != null)
            {
                features.Add(Path.Combine(workspace.PathName, pDataset.BrowseName));
            }

            return features.ToArray();
        }

        public static void Messages(IGeoProcessorResult2 result)
        {
            if (result.MessageCount <= 0) return;
            for (var i = 0; i < result.MessageCount; i++) { Console.WriteLine(result.GetMessage(i)); }
        }

        public static void Messages(Exception ex, ref Geoprocessor gp)
        {
            Console.WriteLine("..EXCEPTION: " + ex.Message);
            if (gp.MessageCount <= 0) return;
            for (var i = 0; i < gp.MessageCount; i++) { Console.WriteLine(".." + gp.GetMessage(i)); }
        }

        public static IWorkspace OpenGeodatabaseWorkspace(string geodatabase)
        {
            const string progId = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
            Type factoryType = Type.GetTypeFromProgID(progId);
            IWorkspaceFactory pWorkspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return pWorkspaceFactory.OpenFromFile(geodatabase, 0);
        }
        #endregion
    }
}

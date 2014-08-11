using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace WritePythonScripts
{
    class Program
    {
        private static readonly LicenseInitializer _aoLicenseInitializer = new LicenseInitializer();

        [STAThread]
        static void Main()
        {
            if (!AppIsLicensed()) return;

            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ArcPad_Python");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            WritePythonCode(folder, true);
            WritePythonCode(folder, false);

            Process.Start(new ProcessStartInfo {FileName = folder, UseShellExecute = true, Verb = "open"});

            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            _aoLicenseInitializer.ShutdownApplication();
        }

        static void WritePythonCode(string folder, bool version10X)
        {
            Geoprocessor gp = new Geoprocessor { OverwriteOutput = true }; // Instantiate the geoprocessor using the managed assembly.

            string tbx = GetArcPadExePath().Replace("ArcPad.exe", @"DesktopTools10.0\Toolboxes\ArcPad Tools.tbx"); // Get path to ArcPadTools toolbox.
            string gdb = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"data\Riverside.gdb");
            string axf = gdb.Replace(".gdb", ".axf");
            string feat = string.Join(";", GetFeatures(OpenGeodatabaseWorkspace(gdb), ref gp));
            
            string pyPath = Path.Combine(folder, "ArcPad_" + ((version10X) ? "arcpy.py" : "arcgisscripting.py"));

            string[] lines = 
            {
 (version10X) ? "import arcpy,os\n" 
              : "import arcgisscripting,os\narcpy = arcgisscripting.create(9.3)\n",
                "try:",
 (version10X) ? "   print(r'Importing toolbox %s' % r'{0}')"
              : "   print(r'Adding toolbox %s' % r'{0}')",
 (version10X) ? "   arcpy.ImportToolbox(r'{0}')" 
              : "   arcpy.AddToolbox(r'{0}')",
                "   try:",
                "      arcpy.ArcPadCheckout_ArcPad(r'{0}', '{1}', '{2}', '{3}', r'{4}')",
                "      print(arcpy.GetMessages(0))",
                "   except arcpy.ExecuteError: print(arcpy.GetMessages(2))",
                "   except: print('Generic Exception')",
                "   finally: arcpy.RemoveToolbox(r'{0}')",
 (version10X) ? "except: print('Error Importing Toolbox')\n"
              : "except: print('Error Adding Toolbox')\n",
            };

            lines[2] = string.Format(lines[2], tbx);
            lines[3] = string.Format(lines[3], tbx);
            lines[5] = string.Format(lines[5], string.Join(";", feat), "", "", "", axf);
            lines[9] = string.Format(lines[9], tbx);

            using (StreamWriter writer = new StreamWriter(pyPath)) { foreach (string line in lines) { writer.WriteLine(line); } }
        }

        #region Ignore this area. This is just supplemental code.
        private static bool AppIsLicensed()
        {
            if (_aoLicenseInitializer.InitializeApplication(
                    new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeAdvanced },
                    new esriLicenseExtensionCode[] { })) return true;
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

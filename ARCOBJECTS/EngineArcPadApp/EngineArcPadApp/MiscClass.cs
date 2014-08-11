using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Path = System.IO.Path;

namespace EngineArcPadApp
{
    class MiscClass
    {
        public static string ArcPadExePath;
        public static string ArcPadTbxPath;
        public const esriLicenseProductCode ProductCode = esriLicenseProductCode.esriLicenseProductCodeAdvanced;
        public const ProductCode BindingProductCode = ESRI.ArcGIS.ProductCode.EngineOrDesktop;

        public static readonly string Data = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");

        public static bool CheckForArcPadTools(Geoprocessor gp, bool finalCheck = false)
        {
            while (true)
            {
                string tool;
                List<string> tools = new List<string>();
                IGpEnumList toolCollection = gp.ListToolboxes("arc*");

                while ((tool = toolCollection.Next()) != null)
                {
                    tools.Add(tool);
                }

                if (!tools.Contains("ArcPad Tools(ArcPad)"))
                {
                    if (!finalCheck)
                    {
                        gp.AddToolbox(ArcPadTbxPath);
                        finalCheck = true;
                        continue;
                    }
                    else
                    {
                        throw new Exception("Missing ArcPad Tools.");
                    }
                }
                break;
            }

            return true;
        }

        public static bool CheckForArcPadTools(IGeoProcessor2 gp, bool finalCheck = false)
        {
            while (true)
            {
                string tool;
                List<string> tools = new List<string>();
                IGpEnumList toolCollection = gp.ListToolboxes("arc*");

                while ((tool = toolCollection.Next()) != null)
                {
                    tools.Add(tool);
                }

                if (!tools.Contains("ArcPad Tools(ArcPad)"))
                {
                    if (!finalCheck)
                    {
                        gp.AddToolbox(ArcPadTbxPath);
                        finalCheck = true;
                        continue;
                    }
                    else
                    {
                        throw new Exception("Missing ArcPad Tools.");
                    }
                }
                break;
            }

            return true;
        }

        public static string CreateCheckoutScript(String[] features, String axf, String schemaOnly = "#", String password = "#", String encrypt = "#", bool version10X = true)
        {
            string pyPath = Path.Combine(Data, "arcpad_test.py");

            string[] lines = 
            {
 (version10X) ? "import arcpy,os\n" 
              : "import arcgisscripting,os\narcpy = arcgisscripting.create(9.3)\n",
                "def SimplifyPath(path, length):",
                "   parts = str.split(path, os.sep); parts.reverse()",
                "   xPath = [parts[i] for i in range(length)]; xPath.reverse()",
                "   return r'..' + os.sep + os.sep.join(xPath)\n",
                "def main():",
                "   try:",
 (version10X) ? "      print(r'Importing toolbox %s' % SimplifyPath(r'{0}', 4))"
              : "      print(r'Adding toolbox %s' % SimplifyPath(r'{0}', 4))",
 (version10X) ? "      #arcpy.ImportToolbox(r'{0}')" 
              : "      arcpy.AddToolbox(r'{0}')",
                "      try:",
                "         arcpy.ArcPadCheckout_ArcPad(r'{0}', '{1}', '{2}', '{3}', r'{4}')",
                "         print(arcpy.GetMessages(0))",
                "      except arcpy.ExecuteError: print(arcpy.GetMessages(2))",
                "      except: print('Generic Exception')",
                "      finally: arcpy.RemoveToolbox(r'{0}')",
 (version10X) ? "   except: print('Error Importing Toolbox')\n"
              : "   except: print('Error Adding Toolbox')\n",
                "if __name__ == '__main__':\n   main()"
            };

            lines[7] = string.Format(lines[7], ArcPadTbxPath);
            lines[8] = string.Format(lines[8], ArcPadTbxPath);
            lines[10] = string.Format(lines[10], string.Join(";", features), schemaOnly, password, encrypt, axf);
            lines[14] = string.Format(lines[14], ArcPadTbxPath);

            using (StreamWriter writer = new StreamWriter(pyPath)) { foreach (string line in lines) { writer.WriteLine(line); } }
            return pyPath;
        }

        public static string CreateCheckinScript(string axf, string password = "#", string features = "#", bool version10X = true)
        {
            string pyPath = Path.Combine(Data, "arcpad_test.py");
            string[] lines = 
            {
 (version10X) ? "import arcpy,os\n" 
              : "import arcgisscripting,os\narcpy = arcgisscripting.create(9.3)\n",
                "def SimplifyPath(path, length):",
                "   parts = str.split(path, os.sep); parts.reverse()",
                "   xPath = [parts[i] for i in range(length)]; xPath.reverse()",
                "   return r'..' + os.sep + os.sep.join(xPath)\n",
                "def main():",
                "   try:",
 (version10X) ? "      print(r'Importing toolbox %s' % SimplifyPath(r'{0}', 4))" 
              : "      print(r'Adding toolbox %s' % SimplifyPath(r'{0}', 4))",
 (version10X) ? "      #arcpy.ImportToolbox(r'{0}')" 
              : "      arcpy.AddToolbox(r'{0}')",
                "      try:",
                "         arcpy.ArcPadCheckin_ArcPad(r'{0}', '{1}', r'{2}')",
                "         print(arcpy.GetMessages(0))",
                "      except arcpy.ExecuteError: print(arcpy.na GetMessages(2))",
                "      except: print('Generic Exception')",
                "      finally: arcpy.RemoveToolbox(r'{0}')",
 (version10X) ? "   except: print('Error Importing Toolbox')\n"
              : "   except: print('Error Adding Toolbox')\n",
                "if __name__ == '__main__':\n   main()"
            };

            lines[7] = string.Format(lines[7], ArcPadTbxPath);
            lines[8] = string.Format(lines[8], ArcPadTbxPath);
            lines[10] = string.Format(lines[10], axf, password, features);
            lines[14] = string.Format(lines[14], ArcPadTbxPath);

            using (StreamWriter writer = new StreamWriter(pyPath)) { foreach (string line in lines) { writer.WriteLine(line); } }
            return pyPath;
        }

        public static string ExecuteCommand(string arguments)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = GetPythonInstallPath(),
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = arguments
                };

                Process process = new Process { StartInfo = startInfo };
                process.Start();

                string result = process.StandardOutput.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
            catch (Exception)
            {
                return string.Empty;
            }
            finally
            {
                Registry.LocalMachine.Close();
            }
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

        public static String[] GetFeatures(IWorkspace workspace, ref IGeoProcessor2 gp, String wildCard = "", String featureType = "")
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

        private static string GetPythonInstallPath()
        {
            RuntimeInfo activeRuntimeInfo = RuntimeManager.ActiveRuntime;
            if (activeRuntimeInfo.Version.Contains("10.2"))
                return @"C:\Python27\ArcGIS10.2\python.exe";
            else if (activeRuntimeInfo.Version.Contains("10.1"))
                return @"C:\Python27\ArcGIS10.1\python.exe";
            else if (activeRuntimeInfo.Version.Contains("10.0"))
                return @"C:\Python26\ArcGIS10.0\python.exe";
            else
                return string.Empty;
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

        public static void GetToolboxPath()
        {
            ArcPadExePath = GetArcPadExePath();
            ArcPadTbxPath = ArcPadExePath.Replace("ArcPad.exe", @"DesktopTools10.0\Toolboxes\ArcPad Tools.tbx");
        }

        public static void OpenAxf(string axf)
        {
            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ArcPadExePath,
                    Arguments = string.Format("\"{0}\"", axf)
                }
            };

            p.Start();
        }

        public static void OpenDataInArcMap(string[] features, ref ListBox lbx)
        {
            var runtimes = RuntimeManager.InstalledRuntimes;
            string arcmap = runtimes.Where(runtimeInfo => 
                runtimeInfo.Product.ToString() == "Desktop").Select(runtimeInfo => 
                    runtimeInfo.Path).FirstOrDefault();

            if (arcmap == null) return;
            lbx.Items.Add("OPTIONAL WORKFLOW: DISPLAY RESULTS IN ARCMAP...");
            lbx.Items.Add("Opening Data in new Map Document");
            lbx.Refresh();

            arcmap = Path.Combine(arcmap, @"bin\ArcMap.exe");

            string mxdPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ArcMap.mxd");
            IMapDocument mapDocument = new MapDocumentClass();
            mapDocument.New(mxdPath);
            mapDocument.ActiveView.Activate(0);

            IGPUtilities3 gpUtils = new GPUtilitiesClass();

            foreach (FeatureLayerClass layer in features.Select(
                gpUtils.OpenFeatureClassFromString).Select(fClass => 
                    new FeatureLayerClass {FeatureClass = fClass, Name = fClass.AliasName}))
            {
                mapDocument.ActiveView.FocusMap.AddLayer(layer);
            }

            mapDocument.ActiveView.Extent = ((IGeoDataset) 
                ((IFeatureLayer) mapDocument.ActiveView.FocusMap.Layer[mapDocument.ActiveView.FocusMap.LayerCount - 1]).FeatureClass).Extent;
            mapDocument.Save(true);
            mapDocument.Close();

            Process p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = arcmap,
                    Arguments = string.Format("\"{0}\"", mxdPath)
                }
            };

            p.Start();
        }

        public static IWorkspace OpenGeodatabaseWorkspace(string geodatabase)
        {
            const string progId = "esriDataSourcesGDB.FileGDBWorkspaceFactory";
            Type factoryType = Type.GetTypeFromProgID(progId);
            IWorkspaceFactory pWorkspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            return pWorkspaceFactory.OpenFromFile(geodatabase, 0);
        }

        public static string SimplifyPath(string path, int length)
        {
            List<string> parts = path.Split(Path.DirectorySeparatorChar).Reverse().ToList();
            List<string> xPath = new List<string>();

            for (int i = 0; i < length; i++)
            {
                xPath.Add(parts[i]);
            }

            xPath.Reverse();

            return string.Join(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture), xPath);
        }
    }
}

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Console2
{
    class Program2
    {
        private static readonly LicenseInitializer AoLicenseInitializer = new LicenseInitializer();
        private static readonly Stopwatch StopWatch = new Stopwatch();
        private static int _count = 0;

        [STAThread]
        static void Main()
        {
            LicenseApplication();

            IApplication pArcMap = MiscClass.GetActiveArcMapSession();

            if (pArcMap != null)
            {
                Console.Write("Initializing...");

                IBasicDocument2 pBasicDocument = (IBasicDocument2)pArcMap.Document;
                IFeatureClass pFeatureClass = ((IFeatureLayer)pBasicDocument.FocusMap.Layer[0]).FeatureClass;

                // Alternative Workflow:
                //   Uses the string path instead of the FeatureClass object from the layer in the map
                string name = pFeatureClass.AliasName;
                string path = ((IDataset)pFeatureClass).Workspace.PathName;

                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)MiscClass.OpenGDBWorkspaceFromFile(path);
                pFeatureClass = pFeatureWorkspace.OpenFeatureClass(name);
                // End Alternative Workflow

                int expected = MiscClass.GetCountUsingGP(pFeatureClass);
                MiscClass.SetFieldToNull(pFeatureClass, MiscClass.FieldA);
                MiscClass.SetFieldToNull(pFeatureClass, MiscClass.FieldB);

                Console.WriteLine("Done.\n\nPress enter to begin test."); Console.ReadLine();

                while (true)
                {
                    UpdateCursor(pFeatureClass, expected);

                    Console.WriteLine("Enter Y to run again or press enter to exit.");
                    string line = Console.ReadLine();

                    if (line != null && line.ToUpper() != "Y") break;
                }

                MiscClass.KillProcess("ArcMap");
            }

            AoLicenseInitializer.ShutdownApplication();
        }

        static void UpdateCursor(IFeatureClass pFeatureClass, int expected)
        {
            int count = 0;
            _count += 1;

            try
            {
                IFeatureCursor pUpdateCursor = pFeatureClass.Update(null, true);
                int updateFieldA = pFeatureClass.FindField(MiscClass.FieldA);
                int updateFieldB = pFeatureClass.FindField(MiscClass.FieldB);

                try
                {
                    Console.Write("Updating features via Update Cursor...");
                    Console.Write(0.ToString(MiscClass.Percent));
                    StopWatch.Restart();
                    IFeature pFeature;
                    while ((pFeature = pUpdateCursor.NextFeature()) != null)
                    {
                        count += 1;
                        if ((count % 100) == 0)
                        {
                            double current = count / (double)expected;
                            Console.Write(MiscClass.Bkspace + current.ToString(MiscClass.Percent));
                        }

                        pFeature.Value[updateFieldA] = Environment.TickCount;
                        pFeature.Value[updateFieldB] = _count;
                        pUpdateCursor.UpdateFeature(pFeature);
                    }
                }

                catch (Exception ex) { Console.WriteLine("EXCEPTION:\n..." + ex.Message); }
                finally
                {
                    StopWatch.Stop();
                    TimeSpan ts = StopWatch.Elapsed;
                    Marshal.FinalReleaseComObject(pUpdateCursor);

                    Console.Write(MiscClass.Bkspace);
                    Console.WriteLine("Done. [Time: {0:00}:{1}]\n", Math.Floor(ts.TotalMinutes), ts.ToString("ss\\.ff"));
                }

            }
            catch (Exception ex) { Console.WriteLine("\nException: {0}\n", ex.Message); }
        }

        static void LicenseApplication()
        {
            if (
                AoLicenseInitializer.InitializeApplication(
                    new[] { esriLicenseProductCode.esriLicenseProductCodeAdvanced },
                    new esriLicenseExtensionCode[] { })) return;
            Console.WriteLine(AoLicenseInitializer.LicenseMessage());
            Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.");
            AoLicenseInitializer.ShutdownApplication();
        }
    }
}

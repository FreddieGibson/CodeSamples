using System.Globalization;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Console3
{
    class Program3
    {
        private static readonly LicenseInitializer AoLicenseInitializer = new LicenseInitializer();
        private static readonly Stopwatch StopWatch = new Stopwatch();
        private static int _count = 0;

        [STAThread]
        static void Main()
        {
            LicenseApplication();

            MiscClass.KillProcess("ArcMap");
            Process tArcMap = MiscClass.StartArcGISDesktopSession("ArcMap");
            Console.WriteLine("Press enter after ArcMap session has started completed.");
            Console.ReadLine();

            Console.Write("Enter a number 1-3 and press enter. (1=10K 2=50K 3=100K) ");
            string choice = Console.ReadLine();

            string feat;
            switch (int.Parse(choice[choice.Length - 1].ToString(CultureInfo.InvariantCulture)))
            {
                case 2:
                    feat = MiscClass.Feats[1];
                    break;
                case 3:
                    feat = MiscClass.Feats[2];
                    break;
                default:
                    feat = MiscClass.Feats[0];
                    break;
            }

            Console.WriteLine("\nAdd {0} to the map and then press enter.", feat);
            Console.ReadLine();

            Console.Write("Initializing...");
            string path = System.IO.Path.GetDirectoryName(feat);
            string name = System.IO.Path.GetFileName(feat);

            IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace) MiscClass.OpenGDBWorkspaceFromFile(path);
            IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(name);

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

            tArcMap.Kill();

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

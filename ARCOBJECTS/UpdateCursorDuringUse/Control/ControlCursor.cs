using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Control
{
    class ControlCursor
    {
        private static readonly LicenseInitializer AoLicenseInitializer = new LicenseInitializer();
        private static readonly Stopwatch StopWatch = new Stopwatch();
        private static int _count = 0;
    
        [STAThread]
        static void Main()
        {
            LicenseApplication();

            Console.WriteLine("*** CONTROL TEST A ***");
            foreach (var feature in MiscClass.Feats)
            {
                Console.Write("Initializing...");
                string path = System.IO.Path.GetDirectoryName(feature);
                string name = System.IO.Path.GetFileName(feature);

                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)MiscClass.OpenGDBWorkspaceFromFile(path);
                IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(name);

                int expected = MiscClass.GetCountUsingGP(pFeatureClass);
                MiscClass.SetFieldToNull(pFeatureClass, MiscClass.FieldA);
                MiscClass.SetFieldToNull(pFeatureClass, MiscClass.FieldB);

                Console.WriteLine("Done.\nTesting Update Cursor against features on disk.");
                UpdateCursor(pFeatureClass, expected);
            }

            Console.WriteLine("*** CONTROL TEST B ***");
            foreach (var feature in MiscClass.Feats)
            {
                Console.Write("Initializing...");
                string path = System.IO.Path.GetDirectoryName(feature);
                string name = System.IO.Path.GetFileName(feature);

                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)MiscClass.OpenGDBWorkspaceFromFile(path);
                IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(name);

                int expected = MiscClass.GetCountUsingGP(pFeatureClass);
                MiscClass.SetFieldToNull(pFeatureClass, MiscClass.FieldA);
                MiscClass.SetFieldToNull(pFeatureClass, MiscClass.FieldB);

                Console.WriteLine("Done.\nTesting optimized Update Cursor against features on disk.");
                UpdateCursor(pFeatureClass, expected, true);
            }

            IWorkspaceFactory pWorkspaceFactory = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWorkspaceName = pWorkspaceFactory.Create(null, "MyWorkspace", null, 0);
            IName pName = (IName) pWorkspaceName;
            IWorkspace pMemWorkspace = (IWorkspace) pName.Open();

            Console.WriteLine("*** CONTROL TEST C ***");
            foreach (var feature in MiscClass.Feats)
            {
                Console.Write("Copying data into In-Memory workspace...");
                string path = System.IO.Path.GetDirectoryName(feature);
                string name = System.IO.Path.GetFileName(feature);

                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)MiscClass.OpenGDBWorkspaceFromFile(path);
                IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(name);

                MiscClass.ConvertFeatureClass(pFeatureClass, pMemWorkspace, pFeatureClass.AliasName);

                IFeatureWorkspace pMemFeatureWorkspace = (IFeatureWorkspace) pMemWorkspace;
                IFeatureClass pMemFeatureClass = pMemFeatureWorkspace.OpenFeatureClass(name);

                Console.Write("Done.\nIntializing...");
                int expected = MiscClass.GetCountUsingGP(pMemFeatureClass);
                MiscClass.SetFieldToNull(pMemFeatureClass, MiscClass.FieldA);
                MiscClass.SetFieldToNull(pMemFeatureClass, MiscClass.FieldB);

                Console.WriteLine("Done.\nTesting Update Cursor against features in memory.");
                UpdateCursor(pMemFeatureClass, expected);
                
                ((IDataset)pMemFeatureClass).Delete();
            }

            Console.WriteLine("*** CONTROL TEST D ***\n");
            foreach (var feature in MiscClass.Feats)
            {
                Console.Write("Copying data into In-Memory workspace...");
                string path = System.IO.Path.GetDirectoryName(feature);
                string name = System.IO.Path.GetFileName(feature);

                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)MiscClass.OpenGDBWorkspaceFromFile(path);
                IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(name);

                MiscClass.ConvertFeatureClass(pFeatureClass, pMemWorkspace, pFeatureClass.AliasName);

                IFeatureWorkspace pMemFeatureWorkspace = (IFeatureWorkspace)pMemWorkspace;
                IFeatureClass pMemFeatureClass = pMemFeatureWorkspace.OpenFeatureClass(name);

                Console.Write("Done.\nIntializing...");
                int expected = MiscClass.GetCountUsingGP(pMemFeatureClass);
                MiscClass.SetFieldToNull(pMemFeatureClass, MiscClass.FieldA);
                MiscClass.SetFieldToNull(pMemFeatureClass, MiscClass.FieldB);

                Console.WriteLine("Done.\nTesting optimized Update Cursor against features in memory.");
                UpdateCursor(pMemFeatureClass, expected, true);

                ((IDataset)pMemFeatureClass).Delete();
            }
            
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            AoLicenseInitializer.ShutdownApplication();
        }

        static void SearchCursor(IFeatureClass pFeatureClass, int expected, bool optimized = false)
        {
            int count = 0;
            _count += 1;

            try
            {
                IQueryFilter pQueryFilter = (optimized)
                ? new QueryFilterClass { SubFields = string.Join(",", new[] { MiscClass.FieldA, MiscClass.FieldB }) }
                : new QueryFilterClass();

                IFeatureCursor pSearchCursor = pFeatureClass.Search(pQueryFilter, false);
                int searchFieldA = pFeatureClass.FindField(MiscClass.FieldA);
                int searchFieldB = pFeatureClass.FindField(MiscClass.FieldB);

                IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)((IDataset)pFeatureClass).Workspace;
                pWorkspaceEdit.StartEditing(true);
                pWorkspaceEdit.StartEditOperation();

                try
                {
                    Console.Write("Updating feature  [{0}] via Search Cursor...", pFeatureClass.AliasName);
                    Console.Write(0.ToString(MiscClass.Percent));
                    StopWatch.Restart();
                    IFeature pFeature;

                    while ((pFeature = pSearchCursor.NextFeature()) != null)
                    {
                        count += 1;
                        if ((count % 100) == 0)
                        {
                            double current = count / (double)expected;
                            Console.Write(MiscClass.Bkspace + current.ToString(MiscClass.Percent));
                        }

                        pFeature.Value[searchFieldA] = Environment.TickCount;
                        pFeature.Value[searchFieldB] = _count;
                        pFeature.Store();
                        //pSearchCursor.UpdateFeature(pFeature);
                    }
                }
                catch (Exception ex) { Console.WriteLine("EXCEPTION:\n..." + ex.Message); }
                finally
                {
                    StopWatch.Stop();
                    TimeSpan ts = StopWatch.Elapsed;
                    Marshal.FinalReleaseComObject(pSearchCursor);

                    Console.Write(MiscClass.Bkspace);
                    Console.WriteLine("Done. [Time: {0:00}:{1}]\n", Math.Floor(ts.TotalMinutes), ts.ToString("ss\\.ff"));
                }

                pWorkspaceEdit.StopEditOperation();
                pWorkspaceEdit.StopEditing(true);
            }
            catch (Exception ex) { Console.WriteLine("\nException: {0}\n", ex.Message); }
        }

        static void UpdateCursor(IFeatureClass pFeatureClass, int expected, bool optimized = false)
        {
            int count = 0;
            _count += 1;

            IQueryFilter pQueryFilter = (optimized)
                ? new QueryFilterClass {SubFields = string.Join(",", new[] {MiscClass.FieldA, MiscClass.FieldB})}
                : new QueryFilterClass();

            IFeatureCursor pUpdateCursor = pFeatureClass.Update(pQueryFilter, true);
            int updateFieldA = pFeatureClass.FindField(MiscClass.FieldA);
            int updateFieldB = pFeatureClass.FindField(MiscClass.FieldB);
            try
            {
                try
                {
                    Console.Write("Updating feature [{0}] via Update Cursor...", pFeatureClass.AliasName);
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
                    new[] {esriLicenseProductCode.esriLicenseProductCodeAdvanced},
                    new esriLicenseExtensionCode[] {})) return;
            Console.WriteLine(AoLicenseInitializer.LicenseMessage());
            Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.");
            AoLicenseInitializer.ShutdownApplication();
        }
    }
}

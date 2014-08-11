using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MemUsageConsole
{
    class Program
    {
        private static readonly LicenseInitializer _aoLicenseInitializer = new LicenseInitializer();

        [STAThread()]
        static void Main(string[] args)
        {
            if (!AppIsLicensed()) return;

            MiscClass.KillProcess(new []{"ArcMap", "taskmgr"});

            int request;

            while (true)
            {
                Console.WriteLine("1 : Open Single Object and Session of ArcMap\n2 : Open Multiple Objects\n3 : Open Multiple ArcMap Sessions\nEnter 1, 2, or 3");
                if (int.TryParse(Console.ReadLine(), out request)) break;
            }

            Console.Write("\nSelect a map document...");
            string mxdPath = MiscClass.BrowseForFile("Select a *.mxd file", "Map Document (*.mxd)|*.mxd");
            Console.WriteLine("Done.");

            int numMaps = 2;

            if (request == 2 || request == 3)
            {
                while (true)
                {
                    Console.WriteLine("How many maps do you want to create?");
                    if (int.TryParse(Console.ReadLine(), out numMaps)) break;
                }
            }

            string arcmap = MiscClass.GetArcGISDesktopProductPath("ArcMap");

            string[] maps;
            IMapDocument mapDoc = new MapDocumentClass();
            Console.WriteLine();

            switch (request)
            {
                case 1:
                    Console.WriteLine("Opening Map Document with IMapDocument::Open and in ArcMap.exe");
                    Console.Write("...Creating 2 map documents...");
                    maps = MiscClass.CloneMaps(2, mxdPath);
                    Console.WriteLine("Done.");

                    Console.Write("...Opening MXD Object and ArcMap.exe");
                    mapDoc.Open(maps[0]);

                    // Open map documents using ArcMap.exe
                    Process.Start(arcmap, maps[1]);

                    // Open Task Manager
                    Process.Start("taskmgr");

                    Console.WriteLine("Done.\n...Look at Task Manager");
                    break;
                case 2:
                    Console.WriteLine("Open {0:0,0} mxds with IMapDocument::Open", numMaps);
                    Console.Write("...Creating {0:0,0} map documents...", numMaps);
                    maps = MiscClass.CloneMaps(numMaps, mxdPath);
                    Console.WriteLine("Done.");

                    Console.Write("...Opening {0:0,0} map documents...", numMaps);
                    List<IMapDocument> mapObjects = new List<IMapDocument>();
                    for (int i = 0; i < maps.Length; i++)
                    {
                        mapObjects.Add(new MapDocumentClass());
                        mapObjects[i].Open(maps[i]);
                    }

                    // Open Task Manager
                    Process.Start("taskmgr");

                    Console.WriteLine("Done.\n...Look at Task Manager");
                    break;
                case 3:
                    Console.WriteLine("Open {0:0,0} mxds with IMapDocument::Open", numMaps);
                    Console.Write("...Creating {0:0,0} map documents...", numMaps);
                    maps = MiscClass.CloneMaps(numMaps, mxdPath);
                    Console.WriteLine("Done.");

                    Console.Write("...Opening {0:0,0} map documents...", numMaps);
                    foreach (string t in maps)
                    {
                        // Open map documents using ArcMap.exe
                        Process.Start(arcmap, t);
                    }

                    // Open Task Manager
                    Process.Start("taskmgr");

                    Console.WriteLine("Done.\n...Look at Task Manager");
                    break;
                default:
                    Console.WriteLine("Invalid Request [{0}]", request);
                    break;
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

            MiscClass.KillProcess(new[] { "ArcMap", "taskmgr" });

            _aoLicenseInitializer.ShutdownApplication();
        }

        private static bool AppIsLicensed()
        {
            //ESRI License Initializer generated code.
            if (_aoLicenseInitializer.InitializeApplication(new[] {esriLicenseProductCode.esriLicenseProductCodeAdvanced},
                    new esriLicenseExtensionCode[] {})) return true;

            Console.WriteLine(_aoLicenseInitializer.LicenseMessage());
            Console.WriteLine("This application could not initialize with the correct ArcGIS license and will shutdown.");
            _aoLicenseInitializer.ShutdownApplication();
            return false;
        }
    }
}

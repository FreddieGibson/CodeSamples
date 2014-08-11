using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace MemUsageConsole
{
    class MiscClass
    {
        public static void KillProcess(string[] processNames)
        {
            foreach (var process in processNames.SelectMany(Process.GetProcessesByName))
            {
                process.Kill();
            }
        }

        public static string GetArcGISDesktopProductPath(string productName)
        {
            IEnumerable<RuntimeInfo> runtimes = RuntimeManager.InstalledRuntimes;
            string productPath = (from runtime in runtimes where runtime.Product.ToString() == "Desktop" select runtime.Path).FirstOrDefault();

            if (productPath == null) return null;
            string exe;
            switch (productName.ToLower())
            {
                case "arccatalog":
                    exe = Path.Combine(productPath, @"bin\ArcCatalog.exe");
                    break;
                case "arcscene":
                    exe = Path.Combine(productPath, @"bin\ArcCatalog.exe");
                    break;
                case "arcglobe":
                    exe = Path.Combine(productPath, @"bin\ArcGlobe.exe");
                    break;
                default:
                    exe = Path.Combine(productPath, @"bin\ArcMap.exe");
                    break;
            }

            return exe;
        }

        public static string[] CloneMaps(int numCopies, string mxdPath)
        {
            List<string> maps = new List<string>();
            string testPath = Path.Combine(Path.GetDirectoryName(mxdPath), "TestMaps");
            if (System.IO.Directory.Exists(testPath)) System.IO.Directory.Delete(testPath, true);

            IMapDocument srcMap = new MapDocumentClass();
            srcMap.Open(mxdPath);
            srcMap.Save(false);
            Marshal.FinalReleaseComObject(srcMap);

            System.IO.Directory.CreateDirectory(testPath);
            for (int i = 0; i < numCopies; i++)
            {
                const string fmt = "_{0:0000}.mxd";
                string mapName = Path.GetFileNameWithoutExtension(mxdPath) + string.Format(fmt, i + 1);
                string mapPath = Path.Combine(testPath, mapName);
                System.IO.File.Copy(mxdPath, mapPath);
                maps.Add(mapPath);
            }

            return maps.ToArray();
        }

        public static string BrowseForFile(string title, string extension)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = extension,
                Multiselect = false,
                ShowHelp = true,
                Title = title,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) { return null; }

            string fileName = openFileDialog.FileName;
            return fileName;
        }
    }
}

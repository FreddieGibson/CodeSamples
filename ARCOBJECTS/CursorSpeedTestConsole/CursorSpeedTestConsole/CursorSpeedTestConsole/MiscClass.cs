using System;
using System.Windows.Forms;

namespace CursorSpeedTestConsole
{
    class MiscClass
    {
        public static string SelectShapefile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Shapefiles (*.shp)|*.shp",
                Multiselect = false,
                ShowHelp = false,
                Title = "Please select an esri shapefile",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                RestoreDirectory = true
            };

            return (openFileDialog.ShowDialog() == DialogResult.OK) ? openFileDialog.FileName : null;
        }
    }
}

using System;
using ESRI.ArcGIS;

namespace CursorSpeedTestConsole
{
    internal partial class LicenseInitializer
    {
        public LicenseInitializer()
        {
            ResolveBindingEvent += BindingArcGISRuntime;
        }

        static void BindingArcGISRuntime(object sender, EventArgs e)
        {
            //
            // TODO: Modify ArcGIS runtime binding code as needed
            //
            if (RuntimeManager.Bind(ProductCode.Desktop)) return;

            // Failed to bind, announce and force exit
            Console.WriteLine("Invalid ArcGIS runtime binding. Application will shut down.");
            Environment.Exit(0);
        }
    }
}
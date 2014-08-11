using System;
using ESRI.ArcGIS;

namespace MemUsageConsole
{
    internal partial class LicenseInitializer
    {
        public LicenseInitializer() { ResolveBindingEvent += BindingArcGISRuntime; }

        static void BindingArcGISRuntime(object sender, EventArgs e)
        {
            if (RuntimeManager.Bind(ProductCode.EngineOrDesktop)) return;
            
            // Failed to bind, announce and force exit
            Console.WriteLine("Invalid ArcGIS runtime binding. Application will shut down.");
            Environment.Exit(0);
        }
    }
}
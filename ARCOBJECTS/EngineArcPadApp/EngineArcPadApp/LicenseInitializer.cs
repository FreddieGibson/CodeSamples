using System;
using ESRI.ArcGIS;

namespace EngineArcPadApp
{
    internal partial class LicenseInitializer
    {
        public LicenseInitializer()
        {
            ResolveBindingEvent += BindingArcGISRuntime;
        }

        static void BindingArcGISRuntime(object sender, EventArgs e)
        {
            if (RuntimeManager.Bind(MiscClass.BindingProductCode)) return;

            // Failed to bind, announce and force exit
            System.Windows.Forms.MessageBox.Show("Invalid ArcGIS runtime binding. Application will shut down.");
            Environment.Exit(0);
        }
    }
}
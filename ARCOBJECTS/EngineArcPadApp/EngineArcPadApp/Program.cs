using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

namespace EngineArcPadApp
{
    static class Program
    {
        private static readonly LicenseInitializer _aoLicenseInitializer = new LicenseInitializer();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ESRI License Initializer generated code.
            if (!_aoLicenseInitializer.InitializeApplication(new [] { MiscClass.ProductCode },
            new esriLicenseExtensionCode[] { }))
            {
                MessageBox.Show(_aoLicenseInitializer.LicenseMessage() +
                "\n\nThis application could not initialize with the correct ArcGIS license and will shutdown.",
                "ArcGIS License Failure");

                _aoLicenseInitializer.ShutdownApplication();
                Application.Exit();
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ArcPadSampleForm());

            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            _aoLicenseInitializer.ShutdownApplication();
        }
    }
}
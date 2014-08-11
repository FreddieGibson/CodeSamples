using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ESRI.ArcGIS.esriSystem;
using WPFMapViewer;

namespace WPFMapWrapper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeEngineLicense();
        }

        private static void InitializeEngineLicense()
        {
            AoInitialize aoi = new AoInitializeClass();

            //more license choices could be included here
            esriLicenseProductCode productCode = MiscClass.LicenseLevel;
            if (aoi.IsProductCodeAvailable(productCode) != esriLicenseStatus.esriLicenseAvailable) return;

            aoi.Initialize(productCode);
            aoi.CheckOutExtension(esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst);
        }
    }
}

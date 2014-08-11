using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Path = System.IO.Path;

namespace EngineArcPadApp
{
    public partial class ArcPadSampleForm : Form
    {
        private static bool _useArcObjects = true;
        private static bool _useArcpy = true;
        private static bool _useManaged = true;
        private static IGeoProcessor2 _gpUnManaged;
        private static Geoprocessor _gpManaged;
        private static string _gdb;
        private static string _axf;
        private static string[] _feats;

        private static ListBox _msg;

        public ArcPadSampleForm()
        {
            InitializeComponent();

            _msg = this.msgListBox;

            this.checkOutButton.Enabled = false;
            this.openAxfButton.Enabled = false;
            this.checkInButton.Enabled = false;
            this.pyOptionsGroupBox.Enabled = false;

            MiscClass.GetToolboxPath();
            _gpManaged = new Geoprocessor { OverwriteOutput = true };
            _gpUnManaged = new GeoProcessorClass { OverwriteOutput = true };
            
        }

        private void mxdButton_Click(object sender, EventArgs e)
        {
            _msg.Items.Add("PREPARING DATA..."); _msg.Refresh();
            _gdb = Path.Combine(MiscClass.Data, "Riverside.gdb");
            _axf = _gdb.Replace(".gdb", ".axf");

            _msg.Items.Add("GDB Path: ..\\" + MiscClass.SimplifyPath(_gdb, 3));
            _msg.Items.Add("AXF Path: ..\\" + MiscClass.SimplifyPath(_axf, 3)); _msg.Refresh();

            IWorkspace ws = MiscClass.OpenGeodatabaseWorkspace(_gdb);
            string[] gdbFeatures = MiscClass.GetFeatures(ws, ref _gpManaged);

            _feats = gdbFeatures.ToArray();
            foreach (string feat in _feats)
            {
                _msg.Items.Add("Feature Class: ..\\" + MiscClass.SimplifyPath(feat, 4)); _msg.Refresh();  
            }
            
            this.checkOutButton.Enabled = true;
            _msg.Items.Add(">>>>>>>>>>>>>>>"); _msg.Refresh();
        }

        private void checkOutButton_Click(object sender, EventArgs e)
        {
            _msg.Items.Add("CHECKING OUT DATA..."); _msg.Refresh();
            if (File.Exists(_axf)) File.Delete(_axf);

            ArcPad_CheckOut(_feats, _axf);
            this.openAxfButton.Enabled = System.IO.File.Exists(_axf);
        }

        private void openAxfButton_Click(object sender, EventArgs e)
        {
            _msg.Items.Add("OPENING AXF..."); _msg.Refresh();
            MiscClass.OpenAxf(_axf);
            this.checkInButton.Enabled = true;
            
            _msg.Items.Add(">>>>>>>>>>>>>>>"); _msg.Refresh();
        }

        private void checkInButton_Click(object sender, EventArgs e)
        {
            _msg.Items.Add("CHECKING IN DATA..."); _msg.Refresh();
            ArcPad_CheckIn(_axf);

            // OPTIONAL: Display data in ArcMap.
            MiscClass.OpenDataInArcMap(_feats, ref _msg);
        }
        
        private static void ArcPad_CheckOut(string[] features, string axf, string schemaOnly = "#", string password = "#", string encrypt = "#")
        {
            if (_useArcObjects)
            {
                if (_useManaged)
                {
                    const String tool = "ArcPadCheckout_ArcPad";
                    IVariantArray parameters = new VarArrayClass();

                    parameters.Add(string.Join(";", features));
                    parameters.Add(schemaOnly);
                    parameters.Add(password);
                    parameters.Add(encrypt);
                    parameters.Add(axf);

                    if (File.Exists(axf))
                        File.Delete(axf);

                    try
                    {
                        _msg.Items.Add("Executing GP using Geoprocessor (Managed) class in ArcObjects");
                        //_gpManaged.AddToolbox(MiscClass.ArcPadTbxPath);
                        //_msg.Items.Add("Adding Toolbox ..\\" + MiscClass.SimplifyPath(MiscClass.ArcPadTbxPath, 4));
                        _msg.Refresh();

                        var pResult = (IGeoProcessorResult2) _gpManaged.Execute(tool, parameters, null);
                        Messages(pResult);
                    }
                    catch (Exception ex) { Messages(ex, ref _gpManaged); }
                    finally
                    {
                        _gpManaged.ClearMessages();
                        //_gpManaged.RemoveToolbox(MiscClass.ArcPadTbxPath);
                    }

                    parameters.RemoveAll();
                }
                else
                {
                    const String tool = "ArcPadCheckout_ArcPad";
                    IVariantArray parameters = new VarArrayClass();

                    parameters.Add(string.Join(";", features));
                    parameters.Add(schemaOnly);
                    parameters.Add(password);
                    parameters.Add(encrypt);
                    parameters.Add(axf);

                    if (File.Exists(axf))
                        File.Delete(axf);

                    try
                    {
                        _msg.Items.Add("Executing GP using IGeoProcessor (Un-Managed) class in ArcObjects");
                        //_gpUnManaged.AddToolbox(MiscClass.ArcPadTbxPath);
                        //_msg.Items.Add("Adding Toolbox ..\\" + MiscClass.SimplifyPath(MiscClass.ArcPadTbxPath, 4));
                        _msg.Refresh();

                        var pResult = (IGeoProcessorResult2)_gpUnManaged.Execute(tool, parameters, null);
                        Messages(pResult);
                    }
                    catch (Exception ex) { Messages(ex, ref _gpUnManaged); }
                    finally
                    {
                        _gpUnManaged.ClearMessages();
                        //_gpUnManaged.RemoveToolbox(MiscClass.ArcPadTbxPath);
                    }

                    parameters.RemoveAll();
                }
                
            }
            else
            {
                string pyPath = MiscClass.CreateCheckoutScript(features, axf, schemaOnly, password, encrypt, _useArcpy);

                if (File.Exists(axf)) File.Delete(axf);

                _msg.Items.Add("Executing GP using " + ((_useArcpy) ? "arcpy" : "arcgisscripting") + " class in Python");
                _msg.Refresh();

                string command = string.Format("\"{0}\"", pyPath);
                string message = MiscClass.ExecuteCommand(command);

                if (message == string.Empty) _msg.Items.Add("Unhandled exception during execution. Most likely python exe crashed.");
                Messages(message, new[] { '\r', '\n' });
            }
        }

        private static void ArcPad_CheckIn(string axf, string password = "#", string features = "#")
        {
            if (_useArcObjects)
            {
                if (_useManaged)
                {
                    const String tool = "ArcPadCheckin_ArcPad";
                    IVariantArray parameters = new VarArrayClass();

                    parameters.Add(axf);
                    parameters.Add(password);
                    parameters.Add(features);

                    try
                    {
                        _msg.Items.Add("Executing GP using Geoprocessor (Managed) class in ArcObjects");
                        //_gpManaged.AddToolbox(MiscClass.ArcPadTbxPath);
                        //_msg.Items.Add("Adding Toolbox ..\\" + MiscClass.SimplifyPath(MiscClass.ArcPadTbxPath, 4));
                        _msg.Refresh();

                        var pResult = (IGeoProcessorResult2)_gpManaged.Execute(tool, parameters, null);
                        Messages(pResult);
                    }
                    catch (Exception ex) { Messages(ex, ref _gpManaged); }
                    finally
                    {
                        _gpManaged.ClearMessages();
                        _gpManaged.RemoveToolbox(MiscClass.ArcPadTbxPath);
                    }

                    parameters.RemoveAll();   
                    
                }
                else
                {
                    const String tool = "ArcPadCheckin_ArcPad";
                    IVariantArray parameters = new VarArrayClass();

                    parameters.Add(axf);
                    parameters.Add(password);
                    parameters.Add(features);

                    try
                    {
                        _msg.Items.Add("Executing GP using IGeoProcessor2 (Un-Managed) class in ArcObjects");
                        //_gpUnManaged.AddToolbox(MiscClass.ArcPadTbxPath);
                        //_msg.Items.Add("Adding Toolbox ..\\" + MiscClass.SimplifyPath(MiscClass.ArcPadTbxPath, 4));
                        _msg.Refresh();

                        var pResult = (IGeoProcessorResult2)_gpUnManaged.Execute(tool, parameters, null);
                        Messages(pResult);
                    }
                    catch (Exception ex) { Messages(ex, ref _gpUnManaged); }
                    finally
                    {
                        _gpUnManaged.ClearMessages();
                        _gpUnManaged.RemoveToolbox(MiscClass.ArcPadTbxPath);
                    }

                    parameters.RemoveAll();   
                }
            }
            else
            {
                string pyPath = MiscClass.CreateCheckinScript(axf, password, features, _useArcpy);

                _msg.Items.Add("Executing GP using " + ((_useArcpy) ? "arcpy" : "arcgisscripting") + " class in Python");
                _msg.Refresh();

                string command = string.Format("\"{0}\"", pyPath);
                string message = MiscClass.ExecuteCommand(command);

                if (message == string.Empty) _msg.Items.Add("Unhandled exception during execution. Most likely python exe crashed.");
                Messages(message, new [] {'\r', '\n'});
            }
        }

        public static void Messages(IGeoProcessorResult2 result)
        {
            if (result.MessageCount <= 0) return;
            for (var i = 0; i < result.MessageCount; i++)
            {
                _msg.Items.Add(result.GetMessage(i));
            }
            _msg.Items.Add(">>>>>>>>>>>>>>>"); _msg.Refresh();
        }

        public static void Messages(Exception ex, ref Geoprocessor gp)
        {
            _msg.Items.Add("..EXCEPTION: " + ex.Message);
            if (gp.MessageCount <= 0) return;
            for (var i = 0; i < gp.MessageCount; i++)
            {
                _msg.Items.Add(".." + gp.GetMessage(i));
            }
            _msg.Items.Add(">>>>>>>>>>>>>>>"); _msg.Refresh();
        }

        public static void Messages(Exception ex, ref IGeoProcessor2 gp)
        {
            _msg.Items.Add("..EXCEPTION: " + ex.Message);
            if (gp.MessageCount <= 0) return;
            for (var i = 0; i < gp.MessageCount; i++)
            {
                _msg.Items.Add(".." + gp.GetMessage(i));
            }
            _msg.Items.Add(">>>>>>>>>>>>>>>"); _msg.Refresh();
        }

        public static void Messages(string messages, char[] delimiters)
        {

            foreach (string message in messages.Split(delimiters, StringSplitOptions.RemoveEmptyEntries))
            {
                _msg.Items.Add(message);
            }
            _msg.Items.Add(">>>>>>>>>>>>>>>"); _msg.Refresh();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton10X_CheckedChanged(object sender, EventArgs e)
        {
            string curVersion = (!this.radioButton10X.Checked) ? @"DesktopTools10.0\Toolboxes" : "DesktopTools9.3";
            string newVersion = (this.radioButton10X.Checked) ? @"DesktopTools10.0\Toolboxes" : "DesktopTools9.3";

            MiscClass.ArcPadTbxPath = MiscClass.ArcPadTbxPath.Replace(curVersion, newVersion);
        }

        private void aoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _useArcObjects = this.aoRadioButton.Checked;
            this.pyOptionsGroupBox.Enabled = this.pyRadioButton.Checked;
            this.aoOptionsGroupBox.Enabled = this.aoRadioButton.Checked;
        }

        private void managedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _useManaged = this.managedRadioButton.Checked;
        }

        private void arcpyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _useArcpy = this.arcpyRadioButton.Checked;
        }
    }
}
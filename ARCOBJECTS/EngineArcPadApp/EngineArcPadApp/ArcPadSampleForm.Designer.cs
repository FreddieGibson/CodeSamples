namespace EngineArcPadApp
{
    partial class ArcPadSampleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gdbButton = new System.Windows.Forms.Button();
            this.checkOutButton = new System.Windows.Forms.Button();
            this.openAxfButton = new System.Windows.Forms.Button();
            this.checkInButton = new System.Windows.Forms.Button();
            this.msgListBox = new System.Windows.Forms.ListBox();
            this.msgLabel = new System.Windows.Forms.Label();
            this.radioButton93X = new System.Windows.Forms.RadioButton();
            this.radioButton10X = new System.Windows.Forms.RadioButton();
            this.tbxOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.aoRadioButton = new System.Windows.Forms.RadioButton();
            this.pyRadioButton = new System.Windows.Forms.RadioButton();
            this.aoOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.managedRadioButton = new System.Windows.Forms.RadioButton();
            this.unmanagedRadioButton = new System.Windows.Forms.RadioButton();
            this.pyOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.arcpyRadioButton = new System.Windows.Forms.RadioButton();
            this.arcgisscriptingRadioButton = new System.Windows.Forms.RadioButton();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.tbxOptionsGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gpOptionsGroupBox.SuspendLayout();
            this.aoOptionsGroupBox.SuspendLayout();
            this.pyOptionsGroupBox.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gdbButton
            // 
            this.gdbButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gdbButton.Location = new System.Drawing.Point(12, 158);
            this.gdbButton.Name = "gdbButton";
            this.gdbButton.Size = new System.Drawing.Size(125, 60);
            this.gdbButton.TabIndex = 0;
            this.gdbButton.Text = "Select GDB";
            this.gdbButton.UseVisualStyleBackColor = true;
            this.gdbButton.Click += new System.EventHandler(this.mxdButton_Click);
            // 
            // checkOutButton
            // 
            this.checkOutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkOutButton.Location = new System.Drawing.Point(12, 226);
            this.checkOutButton.Name = "checkOutButton";
            this.checkOutButton.Size = new System.Drawing.Size(125, 60);
            this.checkOutButton.TabIndex = 1;
            this.checkOutButton.Text = "Check Out";
            this.checkOutButton.UseVisualStyleBackColor = true;
            this.checkOutButton.Click += new System.EventHandler(this.checkOutButton_Click);
            // 
            // openAxfButton
            // 
            this.openAxfButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openAxfButton.Location = new System.Drawing.Point(12, 294);
            this.openAxfButton.Name = "openAxfButton";
            this.openAxfButton.Size = new System.Drawing.Size(125, 60);
            this.openAxfButton.TabIndex = 2;
            this.openAxfButton.Text = "Open AXF";
            this.openAxfButton.UseVisualStyleBackColor = true;
            this.openAxfButton.Click += new System.EventHandler(this.openAxfButton_Click);
            // 
            // checkInButton
            // 
            this.checkInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkInButton.Location = new System.Drawing.Point(12, 362);
            this.checkInButton.Name = "checkInButton";
            this.checkInButton.Size = new System.Drawing.Size(125, 60);
            this.checkInButton.TabIndex = 3;
            this.checkInButton.Text = "Check In";
            this.checkInButton.UseVisualStyleBackColor = true;
            this.checkInButton.Click += new System.EventHandler(this.checkInButton_Click);
            // 
            // msgListBox
            // 
            this.msgListBox.FormattingEnabled = true;
            this.msgListBox.HorizontalScrollbar = true;
            this.msgListBox.Location = new System.Drawing.Point(143, 158);
            this.msgListBox.Name = "msgListBox";
            this.msgListBox.ScrollAlwaysVisible = true;
            this.msgListBox.Size = new System.Drawing.Size(463, 264);
            this.msgListBox.TabIndex = 5;
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.Location = new System.Drawing.Point(143, 141);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(58, 13);
            this.msgLabel.TabIndex = 6;
            this.msgLabel.Text = "Messages:";
            // 
            // radioButton93X
            // 
            this.radioButton93X.AutoSize = true;
            this.radioButton93X.Location = new System.Drawing.Point(18, 38);
            this.radioButton93X.Name = "radioButton93X";
            this.radioButton93X.Size = new System.Drawing.Size(44, 17);
            this.radioButton93X.TabIndex = 7;
            this.radioButton93X.Text = "93X";
            this.radioButton93X.UseVisualStyleBackColor = true;
            // 
            // radioButton10X
            // 
            this.radioButton10X.AutoSize = true;
            this.radioButton10X.Checked = true;
            this.radioButton10X.Location = new System.Drawing.Point(18, 16);
            this.radioButton10X.Name = "radioButton10X";
            this.radioButton10X.Size = new System.Drawing.Size(44, 17);
            this.radioButton10X.TabIndex = 8;
            this.radioButton10X.TabStop = true;
            this.radioButton10X.Text = "10X";
            this.radioButton10X.UseVisualStyleBackColor = true;
            this.radioButton10X.CheckedChanged += new System.EventHandler(this.radioButton10X_CheckedChanged);
            // 
            // tbxOptionsGroupBox
            // 
            this.tbxOptionsGroupBox.Controls.Add(this.radioButton93X);
            this.tbxOptionsGroupBox.Controls.Add(this.radioButton10X);
            this.tbxOptionsGroupBox.Location = new System.Drawing.Point(171, 20);
            this.tbxOptionsGroupBox.Name = "tbxOptionsGroupBox";
            this.tbxOptionsGroupBox.Size = new System.Drawing.Size(125, 60);
            this.tbxOptionsGroupBox.TabIndex = 9;
            this.tbxOptionsGroupBox.TabStop = false;
            this.tbxOptionsGroupBox.Text = "Toolbox Version";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(620, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gpOptionsGroupBox
            // 
            this.gpOptionsGroupBox.Controls.Add(this.aoRadioButton);
            this.gpOptionsGroupBox.Controls.Add(this.pyRadioButton);
            this.gpOptionsGroupBox.Location = new System.Drawing.Point(37, 20);
            this.gpOptionsGroupBox.Name = "gpOptionsGroupBox";
            this.gpOptionsGroupBox.Size = new System.Drawing.Size(125, 60);
            this.gpOptionsGroupBox.TabIndex = 10;
            this.gpOptionsGroupBox.TabStop = false;
            this.gpOptionsGroupBox.Text = "GP Language";
            // 
            // aoRadioButton
            // 
            this.aoRadioButton.AutoSize = true;
            this.aoRadioButton.Checked = true;
            this.aoRadioButton.Location = new System.Drawing.Point(18, 18);
            this.aoRadioButton.Name = "aoRadioButton";
            this.aoRadioButton.Size = new System.Drawing.Size(77, 17);
            this.aoRadioButton.TabIndex = 7;
            this.aoRadioButton.TabStop = true;
            this.aoRadioButton.Text = "ArcObjects";
            this.aoRadioButton.UseVisualStyleBackColor = true;
            this.aoRadioButton.CheckedChanged += new System.EventHandler(this.aoRadioButton_CheckedChanged);
            // 
            // pyRadioButton
            // 
            this.pyRadioButton.AutoSize = true;
            this.pyRadioButton.Location = new System.Drawing.Point(18, 39);
            this.pyRadioButton.Name = "pyRadioButton";
            this.pyRadioButton.Size = new System.Drawing.Size(58, 17);
            this.pyRadioButton.TabIndex = 8;
            this.pyRadioButton.Text = "Python";
            this.pyRadioButton.UseVisualStyleBackColor = true;
            // 
            // aoOptionsGroupBox
            // 
            this.aoOptionsGroupBox.Controls.Add(this.managedRadioButton);
            this.aoOptionsGroupBox.Controls.Add(this.unmanagedRadioButton);
            this.aoOptionsGroupBox.Location = new System.Drawing.Point(305, 20);
            this.aoOptionsGroupBox.Name = "aoOptionsGroupBox";
            this.aoOptionsGroupBox.Size = new System.Drawing.Size(125, 60);
            this.aoOptionsGroupBox.TabIndex = 11;
            this.aoOptionsGroupBox.TabStop = false;
            this.aoOptionsGroupBox.Text = "ArcObjects Assembly";
            // 
            // managedRadioButton
            // 
            this.managedRadioButton.AutoSize = true;
            this.managedRadioButton.Checked = true;
            this.managedRadioButton.Location = new System.Drawing.Point(18, 18);
            this.managedRadioButton.Name = "managedRadioButton";
            this.managedRadioButton.Size = new System.Drawing.Size(70, 17);
            this.managedRadioButton.TabIndex = 7;
            this.managedRadioButton.TabStop = true;
            this.managedRadioButton.Text = "Managed";
            this.managedRadioButton.UseVisualStyleBackColor = true;
            this.managedRadioButton.CheckedChanged += new System.EventHandler(this.managedRadioButton_CheckedChanged);
            // 
            // unmanagedRadioButton
            // 
            this.unmanagedRadioButton.AutoSize = true;
            this.unmanagedRadioButton.Location = new System.Drawing.Point(18, 39);
            this.unmanagedRadioButton.Name = "unmanagedRadioButton";
            this.unmanagedRadioButton.Size = new System.Drawing.Size(87, 17);
            this.unmanagedRadioButton.TabIndex = 8;
            this.unmanagedRadioButton.Text = "Un-Managed";
            this.unmanagedRadioButton.UseVisualStyleBackColor = true;
            // 
            // pyOptionsGroupBox
            // 
            this.pyOptionsGroupBox.Controls.Add(this.arcpyRadioButton);
            this.pyOptionsGroupBox.Controls.Add(this.arcgisscriptingRadioButton);
            this.pyOptionsGroupBox.Location = new System.Drawing.Point(439, 19);
            this.pyOptionsGroupBox.Name = "pyOptionsGroupBox";
            this.pyOptionsGroupBox.Size = new System.Drawing.Size(125, 60);
            this.pyOptionsGroupBox.TabIndex = 12;
            this.pyOptionsGroupBox.TabStop = false;
            this.pyOptionsGroupBox.Text = "Python Assembly";
            // 
            // arcpyRadioButton
            // 
            this.arcpyRadioButton.AutoSize = true;
            this.arcpyRadioButton.Checked = true;
            this.arcpyRadioButton.Location = new System.Drawing.Point(18, 18);
            this.arcpyRadioButton.Name = "arcpyRadioButton";
            this.arcpyRadioButton.Size = new System.Drawing.Size(51, 17);
            this.arcpyRadioButton.TabIndex = 7;
            this.arcpyRadioButton.TabStop = true;
            this.arcpyRadioButton.Text = "arcpy";
            this.arcpyRadioButton.UseVisualStyleBackColor = true;
            this.arcpyRadioButton.CheckedChanged += new System.EventHandler(this.arcpyRadioButton_CheckedChanged);
            // 
            // arcgisscriptingRadioButton
            // 
            this.arcgisscriptingRadioButton.AutoSize = true;
            this.arcgisscriptingRadioButton.Location = new System.Drawing.Point(18, 39);
            this.arcgisscriptingRadioButton.Name = "arcgisscriptingRadioButton";
            this.arcgisscriptingRadioButton.Size = new System.Drawing.Size(92, 17);
            this.arcgisscriptingRadioButton.TabIndex = 8;
            this.arcgisscriptingRadioButton.Text = "arcgisscripting";
            this.arcgisscriptingRadioButton.UseVisualStyleBackColor = true;
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.Controls.Add(this.pyOptionsGroupBox);
            this.optionsGroupBox.Controls.Add(this.tbxOptionsGroupBox);
            this.optionsGroupBox.Controls.Add(this.aoOptionsGroupBox);
            this.optionsGroupBox.Controls.Add(this.gpOptionsGroupBox);
            this.optionsGroupBox.Location = new System.Drawing.Point(12, 27);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(594, 95);
            this.optionsGroupBox.TabIndex = 13;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Execution Options";
            // 
            // ArcPadSampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 434);
            this.Controls.Add(this.optionsGroupBox);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this.msgListBox);
            this.Controls.Add(this.checkInButton);
            this.Controls.Add(this.openAxfButton);
            this.Controls.Add(this.checkOutButton);
            this.Controls.Add(this.gdbButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ArcPadSampleForm";
            this.Text = "ArcPad Engine Sample";
            this.tbxOptionsGroupBox.ResumeLayout(false);
            this.tbxOptionsGroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gpOptionsGroupBox.ResumeLayout(false);
            this.gpOptionsGroupBox.PerformLayout();
            this.aoOptionsGroupBox.ResumeLayout(false);
            this.aoOptionsGroupBox.PerformLayout();
            this.pyOptionsGroupBox.ResumeLayout(false);
            this.pyOptionsGroupBox.PerformLayout();
            this.optionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button gdbButton;
        private System.Windows.Forms.Button checkOutButton;
        private System.Windows.Forms.Button openAxfButton;
        private System.Windows.Forms.Button checkInButton;
        private System.Windows.Forms.ListBox msgListBox;
        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.RadioButton radioButton93X;
        private System.Windows.Forms.RadioButton radioButton10X;
        private System.Windows.Forms.GroupBox tbxOptionsGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox gpOptionsGroupBox;
        private System.Windows.Forms.RadioButton aoRadioButton;
        private System.Windows.Forms.RadioButton pyRadioButton;
        private System.Windows.Forms.GroupBox aoOptionsGroupBox;
        private System.Windows.Forms.RadioButton managedRadioButton;
        private System.Windows.Forms.RadioButton unmanagedRadioButton;
        private System.Windows.Forms.GroupBox pyOptionsGroupBox;
        private System.Windows.Forms.RadioButton arcpyRadioButton;
        private System.Windows.Forms.RadioButton arcgisscriptingRadioButton;
        private System.Windows.Forms.GroupBox optionsGroupBox;
    }
}


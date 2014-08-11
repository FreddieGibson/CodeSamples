namespace CompoundAssignments
{
    partial class CompoundAssignments
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
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.gridSizeComboBox = new System.Windows.Forms.ComboBox();
            this.gridSizeLabel = new System.Windows.Forms.Label();
            this.drawGroupBox = new System.Windows.Forms.GroupBox();
            this.letterLabel = new System.Windows.Forms.Label();
            this.letterTextBox = new System.Windows.Forms.TextBox();
            this.settingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.letterTextBox);
            this.settingsGroupBox.Controls.Add(this.letterLabel);
            this.settingsGroupBox.Controls.Add(this.gridSizeComboBox);
            this.settingsGroupBox.Controls.Add(this.gridSizeLabel);
            this.settingsGroupBox.Location = new System.Drawing.Point(13, 13);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(428, 100);
            this.settingsGroupBox.TabIndex = 0;
            this.settingsGroupBox.TabStop = false;
            // 
            // gridSizeComboBox
            // 
            this.gridSizeComboBox.FormattingEnabled = true;
            this.gridSizeComboBox.Items.AddRange(new object[] {
            "4",
            "10",
            "20"});
            this.gridSizeComboBox.Location = new System.Drawing.Point(165, 41);
            this.gridSizeComboBox.Name = "gridSizeComboBox";
            this.gridSizeComboBox.Size = new System.Drawing.Size(42, 21);
            this.gridSizeComboBox.TabIndex = 1;
            this.gridSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.gridSizeComboBox_SelectedIndexChanged);
            // 
            // gridSizeLabel
            // 
            this.gridSizeLabel.AutoSize = true;
            this.gridSizeLabel.Location = new System.Drawing.Point(107, 44);
            this.gridSizeLabel.Name = "gridSizeLabel";
            this.gridSizeLabel.Size = new System.Drawing.Size(52, 13);
            this.gridSizeLabel.TabIndex = 0;
            this.gridSizeLabel.Text = "Grid Size:";
            // 
            // drawGroupBox
            // 
            this.drawGroupBox.Location = new System.Drawing.Point(13, 120);
            this.drawGroupBox.Name = "drawGroupBox";
            this.drawGroupBox.Size = new System.Drawing.Size(428, 447);
            this.drawGroupBox.TabIndex = 1;
            this.drawGroupBox.TabStop = false;
            this.drawGroupBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawGroupBox_Paint);
            // 
            // letterLabel
            // 
            this.letterLabel.AutoSize = true;
            this.letterLabel.Location = new System.Drawing.Point(246, 44);
            this.letterLabel.Name = "letterLabel";
            this.letterLabel.Size = new System.Drawing.Size(37, 13);
            this.letterLabel.TabIndex = 2;
            this.letterLabel.Text = "Letter:";
            // 
            // letterTextBox
            // 
            this.letterTextBox.Location = new System.Drawing.Point(289, 41);
            this.letterTextBox.MaxLength = 1;
            this.letterTextBox.Name = "letterTextBox";
            this.letterTextBox.Size = new System.Drawing.Size(32, 20);
            this.letterTextBox.TabIndex = 3;
            this.letterTextBox.TextChanged += new System.EventHandler(this.letterTextBox_TextChanged);
            // 
            // CompoundAssignments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 579);
            this.Controls.Add(this.drawGroupBox);
            this.Controls.Add(this.settingsGroupBox);
            this.Name = "CompoundAssignments";
            this.Text = "Compound Assignments";
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.ComboBox gridSizeComboBox;
        private System.Windows.Forms.Label gridSizeLabel;
        private System.Windows.Forms.GroupBox drawGroupBox;
        private System.Windows.Forms.TextBox letterTextBox;
        private System.Windows.Forms.Label letterLabel;
    }
}


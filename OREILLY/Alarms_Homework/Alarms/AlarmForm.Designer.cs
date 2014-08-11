namespace Alarms
{
    partial class AlarmForm
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
            this.alarmLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.hourNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.minuteNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.hourLabel = new System.Windows.Forms.Label();
            this.minuteLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.warningLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hourNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // alarmLabel
            // 
            this.alarmLabel.AutoSize = true;
            this.alarmLabel.Location = new System.Drawing.Point(12, 27);
            this.alarmLabel.Name = "alarmLabel";
            this.alarmLabel.Size = new System.Drawing.Size(36, 13);
            this.alarmLabel.TabIndex = 0;
            this.alarmLabel.Text = "Alarm:";
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(12, 57);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(52, 13);
            this.categoryLabel.TabIndex = 1;
            this.categoryLabel.Text = "Category:";
            // 
            // hourNumericUpDown
            // 
            this.hourNumericUpDown.Location = new System.Drawing.Point(73, 25);
            this.hourNumericUpDown.Name = "hourNumericUpDown";
            this.hourNumericUpDown.Size = new System.Drawing.Size(44, 20);
            this.hourNumericUpDown.TabIndex = 2;
            this.hourNumericUpDown.ValueChanged += new System.EventHandler(this.hourNumericUpDown_ValueChanged);
            // 
            // minuteNumericUpDown
            // 
            this.minuteNumericUpDown.Location = new System.Drawing.Point(150, 25);
            this.minuteNumericUpDown.Name = "minuteNumericUpDown";
            this.minuteNumericUpDown.Size = new System.Drawing.Size(44, 20);
            this.minuteNumericUpDown.TabIndex = 3;
            this.minuteNumericUpDown.ValueChanged += new System.EventHandler(this.minuteNumericUpDown_ValueChanged);
            // 
            // hourLabel
            // 
            this.hourLabel.AutoSize = true;
            this.hourLabel.Location = new System.Drawing.Point(123, 27);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(21, 13);
            this.hourLabel.TabIndex = 4;
            this.hourLabel.Text = "Hr.";
            // 
            // minuteLabel
            // 
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.Location = new System.Drawing.Point(200, 27);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new System.Drawing.Size(27, 13);
            this.minuteLabel.TabIndex = 5;
            this.minuteLabel.Text = "Min.";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(73, 54);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(149, 21);
            this.categoryComboBox.TabIndex = 6;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Location = new System.Drawing.Point(73, 84);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(65, 17);
            this.enabledCheckBox.TabIndex = 7;
            this.enabledCheckBox.Text = "Enabled";
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // warningLabel
            // 
            this.warningLabel.AutoSize = true;
            this.warningLabel.ForeColor = System.Drawing.Color.Red;
            this.warningLabel.Location = new System.Drawing.Point(88, 110);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(56, 13);
            this.warningLabel.TabIndex = 8;
            this.warningLabel.Text = "<warning>";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(42, 141);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(137, 141);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AlarmForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(239, 189);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.warningLabel);
            this.Controls.Add(this.enabledCheckBox);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.hourLabel);
            this.Controls.Add(this.minuteNumericUpDown);
            this.Controls.Add(this.hourNumericUpDown);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.alarmLabel);
            this.Name = "AlarmForm";
            this.Text = "Alarm";
            this.Shown += new System.EventHandler(this.AlarmForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.hourNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label alarmLabel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.NumericUpDown hourNumericUpDown;
        private System.Windows.Forms.NumericUpDown minuteNumericUpDown;
        private System.Windows.Forms.Label hourLabel;
        private System.Windows.Forms.Label minuteLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.Label warningLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
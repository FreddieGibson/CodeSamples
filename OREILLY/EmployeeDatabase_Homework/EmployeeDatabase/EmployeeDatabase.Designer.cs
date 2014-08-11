namespace EmployeeDatabase
{
    partial class EmployeeDatabase
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
            this.employeesListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // employeesListBox
            // 
            this.employeesListBox.FormattingEnabled = true;
            this.employeesListBox.Location = new System.Drawing.Point(12, 12);
            this.employeesListBox.Name = "employeesListBox";
            this.employeesListBox.Size = new System.Drawing.Size(260, 186);
            this.employeesListBox.TabIndex = 0;
            this.employeesListBox.SelectedIndexChanged += new System.EventHandler(this.employeesListBox_SelectedIndexChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 214);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(125, 35);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(147, 214);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(125, 35);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // EmployeeDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.employeesListBox);
            this.Name = "EmployeeDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Database";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox employeesListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
    }
}
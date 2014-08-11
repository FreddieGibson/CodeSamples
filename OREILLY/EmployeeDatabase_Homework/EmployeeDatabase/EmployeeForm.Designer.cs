namespace EmployeeDatabase
{
    partial class EmployeeForm
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
            this.idLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.employeeIDLabel = new System.Windows.Forms.Label();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.dateOfBirthLabel = new System.Windows.Forms.Label();
            this.address1Label = new System.Windows.Forms.Label();
            this.address2Label = new System.Windows.Forms.Label();
            this.cityLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.zipLabel = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.zipMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.addr1TextBox = new System.Windows.Forms.TextBox();
            this.addr2TextBox = new System.Windows.Forms.TextBox();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.phoneMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.phoneNumberLabel = new System.Windows.Forms.Label();
            this.dobMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(96, 20);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(45, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "pending";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(18, 328);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 35);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(134, 328);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 35);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // employeeIDLabel
            // 
            this.employeeIDLabel.AutoSize = true;
            this.employeeIDLabel.Location = new System.Drawing.Point(15, 20);
            this.employeeIDLabel.Name = "employeeIDLabel";
            this.employeeIDLabel.Size = new System.Drawing.Size(21, 13);
            this.employeeIDLabel.TabIndex = 3;
            this.employeeIDLabel.Text = "ID:";
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(15, 47);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(61, 13);
            this.lastNameLabel.TabIndex = 4;
            this.lastNameLabel.Text = "Last Name:";
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(15, 78);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(60, 13);
            this.firstNameLabel.TabIndex = 5;
            this.firstNameLabel.Text = "First Name:";
            // 
            // dateOfBirthLabel
            // 
            this.dateOfBirthLabel.AutoSize = true;
            this.dateOfBirthLabel.Location = new System.Drawing.Point(15, 109);
            this.dateOfBirthLabel.Name = "dateOfBirthLabel";
            this.dateOfBirthLabel.Size = new System.Drawing.Size(69, 13);
            this.dateOfBirthLabel.TabIndex = 6;
            this.dateOfBirthLabel.Text = "Date of Birth:";
            // 
            // address1Label
            // 
            this.address1Label.AutoSize = true;
            this.address1Label.Location = new System.Drawing.Point(15, 140);
            this.address1Label.Name = "address1Label";
            this.address1Label.Size = new System.Drawing.Size(57, 13);
            this.address1Label.TabIndex = 7;
            this.address1Label.Text = "Address 1:";
            // 
            // address2Label
            // 
            this.address2Label.AutoSize = true;
            this.address2Label.Location = new System.Drawing.Point(15, 171);
            this.address2Label.Name = "address2Label";
            this.address2Label.Size = new System.Drawing.Size(57, 13);
            this.address2Label.TabIndex = 8;
            this.address2Label.Text = "Address 2:";
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Location = new System.Drawing.Point(15, 202);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(27, 13);
            this.cityLabel.TabIndex = 9;
            this.cityLabel.Text = "City:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(15, 233);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(35, 13);
            this.stateLabel.TabIndex = 10;
            this.stateLabel.Text = "State:";
            // 
            // zipLabel
            // 
            this.zipLabel.AutoSize = true;
            this.zipLabel.Location = new System.Drawing.Point(15, 264);
            this.zipLabel.Name = "zipLabel";
            this.zipLabel.Size = new System.Drawing.Size(53, 13);
            this.zipLabel.TabIndex = 11;
            this.zipLabel.Text = "Zip Code:";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Location = new System.Drawing.Point(96, 44);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.lastNameTextBox.TabIndex = 0;
            // 
            // zipMaskedTextBox
            // 
            this.zipMaskedTextBox.Location = new System.Drawing.Point(96, 261);
            this.zipMaskedTextBox.Mask = "00000";
            this.zipMaskedTextBox.Name = "zipMaskedTextBox";
            this.zipMaskedTextBox.Size = new System.Drawing.Size(78, 20);
            this.zipMaskedTextBox.TabIndex = 8;
            // 
            // addr1TextBox
            // 
            this.addr1TextBox.Location = new System.Drawing.Point(96, 137);
            this.addr1TextBox.Name = "addr1TextBox";
            this.addr1TextBox.Size = new System.Drawing.Size(138, 20);
            this.addr1TextBox.TabIndex = 3;
            // 
            // addr2TextBox
            // 
            this.addr2TextBox.Location = new System.Drawing.Point(96, 168);
            this.addr2TextBox.Name = "addr2TextBox";
            this.addr2TextBox.Size = new System.Drawing.Size(138, 20);
            this.addr2TextBox.TabIndex = 4;
            // 
            // cityTextBox
            // 
            this.cityTextBox.Location = new System.Drawing.Point(96, 199);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(138, 20);
            this.cityTextBox.TabIndex = 6;
            // 
            // phoneMaskedTextBox
            // 
            this.phoneMaskedTextBox.Location = new System.Drawing.Point(96, 292);
            this.phoneMaskedTextBox.Mask = "(999) 000-0000";
            this.phoneMaskedTextBox.Name = "phoneMaskedTextBox";
            this.phoneMaskedTextBox.Size = new System.Drawing.Size(78, 20);
            this.phoneMaskedTextBox.TabIndex = 9;
            // 
            // phoneNumberLabel
            // 
            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Location = new System.Drawing.Point(15, 295);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Size = new System.Drawing.Size(81, 13);
            this.phoneNumberLabel.TabIndex = 20;
            this.phoneNumberLabel.Text = "Phone Number:";
            // 
            // dobMaskedTextBox
            // 
            this.dobMaskedTextBox.Location = new System.Drawing.Point(96, 109);
            this.dobMaskedTextBox.Mask = "00/00/0000";
            this.dobMaskedTextBox.Name = "dobMaskedTextBox";
            this.dobMaskedTextBox.Size = new System.Drawing.Size(78, 20);
            this.dobMaskedTextBox.TabIndex = 2;
            this.dobMaskedTextBox.ValidatingType = typeof(System.DateTime);
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Location = new System.Drawing.Point(96, 75);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.firstNameTextBox.TabIndex = 1;
            // 
            // stateTextBox
            // 
            this.stateTextBox.Location = new System.Drawing.Point(96, 230);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(78, 20);
            this.stateTextBox.TabIndex = 7;
            // 
            // EmployeeForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(249, 378);
            this.Controls.Add(this.stateTextBox);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.dobMaskedTextBox);
            this.Controls.Add(this.phoneMaskedTextBox);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.cityTextBox);
            this.Controls.Add(this.addr2TextBox);
            this.Controls.Add(this.addr1TextBox);
            this.Controls.Add(this.zipMaskedTextBox);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.zipLabel);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.address2Label);
            this.Controls.Add(this.address1Label);
            this.Controls.Add(this.dateOfBirthLabel);
            this.Controls.Add(this.firstNameLabel);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.employeeIDLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.idLabel);
            this.Name = "EmployeeForm";
            this.Text = "Employee Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label employeeIDLabel;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.Label dateOfBirthLabel;
        private System.Windows.Forms.Label address1Label;
        private System.Windows.Forms.Label address2Label;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label zipLabel;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.MaskedTextBox zipMaskedTextBox;
        private System.Windows.Forms.TextBox addr1TextBox;
        private System.Windows.Forms.TextBox addr2TextBox;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.MaskedTextBox phoneMaskedTextBox;
        private System.Windows.Forms.Label phoneNumberLabel;
        private System.Windows.Forms.MaskedTextBox dobMaskedTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox stateTextBox;
    }
}
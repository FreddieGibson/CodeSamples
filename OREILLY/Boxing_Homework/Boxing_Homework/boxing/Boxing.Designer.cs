namespace Boxing
{
    partial class Boxing
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
            this.boxingListBox = new System.Windows.Forms.ListBox();
            this.boxingButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boxingListBox
            // 
            this.boxingListBox.FormattingEnabled = true;
            this.boxingListBox.Location = new System.Drawing.Point(12, 12);
            this.boxingListBox.Name = "boxingListBox";
            this.boxingListBox.Size = new System.Drawing.Size(360, 199);
            this.boxingListBox.TabIndex = 0;
            // 
            // boxingButton
            // 
            this.boxingButton.Location = new System.Drawing.Point(108, 222);
            this.boxingButton.Name = "boxingButton";
            this.boxingButton.Size = new System.Drawing.Size(75, 23);
            this.boxingButton.TabIndex = 1;
            this.boxingButton.Text = "Box It";
            this.boxingButton.UseVisualStyleBackColor = true;
            this.boxingButton.Click += new System.EventHandler(this.boxingButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(201, 222);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // Boxing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.boxingButton);
            this.Controls.Add(this.boxingListBox);
            this.Name = "Boxing";
            this.Text = "Boxing";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox boxingListBox;
        private System.Windows.Forms.Button boxingButton;
        private System.Windows.Forms.Button closeButton;
    }
}


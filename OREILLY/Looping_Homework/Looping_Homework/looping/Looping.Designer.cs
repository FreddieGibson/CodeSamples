namespace Looping
{
    partial class Looping
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
            this.sentenceLabel = new System.Windows.Forms.Label();
            this.sentenceTextBox = new System.Windows.Forms.TextBox();
            this.countButton = new System.Windows.Forms.Button();
            this.letterCountLabel = new System.Windows.Forms.Label();
            this.reversedLabel = new System.Windows.Forms.Label();
            this.reversedTextBox = new System.Windows.Forms.TextBox();
            this.animateCheckBox = new System.Windows.Forms.CheckBox();
            this.reverseAnimationCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // sentenceLabel
            // 
            this.sentenceLabel.AutoSize = true;
            this.sentenceLabel.Location = new System.Drawing.Point(12, 18);
            this.sentenceLabel.Name = "sentenceLabel";
            this.sentenceLabel.Size = new System.Drawing.Size(82, 13);
            this.sentenceLabel.TabIndex = 0;
            this.sentenceLabel.Text = "Enter sentence:";
            // 
            // sentenceTextBox
            // 
            this.sentenceTextBox.Location = new System.Drawing.Point(12, 34);
            this.sentenceTextBox.Name = "sentenceTextBox";
            this.sentenceTextBox.Size = new System.Drawing.Size(149, 20);
            this.sentenceTextBox.TabIndex = 1;
            // 
            // countButton
            // 
            this.countButton.Location = new System.Drawing.Point(12, 60);
            this.countButton.Name = "countButton";
            this.countButton.Size = new System.Drawing.Size(75, 23);
            this.countButton.TabIndex = 2;
            this.countButton.Text = "Count";
            this.countButton.UseVisualStyleBackColor = true;
            this.countButton.Click += new System.EventHandler(this.countButton_Click);
            // 
            // letterCountLabel
            // 
            this.letterCountLabel.AutoSize = true;
            this.letterCountLabel.Location = new System.Drawing.Point(12, 96);
            this.letterCountLabel.Name = "letterCountLabel";
            this.letterCountLabel.Size = new System.Drawing.Size(67, 13);
            this.letterCountLabel.TabIndex = 3;
            this.letterCountLabel.Text = "Letter count:";
            // 
            // reversedLabel
            // 
            this.reversedLabel.AutoSize = true;
            this.reversedLabel.Location = new System.Drawing.Point(12, 114);
            this.reversedLabel.Name = "reversedLabel";
            this.reversedLabel.Size = new System.Drawing.Size(56, 13);
            this.reversedLabel.TabIndex = 4;
            this.reversedLabel.Text = "Reversed:";
            // 
            // reversedTextBox
            // 
            this.reversedTextBox.Location = new System.Drawing.Point(12, 130);
            this.reversedTextBox.Name = "reversedTextBox";
            this.reversedTextBox.ReadOnly = true;
            this.reversedTextBox.Size = new System.Drawing.Size(149, 20);
            this.reversedTextBox.TabIndex = 5;
            // 
            // animateCheckBox
            // 
            this.animateCheckBox.AutoSize = true;
            this.animateCheckBox.Location = new System.Drawing.Point(12, 157);
            this.animateCheckBox.Name = "animateCheckBox";
            this.animateCheckBox.Size = new System.Drawing.Size(64, 17);
            this.animateCheckBox.TabIndex = 6;
            this.animateCheckBox.Text = "Animate";
            this.animateCheckBox.UseVisualStyleBackColor = true;
            this.animateCheckBox.CheckedChanged += new System.EventHandler(this.animateCheckBox_CheckedChanged);
            // 
            // reverseAnimationCheckBox
            // 
            this.reverseAnimationCheckBox.AutoSize = true;
            this.reverseAnimationCheckBox.Location = new System.Drawing.Point(12, 180);
            this.reverseAnimationCheckBox.Name = "reverseAnimationCheckBox";
            this.reverseAnimationCheckBox.Size = new System.Drawing.Size(160, 17);
            this.reverseAnimationCheckBox.TabIndex = 7;
            this.reverseAnimationCheckBox.Text = "Reverse Animation Direction";
            this.reverseAnimationCheckBox.UseVisualStyleBackColor = true;
            // 
            // Looping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 242);
            this.Controls.Add(this.reverseAnimationCheckBox);
            this.Controls.Add(this.animateCheckBox);
            this.Controls.Add(this.reversedTextBox);
            this.Controls.Add(this.reversedLabel);
            this.Controls.Add(this.letterCountLabel);
            this.Controls.Add(this.countButton);
            this.Controls.Add(this.sentenceTextBox);
            this.Controls.Add(this.sentenceLabel);
            this.Name = "Looping";
            this.Text = "Looping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Looping_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Looping_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sentenceLabel;
        private System.Windows.Forms.TextBox sentenceTextBox;
        private System.Windows.Forms.Button countButton;
        private System.Windows.Forms.Label letterCountLabel;
        private System.Windows.Forms.Label reversedLabel;
        private System.Windows.Forms.TextBox reversedTextBox;
        private System.Windows.Forms.CheckBox animateCheckBox;
        private System.Windows.Forms.CheckBox reverseAnimationCheckBox;
    }
}


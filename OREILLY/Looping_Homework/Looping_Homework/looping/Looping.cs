using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Looping
{
    public partial class Looping : Form
    {
        private bool _doAnimation = false;

        public Looping()
        {
            InitializeComponent();
            reverseAnimationCheckBox.Enabled = false;
        }

        private void countButton_Click(object sender, EventArgs e)
        {
            int letterCount = 0;
            
            // Use for loop to find and count letters only.
            for (int i = 0; i < sentenceTextBox.Text.Length; i++)
            {
                // Determine if we have a character.
                if (char.IsLetter(sentenceTextBox.Text, i))
                    letterCount++;

                letterCountLabel.Text = "Letter count: " + letterCount;

                // use while loop to reverse.
                StringBuilder reverseSB = new StringBuilder(sentenceTextBox.Text.Length);

                int letterPosition = sentenceTextBox.Text.Length;
                while (letterPosition > 0)
                {
                    // Add character, working from end of string backwards.
                    reverseSB.Append(sentenceTextBox.Text.Substring(--letterPosition, 1));
                }

                reversedTextBox.Text = reverseSB.ToString();
            }
        }

        private void Looping_Paint(object sender, PaintEventArgs e)
        {
            string drawText = sentenceTextBox.Text.Length > 0 ? sentenceTextBox.Text : "Some text";

            // Determine our approximate stopping point
            float maxWidth = e.Graphics.VisibleClipBounds.Width / 2;
            
            // Set up starting x and y locations, noting that only the x is going to change.
            float x = reverseAnimationCheckBox.Left;
            float y = reverseAnimationCheckBox.Top + (reverseAnimationCheckBox.Height * 2);
            float z = reverseAnimationCheckBox.Right / 2;

            // We'll need a previous x location so we can 'erase' the previous drawing.
            float previousX = x;

            // Loop until either we've reached our stopping pint, or doAnimation is false.
            do
            {
                // Draw text at old location.
                e.Graphics.DrawString(drawText, new Font("Arial", 8), new SolidBrush(BackColor), previousX, y);
                
                // Make the system sleep for a short time so we can see the animation.
                System.Threading.Thread.Sleep(300);

                // Allow the program to process events so we can allow the UI to be responsive after sleeping
                Application.DoEvents();

                if (!reverseAnimationCheckBox.Checked)
                {
                    // Draw text at new location.
                    e.Graphics.DrawString(drawText, new Font("Arial", 8), new SolidBrush(Color.Red), x, y);

                    // Save the current location
                    previousX = x;
                }
                else
                {
                    // Draw text at new location.
                    e.Graphics.DrawString(drawText, new Font("Arial", 8), new SolidBrush(Color.Red), z, y);

                    // Save the current location
                    previousX = z;
                }

                // Advance to next location
                x++;
                z--;

            } while (x < maxWidth && _doAnimation);

            // Do some cleanup.
            e.Graphics.DrawString(drawText, new Font("Arial", 8), new SolidBrush(BackColor), previousX, y);
            animateCheckBox.Checked = false;
            reverseAnimationCheckBox.Checked = false;
            _doAnimation = false;
        }

        private void animateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Determine if we are to animate, hten force paint event to occur using Refresh.
            _doAnimation = animateCheckBox.Checked;
            reverseAnimationCheckBox.Enabled = animateCheckBox.Checked;
            Refresh();
        }

        private void Looping_FormClosing(object sender, FormClosingEventArgs e)
        {
            // We need to prevent, or cancel, closing, as code may still be in animation do loop.
            if (!_doAnimation) return;
            MessageBox.Show("You must stop any animation before trying to close the program", this.Text, MessageBoxButtons.OK);
            e.Cancel = true;
        }
    }
}

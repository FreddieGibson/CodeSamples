using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlphabetAquarium
{
    public partial class AlphabetAquarium : Form
    {
        private FishTank _fishTank = new FishTank();

        public AlphabetAquarium()
        {
            InitializeComponent();
        }

        private void moveFish()
        {
            int xMax = fishTankPanel.Bounds.Width - 10;
            int xMin = 10;

            foreach (Fish fish in _fishTank)
            {
                if (fish.Direction == "R")
                {
                    int limit = xMax - 1;
                    fish.XPosition = (fish.XPosition == limit) ? fish.XPosition - 1 : fish.XPosition + 1;
                    fish.Direction = (fish.XPosition == limit) ? "L" : "R";
                }
                else
                {
                    int limit = xMin + 1;
                    fish.XPosition = (fish.XPosition == limit) ? fish.XPosition + 1 : fish.XPosition - 1;
                    fish.Direction = (fish.XPosition == limit) ? "R" : "L";
                }
            }
        }

        private void animateTimer_Tick(object sender, EventArgs e)
        {
            moveFish();
            fishTankPanel.Invalidate();
        }

        private void animateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            animateTimer.Enabled = animateCheckBox.Checked;
        }

        private void AlphabetAquarium_Load(object sender, EventArgs e)
        {
            // Populate "colors" ComboBox, select "Black" as default.
            object[] colors = {"Black", "Red", "Green", "Blue"};
            colorsComboBox.Items.AddRange(colors);
            colorsComboBox.SelectedIndex = colorsComboBox.FindString("Black");

            // Populate the "fish" ComboBox, select "A" as default.
            object[] letters = { "A", "B", "C", "D" };
            fishComboBox.Items.AddRange(letters);
            fishComboBox.SelectedIndex = fishComboBox.FindString("A");
        }

        private void colorsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change the color of the colorPictureBox to match the selected color.
            colorPictureBox.BackColor = Color.FromName(colorsComboBox.SelectedItem.ToString());
        }

        private void addFishButton_Click(object sender, EventArgs e)
        {
            // Use the boundaries of the fishTankPanel to limit our random x, y location.
            Rectangle fishTankRect = fishTankPanel.Bounds;
            Random random = new Random();

            int x = random.Next(10, fishTankRect.Width - 10);
            int y = random.Next(10, fishTankRect.Height - 10);
            
            // Create a new fish object, and add to our fish tank.
            Fish fish = new Fish(fishComboBox.SelectedItem.ToString(), x, y, Color.FromName(colorsComboBox.SelectedItem.ToString()));
            _fishTank.AddFish(fish);
            fishTankPanel.Invalidate();
        }

        private void fishTankPanel_Paint(object sender, PaintEventArgs e)
        {
            // Loop through each fish in our fish tank and draw them.
            for (int i = 0; i < _fishTank.CountFish(); i++)
            {
                Fish fish = _fishTank.GetFish(i);
                e.Graphics.DrawString(fish.FishLetter, new Font("Arial", 10), new SolidBrush(fish.FishColor), new Point(fish.XPosition, fish.YPosition));
                fishCountLabel.Text = _fishTank.CountFish().ToString();
            }
        }

        private void clearFishButton_Click(object sender, EventArgs e)
        {
            _fishTank.ClearFish();
            fishTankPanel.Invalidate();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

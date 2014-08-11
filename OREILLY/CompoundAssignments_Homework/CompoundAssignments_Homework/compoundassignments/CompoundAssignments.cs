using System;
using System.Drawing;
using System.Windows.Forms;

namespace CompoundAssignments
{
    public partial class CompoundAssignments : Form
    {
        private const float GridMargin = 10.0F;

        public CompoundAssignments()
        {
            InitializeComponent();

            // Select the first item in the combo box.
            gridSizeComboBox.SelectedIndex = 0;

            // Prefix
            string prefixValues = "0 ";
            int result = 0;
            int value = 1;
            for (int i = 1; i <= 3; i++)
            {
                result += ++value;
                prefixValues += result + " ";
            }

            // Postfix
            string postfixValues = "0 ";
            result = 0;
            value = 1;
            for (int i = 1; i <= 3; i++)
            {
                result += value++;
                postfixValues += result + " ";
            }

            Console.WriteLine("Prefix: " + prefixValues);
            Console.WriteLine("Postfix: " + postfixValues);
        }

        private void DrawLetter(Graphics drawingArea, string letter, int gridSize)
        {
            //Make sure we have a grid size > 0 before we attempt to draw grid.
            if (gridSize <= 0) return;

            // Use the smaller of the drawing area width or height to ensure a unifor grid spacing.
            float gridWidth = drawingArea.VisibleClipBounds.Width;
            if (drawingArea.VisibleClipBounds.Height < gridWidth)
                gridWidth = drawingArea.VisibleClipBounds.Height;

            // subtract the margin value
            gridWidth -= GridMargin*2;

            // Determine grid cell width
            float cellWidth = gridWidth/gridSize;

            // Get the centroid distance for each letter
            double centroidDist = cellWidth/2;
            float fontSize;

            switch (gridSize)
            {
                case 4:
                    fontSize = 16.0F;
                    break;
                case 10:
                    fontSize = 12.0F;
                    break;
                case 20:
                    fontSize = 7.5F;
                    break;
                default:
                    fontSize = 10.0F;
                    break;
            }

            // Draw each letter in appropiate place
            for (int i = 0; i < gridSize; i++)
            {
                double x = (i*cellWidth) + centroidDist;
                for (int j = 0; j < gridSize; j++)
                {
                    double y = (j*cellWidth) + centroidDist;
                    drawingArea.DrawString(letter, new Font("Arial", fontSize, FontStyle.Bold), new SolidBrush(Color.Black), (float)x, (float)y);
                }
            }
        }

        private void DrawGrid(Graphics drawingArea)
        {
            int gridSize = 0; // Grid size, i.e. 4x4, 10x10, etc.

            // Try to convert he grid size in the combo box to an integer
            if (!Int32.TryParse(gridSizeComboBox.Text, out gridSize)) return;
            
            //Make sure we have a grid size > 0 before we attempt to draw grid.
            if (gridSize <= 0) return;

            // Create pen object for drawing
            Pen redPen = new Pen(Color.Red, 2);
            Pen bluePen = new Pen(Color.Blue, 2);

            // Use the smaller of the drawing area width or height to ensure a unifor grid spacing.
            float gridWidth = drawingArea.VisibleClipBounds.Width;
            if (drawingArea.VisibleClipBounds.Height < gridWidth)
                gridWidth = drawingArea.VisibleClipBounds.Height;

            // subtract the margin value
            gridWidth -= GridMargin*2;

            // Determine grid cell width
            float cellWidth = gridWidth/gridSize;

            // Set up starting x and y coordinates for both horizontal and vertical lines
            float horizontalX = GridMargin;
            float horizontalY = 0;
            float verticalX = 0;
            float verticalY = GridMargin;

            // The number of lines in grid is equal to the size of the grid plus 1, so loop over size of grid + 1, drawing horizontal and vertical lines
            for (int i = 0; i < gridSize + 1; i++)
            {
                // Select pen, red for even, blue for odd.
                Pen pen = (i%2 == 0) ? redPen : bluePen;

                // Draw horizontal lines
                Console.WriteLine("Horizontal: ({0},{1}) to ({2},{3})", horizontalX, horizontalY + GridMargin,
                    horizontalX + gridWidth, horizontalY + GridMargin);

                drawingArea.DrawLine(pen, horizontalX, horizontalY + GridMargin, horizontalX + gridWidth, horizontalY + GridMargin);
                horizontalY += cellWidth;

                // Draw vertical lines.
                Console.WriteLine("Vertical: ({0}, {1}) to ({2},{3})", verticalX + GridMargin, verticalY, verticalX + GridMargin, verticalY + gridWidth);
                drawingArea.DrawLine(pen, verticalX + GridMargin, verticalY, verticalX + GridMargin, verticalY + gridWidth);
                verticalX += cellWidth;
            }
        }

        private void gridSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Notify the group box control that it should redraw itself, which will call the Pain event below.
            drawGroupBox.Refresh();
        }

        private void drawGroupBox_Paint(object sender, PaintEventArgs e)
        {
            // Call the base class OnPaint event to ensure any other controls that need to be redrawn are also redrawn.
            base.OnPaint(e);
            
            // Call our draw grid method.
            DrawGrid(e.Graphics);

            string letter = letterTextBox.Text;
            if (letter == string.Empty || letter == " ")
                return;


            int gridSize = 0; // Grid size, i.e. 4x4, 10x10, etc.

            // Try to convert he grid size in the combo box to an integer
            if (Int32.TryParse(gridSizeComboBox.Text, out gridSize))
            {
                DrawLetter(e.Graphics, letter, gridSize);
            }
        }

        private void letterTextBox_TextChanged(object sender, EventArgs e)
        {
            drawGroupBox.Refresh();
        }
    }
}

using System;
using System.Windows.Forms;

namespace Boxing
{
    public partial class Boxing : Form
    {
        public class Sphere
        {
            // Properties
            public static double Dimesions { get; private set; }
            public static double Radius { get; set; }

            // Constructor
            public Sphere(double radius) { Radius = radius; Dimesions = radius; }

            // Clone
            public Sphere() : this(0.0) { }
            public Sphere(Sphere sphere) : this(Radius) { }
            public Sphere Clone() { return new Sphere(this); }

            // reference
            public Sphere Reference() { return this; }

            // Calculate volume of sphere (4/3 * PI * radius cubed)
            public static double Volume() { return 4/3 * Math.PI * Math.Pow(Radius, 3); }
        }

        public class Box
        {
            // Automatic class properties.
            public double Length { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }

            // Typically constructor used with double, double, double fingerprint.
            public Box(double Length, double Width, double Height)
            {
                this.Length = Length;
                this.Width = Width;
                this.Height = Height;
            }

            // Default constructor, using this to call constructor with double, double, double fingerprint,
            // note using explicit 0.0 double liter to prevent any forced type conversions, and
            // placing empty method body curly braces on same line to conserve space
            public Box() : this(0.0, 0.0, 0.0) { }

            // Private constructor that takes a Box as a parameter, used to clone itself.
            private Box(Box Box) : this(Box.Length, Box.Width, Box.Height) { }

            // A method to return a cloned version of the current Box.
            // Note that cloning is rarely this simple!
            public Box Clone() { return new Box(this); }

            // Calculate volume of box.
            public double Volume()
            {
                return this.Length*this.Width*this.Height;
            }
        }

        class SquareBox
        {
            // box field.
            public Box Box;

            // Automatic property for single dimension value (length, width, and height are all the same.)
            public double Dimension { get; set; }

            // Constructor with double fingerprint
            // Note that we use this constructor to create our Box object.
            public SquareBox(double Dimension)
            {
                this.Box = new Box(Dimension, Dimension, Dimension);
                this.Dimension = Dimension;
            }

            // Default constructor, calls constructor with double fingerprint, passing double literal
            public SquareBox() : this(0.0) { }

            // Return a reference to the current object.
            public SquareBox Reference()
            {
                return this;
            }
        }

        public Boxing()
        {
            InitializeComponent();
        }

        private void boxingButton_Click(object sender, EventArgs e)
        {
            // Fun with box.
            boxingListBox.Items.Add("Creating box...");
            Box box = new Box(5, 10, 15);
            boxingListBox.Items.Add("Cloning box as newBox...");
            Box newBox = box.Clone();
            boxingListBox.Items.Add("Do box and newBox reference same object? " + ReferenceEquals(box, newBox));
            boxingListBox.Items.Add("Setting box to null...");
            box = null;
            boxingListBox.Items.Add("Is box null? " + (box == null));
            boxingListBox.Items.Add("Is newBox null? " + (newBox == null));
            boxingListBox.Items.Add("Volume : " + newBox.Volume());

            // Fun with SquareBox.
            boxingListBox.Items.Add(""); // Empty line
            boxingListBox.Items.Add("Creating squareBox...");
            SquareBox squareBox = new SquareBox(20);
            SquareBox newSquareBox = squareBox.Reference();
            boxingListBox.Items.Add("Do squareBox and newSquareBox reference same object? " + ReferenceEquals(squareBox, newSquareBox));
            boxingListBox.Items.Add("Setting squareBox to null...");
            squareBox = null;
            boxingListBox.Items.Add("Is squareBox null? " + (squareBox == null));
            boxingListBox.Items.Add("Is newSquareBox null? " + (newSquareBox == null));

            // Fun with Sphere.
            boxingListBox.Items.Add("");
            boxingListBox.Items.Add("Creating sphere...");
            Sphere sphere = new Sphere(30);
            Sphere newSphere = sphere.Reference();
            boxingListBox.Items.Add("Do sphere and newSphere reference the same object? " + ReferenceEquals(sphere, newSphere));
            boxingListBox.Items.Add("Setting sphere to null");
            sphere = null;
            boxingListBox.Items.Add("Is sphere null? " + (sphere == null));
            boxingListBox.Items.Add("is newSphere null? " + (newSphere == null));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

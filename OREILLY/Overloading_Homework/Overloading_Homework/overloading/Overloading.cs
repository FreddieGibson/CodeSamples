using System;
using System.Windows.Forms;

namespace Overloading
{
    public partial class Overloading : Form
    {
        private Bag _myBag = new Bag();
        
        public Overloading()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Overloading_Shown(object sender, EventArgs e)
        {
            // Declare some content data.
            const string lastName = "LastName";
            const int age = 21;
            const long mileage = 101600;
            const float pi = 3.14159265359F;

            // Add the content data to our permanent bag.
            _myBag.Add(age);
            _myBag.Add(lastName);
            _myBag.Add(mileage);
            _myBag.Add(pi);

            // Create a temporary bag, and add string and integer literal contents.
            Bag tempBag = new Bag();
            tempBag.Add("Second Bag String");
            tempBag.Add(100);
            tempBag.Add(999999);
            tempBag.Add(123.456F);

            // Add the contents of our temporary bag to our permanent bag.
            _myBag += tempBag;

            // Output the contents of our temporary bag to the ListBox.
            foreach (object contents in _myBag.Items)
            {
                outputListBox.Items.Add(contents.ToString());
            }

            // Create a clone of our permanent bag.
            Bag clonedBag;
            int itemsInClone = _myBag.Clone(out clonedBag);

            // Output the contents of the cloned bag to the ListBox.
            outputListBox.Items.Add("");
            foreach (object contents in clonedBag.Items)
            {
                outputListBox.Items.Add(contents.ToString());
            }

            // Display count of items in cloned bag to the ListBox
            outputListBox.Items.Add("Cloned count: " + itemsInClone);

            outputListBox.Items.Add("");

            // Add items to temporary bag that can be removed from permanent bag
            tempBag.Add(lastName);
            tempBag.Add(age);

            // Subtract the contents of our temporary bag from our permanent bag
            _myBag -= tempBag;

            // Output the contents of our temporary bag to the ListBox.
            foreach (object contents in _myBag.Items)
            {
                outputListBox.Items.Add(contents.ToString());
            }
        }
    }
}

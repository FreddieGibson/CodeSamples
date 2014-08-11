using System.Collections.Generic;
using System.Linq;

namespace Overloading
{
    class Bag
    {
        // Class variables
        private readonly List<object> _items;

        // Accessors
        public List<object> Items
        {
            get { return _items; }
        }

        // Constructors
        public Bag()
        {
            _items = new List<object>();
        }

        // Public methods
        public int Clone(out Bag resultBag)
        {
            // Create our new out Bag.
            resultBag = new Bag();
            
            // Copy the contents to our new out Bag
            foreach (object contents in _items)
            {
                resultBag.Items.Add(contents);
            }

            // Return the count of items in our new out Bag.
            return resultBag.Items.Count;
        }

        // Overloaded methods
        public void Add(int contents)
        {
            _items.Add(contents);
        }

        public void Add(string contents)
        {
            _items.Add(contents);
        }

        public void Add(long contents)
        {
            _items.Add(contents);
        }

        public void Add(float contents)
        {
            _items.Add(contents);
        }

        // Overloaded operators
        public static Bag operator +(Bag firstBag, Bag secondBag)
        {
            // Make sure we have data in both of our bags.
            if (firstBag == null || secondBag == null) return firstBag;
            
            // Add contents from second bag to first bag.
            foreach (object contents in secondBag.Items)
            {
                firstBag.Items.Add(contents);
            }

            // Return first bag (including added second bag contents).
            return firstBag;
        }

        public static Bag operator -(Bag firstBag, Bag secondBag)
        {
            // Make sure we have data in both of our bags.
            if (firstBag == null || secondBag == null) return firstBag;

            // Subtract contents in second bag from first bag.
            foreach (object contents in secondBag.Items.Where(contents => firstBag.Items.Contains(contents)))
            {
                firstBag.Items.Remove(contents);
            }

            // Return first bag (including added second bag contents).
            return firstBag;
        }
    }
}

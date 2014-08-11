using System;

namespace MyShoes
{
    public class Shoe
    {
        /// <summary>
        /// Shoe's Size
        /// </summary>
        private int _size;
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// Shoe's Color
        /// </summary>
        private System.Drawing.Color _color;
        public System.Drawing.Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        /// <summary>
        /// Shoe's Manufacturer
        /// </summary>
        private string _manufacturer;
        public string Manufacturer
        {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }

        /// <summary>
        /// Shoe's Style
        /// </summary>
        private string _style;
        public string Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// Shoe's Date Purchased
        /// </summary>
        private DateTime _datePurchased;
        public DateTime DatePurchased
        {
            get { return _datePurchased; }
            set { _datePurchased = value; }
        }
    }
}

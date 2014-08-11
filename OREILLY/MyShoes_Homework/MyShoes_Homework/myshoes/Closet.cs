using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShoes
{

    public class Closet
    {
        /// <summary>
        /// Collection of shoes
        /// </summary>
        private List<Shoe> _shoes;
        public List<Shoe> Shoes
        {
            get { return _shoes; }
            set { _shoes = value; }
        }

        
        /// <summary>
        /// Number of shoes in closest
        /// </summary>
        private int _count;
        public int Count
        {
            get { return _count; }
        }
    }
}

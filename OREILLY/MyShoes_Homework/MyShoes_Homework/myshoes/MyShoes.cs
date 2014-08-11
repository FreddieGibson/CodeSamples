using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyShoes
{
    public partial class MyShoes : Form
    {

        private Closet _myCloset = new Closet();

        public MyShoes()
        {
            InitializeComponent();
        }
    }
}

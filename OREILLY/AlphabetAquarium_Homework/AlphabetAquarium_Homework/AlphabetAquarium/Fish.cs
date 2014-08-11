using System;
using System.Drawing;

namespace AlphabetAquarium
{
    class Fish
    {
        private Color _fishColor;

        public Color FishColor
        {
            get { return _fishColor; }
            set { _fishColor = value; }
        }

        private int _xPosition;

        public int XPosition
        {
            get { return _xPosition; }
            set { _xPosition = value;  }
        }

        private int _yPosition;

        public int YPosition
        {
            get { return _yPosition; }
            set { _yPosition = value; }
        }

        private string _fishLetter;

        public string FishLetter
        {
            get { return _fishLetter; }
            set { _fishLetter = value; }
        }

        private string _direction;

        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }


        public Fish(string fishLetter, int xPosition, int yPosition, Color fishColor)
        {
            // If no letter specified, use "X."
            _fishLetter = (fishLetter.Length == 0) ? "X" : fishLetter;

            // Ensure the position is >= 0.
            _xPosition = (xPosition < 0) ? 0 : xPosition;

            // Ensure the position is >= 0.
            _yPosition = (yPosition < 0) ? 0 : yPosition;

            // Assign the fish color
            _fishColor = fishColor;

            // Set the direction
            Random random = new Random();
            _direction = (random.Next(1, 50) <= 25) ? "L" : "R";
        }
    }
}

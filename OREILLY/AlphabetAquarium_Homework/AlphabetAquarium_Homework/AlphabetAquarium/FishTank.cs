using System;
using System.Collections;
using System.Collections.Generic;

namespace AlphabetAquarium
{
    class FishTank : IEnumerable
    {
        // Use a List collection to hold the fish.
        private List<Fish>  _fishTank = new List<Fish>();

        public int CountFish()
        {
            return _fishTank.Count;
        }

        public Fish GetFish(int position)
        {
            return _fishTank[position];
        }

        public void AddFish(Fish fish)
        {
            _fishTank.Add(fish);
        }

        public void ClearFish()
        {
            _fishTank.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var fish in _fishTank)
            {
                if (fish == null) break;
                yield return fish;
            }
        }
    }
}

using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class Randomizer
    {
        public static void ShuffleList<T>(List<T> list)
        {
            Random rand = new Random();

            // Shuffle the list using a Fisher-Yates shuffle algorithm
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

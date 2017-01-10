using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.DigitsRecognizer
{
    public class ManhattanDistance : IDistance
    {
        public double Between(int[] first, int[] second)
        {
            if (first.Length != second.Length)
            {
                throw new ArgumentException("Inconsistent images sizes.");
            }
            var length = first.Length;
            var distance = 0;
            for (int i = 0; i < length; i++)
            {
                distance += Math.Abs(first[i] - second[i]);
            }
            return distance;
        }
    }
}

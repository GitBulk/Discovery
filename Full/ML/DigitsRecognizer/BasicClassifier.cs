using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.DigitsRecognizer
{
    public class BasicClassifier : IClassifier
    {
        private IEnumerable<Observation> data;
        private readonly IDistance distance;

        public BasicClassifier(IDistance distance)
        {
            this.distance = distance;
        }

        public string Predict(int[] input)
        {
            Observation closestMatching = null;
            var shortest = double.MaxValue;
            foreach (Observation item in this.data)
            {
                var dist = this.distance.Between(item.Pixels, input);
                if (dist < shortest)
                {
                    shortest = dist;
                    closestMatching = item;
                }
            }
            return closestMatching.Label;
        }

        public void ImportData(IEnumerable<Observation> dataSet)
        {
            this.data = dataSet;
        }
    }
}

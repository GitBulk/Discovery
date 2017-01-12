using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.DigitsRecognizer
{
    public class FunctionalDistance
    {
        private IEnumerable<Observation> data;

        private readonly Func<int[], int[], int> distance;

        public FunctionalDistance(Func<int[], int[], int> distance)
        {
            this.distance = distance;
        }

        public Func<int[], int[], int> Distance
        {
            get
            {
                return this.distance;
            }
        }

        public void ImportData(IEnumerable<Observation> dataSet)
        {
            this.data = dataSet;
        }

        public string Predict(int[] pixels)
        {
            Observation currentBest = null;
            var shortest = Double.MaxValue;
            foreach (Observation obs in this.data)
            {
                var dist = this.Distance(obs.Pixels, pixels);
                if (dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }
            return currentBest.Label;
        }
    }
}

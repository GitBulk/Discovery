using System.Collections.Generic;
using System.Linq;

namespace ML.DigitsRecognizer
{
    public class Evaluator
    {
        public static double Correct(IEnumerable<Observation> validationSet,
            IClassifier classifier)
        {
            return validationSet.Select(obs => Score(obs, classifier)).Average();
        }

        private static double Score(Observation obs, IClassifier classifier)
        {
            if (classifier.Predict(obs.Pixels) == obs.Label)
            {
                return 1;
            }
            return 0;
        }
    }
}

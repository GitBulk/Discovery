using System.Collections.Generic;

namespace ML.DigitsRecognizer
{
    public interface IClassifier
    {
        void ImportData(IEnumerable<Observation> dataSet);
        string Predict(int[] input);
    }
}

using System;
using System.IO;
using System.Linq;

namespace ML.DigitsRecognizer
{
    public class DataReader
    {
        private static Observation ObservationFactory(string data)
        {
            var commaSeperated = data.Split(',');
            var label = commaSeperated[0];
            var pixels = commaSeperated.Skip(1).Select(x => Convert.ToInt32(x)).ToArray();
            return new Observation(label, pixels);
        }

        public static Observation[] ReadObservations(string dataPath)
        {
            var data = File.ReadAllLines(dataPath).Skip(1).Select(ObservationFactory).ToArray();
            return data;
        }
    }
}

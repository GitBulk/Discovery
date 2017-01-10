using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.DigitsRecognizer
{
    public static class Process
    {
        public static void Invoke()
        {
            string inputDataPath = Path.Combine(Directory.GetCurrentDirectory(), @"DigitsRecognizer\Data\trainingsample.csv");
            string validationDataPath = Path.Combine(Directory.GetCurrentDirectory(), @"DigitsRecognizer\Data\validationsample.csv");
            //Console.WriteLine(Directory.GetCurrentDirectory());

            var trainingData = DataReader.ReadObservations(inputDataPath);
            var validationData = DataReader.ReadObservations(validationDataPath);

            var classifier = new BasicClassifier(new ManhattanDistance());
            classifier.ImportData(trainingData);

            var correct = Evaluator.Correct(validationData, classifier);
            Console.WriteLine("Correctly classifies: {0:P2}", correct);
            Console.ReadLine();
        }
    }
}

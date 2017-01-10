namespace ML.DigitsRecognizer
{
    public class Observation
    {
        public Observation(string label, int[] pixels)
        {
            this.Label = label;
            this.Pixels = pixels;
        }

        public string Label { get; set; }
        public int[] Pixels { get; set; }
    }
}

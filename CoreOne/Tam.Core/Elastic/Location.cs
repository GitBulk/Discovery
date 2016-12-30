namespace Tam.Core.Elastic
{
    public class Location
    {
        public int CityId { get; set; }
        public string City { get; set; }

        public string Zip { get; set; }
        public string Type { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string AreaCodes { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string WorldRegion { get; set; }

        public string Country { get; set; }

        public int EstimatedPopulation { get; set; }

        public Coordinates Coordinates { get; set; }
    }
}

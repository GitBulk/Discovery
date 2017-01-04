using System;

namespace AzureCoreOne.Models.VueWorkShop
{
    public class Product
    {
        public Guid Id { get; set; }
        public string imageUrl { get; set; }
        public string imageName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }
}

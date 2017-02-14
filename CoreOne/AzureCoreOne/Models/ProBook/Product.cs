using ProtoBuf;

namespace AzureCoreOne.Models.ProBook
{
    [ProtoContract(SkipConstructor = true)]
    public class Product
    {
        [ProtoMember(1, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public int ProductId { get; set; }
        [ProtoMember(2, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public string Name { get; set; }
        [ProtoMember(3, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public string Description { get; set; }
        [ProtoMember(4, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public decimal Price { get; set; }
        [ProtoMember(5, DataFormat = DataFormat.FixedSize, IsRequired = true)]
        public string Category { get; set; }


        //public static Product[] GetProducts()
        //{
        //    Product kayak = new Product
        //    {
        //        Name = "Kayak",
        //        Price = 275M
        //    };
        //    Product lifejacket = new Product
        //    {
        //        Name = "Lifejacket",
        //        Price = 48.95M
        //    };
        //    return new Product[] { kayak, lifejacket, null };
        //}
    }
}

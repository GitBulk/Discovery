using ProtoBuf;

namespace AzureCoreOne.Models.ProBook
{
    [ProtoContract(SkipConstructor = true)]
    public class Product
    {
        [ProtoMember(1)]
        public int ProductId { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Description { get; set; }
        [ProtoMember(4)]
        public decimal Price { get; set; }
        [ProtoMember(5)]
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

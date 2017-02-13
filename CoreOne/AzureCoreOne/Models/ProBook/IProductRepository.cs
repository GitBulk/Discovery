using System.Collections.Generic;

namespace AzureCoreOne.Models.ProBook
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}

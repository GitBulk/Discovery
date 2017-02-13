using AzureCoreOne.AppContexts;
using System.Collections.Generic;

namespace AzureCoreOne.Models.ProBook
{
    public class EFProductRepository : IProductRepository
    {
        private TamContext context;
        public EFProductRepository(TamContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products => context.Products;
    }
}

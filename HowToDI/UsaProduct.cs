using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowToDI
{
    public class UsaProduct : IProduct
    {
        public string Build()
        {
            return "USA Product";
        }
    }

    public interface IProductService
    {
        string RunService();
    }

    public class UsaProductService : IProductService
    {
        

        public string RunService()
        {
            throw new NotImplementedException();
        }
    }
}
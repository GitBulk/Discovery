using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowToDI
{
    public class VietnamProduct : IProduct
    {
        public string Build()
        {
            return "Vietnam Product";
        }
    }
}
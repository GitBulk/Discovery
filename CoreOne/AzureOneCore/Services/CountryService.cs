using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureOneCore.Services
{
    public class CountryService
    {
        public List<string> Provinces()
        {
            return new List<string>
            {
                "Sai Gon",
                "Ha Noi",
                "Hue",
                "Can Tho"
            };
        }
    }
}

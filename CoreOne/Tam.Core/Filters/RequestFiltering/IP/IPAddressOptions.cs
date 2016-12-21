using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Filters.RequestFiltering.IP
{
    public class IPAddressOptions: IRequestFilterOptions
    {
        public List<string> IPAddresses { get; set; }
    }
}

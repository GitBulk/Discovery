using System.Collections;
using System.Collections.Generic;

namespace Tam.Core.Filters.RequestFiltering
{
    public class RequestFiltMaintenanceWindowerOptions
    {
        public IList<IRequestFilter> Filters { get; set; } = new List<IRequestFilter>();
    }
}

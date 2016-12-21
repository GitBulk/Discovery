using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Filters
{
    public static class RequestFilteringOptionsExtensions
    {
        public static RequestFilteringOptions AddRequestFilter(this RequestFilteringOptions options, IRequestFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            options.Filters.Add(filter);
            return options;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Filters.RequestFiltering
{
    public interface IRequestFilter
    {
        void ApplyFilter(RequestFilteringContext context);
    }

    public interface IRequestFilter<T>: IRequestFilter { }
}

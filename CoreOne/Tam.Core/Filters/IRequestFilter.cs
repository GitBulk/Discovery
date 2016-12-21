using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Filters
{
    public interface IRequestFilter
    {
        void ApplyFilter(RequestFilteringContext context);
    }

    public interface IRequestFilter<T>: IRequestFilter { }
}

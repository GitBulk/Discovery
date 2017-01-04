using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Filters.RequestFiltering
{
    public abstract class RequestFilter<TOptions> : IRequestFilter<TOptions> where TOptions : IRequestFilterOptions
    {

        public abstract TOptions Options { get; }

        public virtual void ApplyFilter(RequestFilteringContext context)
        {
            context.Result = RequestFilterResult.Continue;
        }
    }
}

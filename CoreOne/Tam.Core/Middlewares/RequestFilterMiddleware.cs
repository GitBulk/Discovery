using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Tam.Core.Filters.RequestFiltering;
using Tam.Core.Utilities;

namespace Tam.Core.Middlewares
{
    public class RequestFilterMiddleware
    {
        private readonly RequestDelegate next;
        private readonly RequestFiltMaintenanceWindowerOptions options;

        public RequestFilterMiddleware(RequestDelegate next, RequestFiltMaintenanceWindowerOptions options)
        {
            Guard.ThrowIfNull(next);
            Guard.ThrowIfNull(options);
            this.next = next;
            this.options = options;
        }

        public Task Invoke(HttpContext context)
        {
            Guard.ThrowIfNull(context);
            var requestFilteringContext = new RequestFilteringContext
            {
                HttpContext = context,
                Result = RequestFilterResult.Continue
            };
            foreach (IRequestFilter filter in this.options.Filters)
            {
                filter.ApplyFilter(requestFilteringContext);
                switch (requestFilteringContext.Result)
                {
                    case RequestFilterResult.Continue:
                        break;
                    case RequestFilterResult.StopFilters:
                        return Task.FromResult(0);
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid filter termination {requestFilteringContext.Result}");
                }
            }
            return this.next(context);
        }
    }
}

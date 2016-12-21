using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Tam.Core.Utilities;

namespace Tam.Core.Filters
{
    public class RequestFilteringMiddleware
    {
        private readonly RequestDelegate next;
        private readonly RequestFilteringOptions options;

        public RequestFilteringMiddleware(RequestDelegate next, RequestFilteringOptions options)
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
                Result = RequestFilteringResult.Continue
            };
            foreach (IRequestFilter filter in this.options.Filters)
            {
                filter.ApplyFilter(requestFilteringContext);
                switch (requestFilteringContext.Result)
                {
                    case RequestFilteringResult.Continue:
                        break;
                    case RequestFilteringResult.StopFilters:
                        return Task.FromResult(0);
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid filter termination {requestFilteringContext.Result}");
                }
            }
            return this.next(context);
        }
    }
}

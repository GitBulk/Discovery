using Microsoft.AspNetCore.Http;

namespace Tam.Core.Filters.RequestFiltering
{
    public class RequestFilteringContext
    {
        public HttpContext HttpContext { get; set; }
        public RequestFilterResult Result { get; set; }
    }
}
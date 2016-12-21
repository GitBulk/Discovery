using Microsoft.AspNetCore.Http;

namespace Tam.Core.Filters
{
    public class RequestFilteringContext
    {
        public HttpContext HttpContext { get; set; }
        public RequestFilteringResult Result { get; set; }
    }
}
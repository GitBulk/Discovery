using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Utilities
{
    public static class HttpContextHelper
    {
        public static IHttpConnectionFeature GetConnectionFeature(this HttpContext context)
        {
            return context.Features.Get<IHttpConnectionFeature>();
        }
    }
}

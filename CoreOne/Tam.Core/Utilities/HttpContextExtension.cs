using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tam.Core.Utilities
{
    public static class HttpContextExtension
    {
        public static IHttpConnectionFeature GetHttpConnectionFeature(this HttpContext context)
        {
            return context.Features.Get<IHttpConnectionFeature>();
        }

        public static void AddHttpResponseHeader(this HttpContext context, string key, string value)
        {
            Guard.ThrowIfNullOrWhiteSpace(key);
            context.Response.Headers.Add(key, value);
        }
        public static void AddHttpResponseHeader(this ResultExecutingContext context, string key, string value)
        {
            context.HttpContext.AddHttpResponseHeader(key, value);
        }
    }
}

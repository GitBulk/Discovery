using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Middlewares
{
    public abstract class BaseMiddleware
    {
        protected readonly RequestDelegate next;

        public BaseMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public virtual Task Invoke(HttpContext context)
        {
            return this.next(context);
        }
    }
}

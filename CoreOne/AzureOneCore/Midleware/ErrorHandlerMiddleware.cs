using AzureOneCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureOneCore.Midleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (HttpStatusCodeException exception)
            {
                context.Response.StatusCode = (int)exception.StatusCode;
                context.Response.Headers.Clear();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

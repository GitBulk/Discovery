using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using AzureOneCore.Models;

namespace AzureOneCore.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger logger;

        public GlobalExceptionFilter(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            this.logger = loggerFactory.CreateLogger("Global Exception Filter");
        }

        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse()
            {
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = 500,
                DeclaredType = typeof(ErrorResponse)
            };
            this.logger.LogError("GlobalExceptionFilter", context.Exception);
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}

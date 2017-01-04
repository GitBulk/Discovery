using AzureCoreOne.Filter;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.Filter
{
    public class WhitespaceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var response = context.HttpContext.Response;
            if (context.HttpContext.Request.GetDisplayUrl().Contains("sitemap.xml"))
            {
                return;
            }

            if (response.Body == null)
            {
                return;
            }
            response.Body = new ResponseFilterStream(response.Body);
        }
    }
}

using HowToAudiTrail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HowToAudiTrail.Filters
{
    public class AuditAttribute : ActionFilterAttribute
    {
        // http://rion.io/2013/03/03/implementing-audit-trails-using-asp-net-mvc-actionfilters/
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            string userName = "Anonymous";
            if (request.IsAuthenticated)
            {
                userName = filterContext.HttpContext.User.Identity.Name;
            }
            var audit = new Audit
            {
                AuditID = Guid.NewGuid(),
                UserName = userName,
                CreatedDate = DateTime.UtcNow,
                IpAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                AreaAccessed = request.RawUrl
            };

            var context = new SampleEntities();
            context.Audits.Add(audit);
            context.SaveChanges();


            // using
            // [Audit]
            //public ActionResult AuditedAction()
            //{
            //    return Content("Audit Fired!");
            //}
            base.OnActionExecuting(filterContext);
        }

    }
}
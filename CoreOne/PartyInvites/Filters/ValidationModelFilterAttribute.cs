using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PartyInvites.Filters
{
    public class ValidationModelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            else
            {
                base.OnActionExecuting(context);
            }

        }
    }
}

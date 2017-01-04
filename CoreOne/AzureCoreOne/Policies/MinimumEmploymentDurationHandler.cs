using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tam.Core.Utilities;

namespace AzureCoreOne.Policies
{
    public class MinimumEmploymentDurationHandler : AuthorizationHandler<EmploymentDurationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmploymentDurationRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "EmploymentDate" && c.Issuer == "http://github.com/gitbulk"))
            {
                return Task.FromResult(0);
            }
            var employmentdate = Convert.ToDateTime(context.User.FindFirst(c => c.Type == "EmploymentDate" &&
           c.Issuer == "http://github.com/gitbulk").Value);
            int numerOfMonth = ((DateTimeHelper.GetCurrentSystemDate().Year - employmentdate.Year) * 12) + DateTimeHelper.GetCurrentSystemDate().Month - employmentdate.Month;
            if (numerOfMonth > requirement.MinimumMonths)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}

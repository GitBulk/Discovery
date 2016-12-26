using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Tam.Core.Utilities;

namespace AzureCoreOne.Policies
{
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == ClaimIssuers.GitPublisher))
            {
                return Task.FromResult(0);
            }
            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth && c.Issuer == ClaimIssuers.GitPublisher));

            int calulatedAge = DateTimeHelper.GetCurrentSystemDate().Year - dateOfBirth.Year;
            if (dateOfBirth > DateTimeHelper.GetCurrentSystemDate().AddYears(-calulatedAge))
            {
                calulatedAge--;
            }
            if (calulatedAge >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}

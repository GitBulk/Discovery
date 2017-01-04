using AzureCoreOne.Models.Indentities;
using AzureCoreOne.Models.Parsley;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.Policies
{
    public class EditSkiCardAuthorizationHandler : AuthorizationHandler<EditSkiCardAuthorizationRequirement, SkiCard>
    {
        private readonly UserManager<ApplicationUser> UserManager;

        public EditSkiCardAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            this.UserManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EditSkiCardAuthorizationRequirement requirement, SkiCard resource)
        {
            string userId = this.UserManager.GetUserId(context.User);
            if (resource.ApplicationUserId == userId)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}

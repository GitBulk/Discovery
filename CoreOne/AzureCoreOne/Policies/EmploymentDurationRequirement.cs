using Microsoft.AspNetCore.Authorization;

namespace AzureCoreOne.Policies
{
    public class EmploymentDurationRequirement : IAuthorizationRequirement
    {
        public int MinimumMonths { get; set; }
        public EmploymentDurationRequirement(int minimumMonths)
        {
            this.MinimumMonths = minimumMonths;
        }
    }
}

using Microsoft.AspNetCore.Authorization;

namespace AzureCoreOne.Policies
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int age)
        {
            this.MinimumAge = age;
        }

        public int MinimumAge { get; set; }
    }
}

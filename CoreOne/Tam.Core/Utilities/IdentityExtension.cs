using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tam.Core.Utilities
{
    public static class IdentityExtension
    {
        public static IServiceCollection AddIdentity<TIdentityUser, TIdentityRole, TContext>(this IServiceCollection services, bool useDefaultTokenProvider = true)
            where TIdentityUser: IdentityUser
            where TIdentityRole: IdentityRole
            where TContext: DbContext
        {

            if (useDefaultTokenProvider)
            {
                services.AddIdentity<TIdentityUser, TIdentityRole>().AddEntityFrameworkStores<TContext>().AddDefaultTokenProviders();
            }
            else
            {
                services.AddIdentity<TIdentityUser, TIdentityRole>().AddEntityFrameworkStores<TContext>();
            }
            return services;
        }
    }
}

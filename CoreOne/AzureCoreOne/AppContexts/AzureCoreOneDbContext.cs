using AzureCoreOne.Models.Indentities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.AppContexts
{
    public class AzureCoreOneDbContext : IdentityDbContext<ApplicationUser>
    {
        public AzureCoreOneDbContext(DbContextOptions<AzureCoreOneDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

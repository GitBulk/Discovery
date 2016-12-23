using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tam.Core.Utilities
{
    public static class EntityFrameworkCoreExtension
    {
        public static IServiceCollection AddSqlServerDbContext<T>(this IServiceCollection service, string connectionString) where T: DbContext
        {
            service.AddDbContext<T>(o => o.UseSqlServer(connectionString));
            return service;
        }

    }
}

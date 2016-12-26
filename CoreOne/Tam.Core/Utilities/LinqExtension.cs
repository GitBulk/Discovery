using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tam.Core.Utilities
{
    public static class LinqExtension
    {
        public static bool AnyOrNull<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source == null)
            {
                return false;
            }
            return source.Any(predicate);
        }
    }
}

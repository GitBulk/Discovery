using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tam.Core.Filters.RequestFiltering.Files;
using Tam.Core.Filters.RequestFiltering.IP;
using Tam.Core.Utilities;

namespace Tam.Core.Filters.RequestFiltering
{
    public static class RequestFilterOptionsExtensions
    {
        public static RequestFilterOptions AddRequestFilter(this RequestFilterOptions options, IRequestFilter filter)
        {
            Guard.ThrowIfNull(filter);
            options.Filters.Add(filter);
            return options;
        }


        public static RequestFilterOptions AddIpFilter(this RequestFilterOptions filterOptions, IPAddressOptions options)
        {
            Guard.ThrowIfNull(options);
            var filter = new IPAddresRequestFilter(options);
            filterOptions.AddRequestFilter(filter);
            return filterOptions;
        }

        public static RequestFilterOptions AddFileFilter(this RequestFilterOptions filterOptions, FileExtensionsOptions options)
        {
            Guard.ThrowIfNull(options);
            var filter = new FileExtensionRequestFilter(options);
            return filterOptions.AddRequestFilter(filter);
        }
    }
}

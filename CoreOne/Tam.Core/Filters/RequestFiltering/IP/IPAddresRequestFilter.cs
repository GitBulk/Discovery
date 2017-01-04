using Tam.Core.Utilities;

namespace Tam.Core.Filters.RequestFiltering.IP
{
    public class IPAddresRequestFilter : RequestFilter<IPAddressOptions>
    {
        public override IPAddressOptions Options
        {
            get;
        }

        public IPAddresRequestFilter(IPAddressOptions options)
        {
            this.Options = options;
        }

        public override void ApplyFilter(RequestFilteringContext context)
        {
            var connection = context.HttpContext.GetHttpConnectionFeature();
            if (connection == null)
            {
                context.Result = RequestFilterResult.Continue;
            }
            var userIp = connection.RemoteIpAddress.ToString();
            if (this.Options.IPAddresses.Contains(userIp))
            {
                context.HttpContext.Response.StatusCode = 404;
                context.Result = RequestFilterResult.StopFilters;
            }
            else
            {
                context.Result = RequestFilterResult.Continue;
            }

        }
    }
}

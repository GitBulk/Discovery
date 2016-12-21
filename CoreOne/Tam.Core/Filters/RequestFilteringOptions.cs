using System.Collections;
using System.Collections.Generic;

namespace Tam.Core.Filters
{
    public class RequestFilteringOptions
    {
        public IList Filters { get; set; } = new List<IRequestFilter>();
    }
}

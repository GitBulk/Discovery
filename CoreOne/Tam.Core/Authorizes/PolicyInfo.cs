using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.Authorizes
{
    public class PolicyInfo
    {
        public string RequiredRole { get; set; }
        public string PolicyName { get; set; }
        public string[] Claims { get; set; }
    }
}

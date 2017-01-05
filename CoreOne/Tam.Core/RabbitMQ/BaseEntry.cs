using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tam.Core.RabbitMQ
{
    public abstract class BaseEntry
    {
        public string Id { get; set; }
        public string What { get; set; }
        public DateTime When { get; set; }
        public string Who { get; set; }
    }
}

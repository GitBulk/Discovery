using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.Models.Parsley
{
    public class PassActivation
    {
        public int Id { get; set; }
        public int PassId { get; set; }
        public int ScanId { get; set; }
        public Scan Scan { get; set; }
    }
}

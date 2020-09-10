using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkRouteMonitor.Model
{
    public class Connection
    {
        public int HostId1 { get; set; }
        public int HostId2 { get; set; }
        public int? TTL { get; set; }
    }
}

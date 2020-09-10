using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkRouteMonitor.Model
{
    public class Network
    {
        //public List<string> Hosts { get; set; }
        public HashSet<(string, string)> Connections { get; set; }
        public Dictionary<string,int> Addresses { get; set; }

        public Network()
        {
            Connections = new HashSet<(string, string)>();
            Addresses = new Dictionary<string,int>();
        }
    }
}

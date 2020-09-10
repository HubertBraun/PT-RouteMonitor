using PacketDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkRouteMonitor.Model
{
    public class UnifiedPacket
    {
        public bool ARP { get; set; }
        public ProtocolType Type { get; set; }
        public string IPAddressSrc { get; set; }
        public string IPAddressDst { get; set; }
        public string MACAddressSrc { get; set; }
        public string MACAddressDst { get; set; }
        public int? TTL { get; set; }
        public DateTime CaptureTime { get; set; }
    }
}

using NetworkRouteMonitor.Model;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NetworkRouteMonitor.Model.UnifiedPacket;

namespace NetworkRouteMonitor.Helper
{
    public static class PacketExtractor
    {
        private static List<UnifiedPacket> _globalPackets = new List<UnifiedPacket>();
        public static UnifiedPacket Extract(Packet packet, DateTime date)
        {
            var unifiedPacket = new UnifiedPacket
            {
                CaptureTime = date,
                ARP = false
            };
            if (packet is EthernetPacket)
            {
                var eth = (EthernetPacket)packet;
                unifiedPacket.MACAddressDst = eth.DestinationHardwareAddress.ToString();
                unifiedPacket.MACAddressSrc = eth.SourceHardwareAddress.ToString();
                var ip = packet.Extract<IPPacket>();
                if (ip != null)
                {
                    unifiedPacket.IPAddressDst = ip.DestinationAddress.ToString();
                    unifiedPacket.IPAddressSrc = ip.SourceAddress.ToString();
                    unifiedPacket.Type = ip.Protocol;
                    unifiedPacket.TTL = ip.TimeToLive;
                }
                
                var arp = packet.Extract<ArpPacket>();
                if (arp != null)
                {
                    unifiedPacket.IPAddressDst = arp.TargetProtocolAddress.ToString();
                    unifiedPacket.IPAddressSrc = arp.SenderProtocolAddress.ToString();
                    unifiedPacket.ARP = true;
                }
                if(arp == null && ip == null)
                {
                    Console.WriteLine(packet.ToString());
                }
                else
                {
                    AddPacket(unifiedPacket);
                }
            }
            else
            {
                Console.WriteLine(packet.ToString());
            }
            return unifiedPacket;
        }
        public static void AddPacket(UnifiedPacket packet)
        {
            _globalPackets.Add(packet);
        }
        public static void ClearPackets()
        {
            _globalPackets.Clear();
        }
        public static List<UnifiedPacket> GetPackets()
        {
            return new List<UnifiedPacket>(_globalPackets);
        }
    }
}

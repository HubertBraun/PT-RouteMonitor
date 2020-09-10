using NetworkRouteMonitor.Model;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkRouteMonitor.Helper
{
    public class DataReader
    {
        private CaptureDeviceList _devices;
        public static ICaptureDevice ChoosenDevice { get; private set; }
        public delegate void Data(object sender, UnifiedPacket e);
        public event Data DataReceived;
        public List<RawCapture> RawCapturedPacked { get; private set; }
        public DataReader()
        {
            RawCapturedPacked = new List<RawCapture>();
        }
        public List<string> GetInterfaces()
        {
            var interfaces = new List<string>();
            _devices = CaptureDeviceList.Instance;
            foreach (var dev in _devices)
            {
                var name = Regex.Match(dev.ToString(), "FriendlyName: (.*)").Groups[1].Value;
                //var description = Regex.Match(dev.ToString(), "Description: (.*)").Groups[1].Value;
                //var i = description + name;
                interfaces.Add(name);
                Console.WriteLine(name);
            }
            return interfaces;
        }
        public void SetDevice(int index)
        {
            ChoosenDevice = _devices[index];
        }
        public void SetDevice(ICaptureDevice device)
        {
            ChoosenDevice = device;
        }
        public void CaptureData()
        {
            ChoosenDevice.OnPacketArrival += new PacketArrivalEventHandler(OnPacketReceive);

            int readTimeoutMilliseconds = 1000;
            ChoosenDevice.Open();//DeviceMode.Promiscuous, readTimeoutMilliseconds);
            ChoosenDevice.Capture();
            //ChoosenDevice.Close();
        }
        public void StopCapturingData()
        {
            ChoosenDevice.Close();
        }
        public void OnPacketReceive(object sender, CaptureEventArgs args)
        {
            RawCapturedPacked.Add(args.Packet);
            var packet = Packet.ParsePacket(args.Packet.LinkLayerType, args.Packet.Data);
            DataReceived?.Invoke(null, PacketExtractor.Extract(packet, args.Packet.Timeval.Date));
        }
    }
}

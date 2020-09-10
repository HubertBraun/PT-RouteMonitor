using NetworkRouteMonitor.Helper;
using NetworkRouteMonitor.Model;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkRouteMonitor
{
    public partial class MapForm : Form
    {
        private DataReader _reader = new DataReader();
        private Graphics _graphics;
        private Network _network = new Network();
        private Timer _refreshMapTimer = new Timer();
        StringFormat format = new StringFormat();
        public MapForm()
        {
            InitializeComponent();
            _graphics = CreateGraphics();
            _refreshMapTimer.Interval = 5000; // 5 sek
            _refreshMapTimer.Enabled = true;
            _refreshMapTimer.Tick += new EventHandler(RefreshMap);
        }

        private void LogButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _reader.DataReceived += DataReceived;
            var t = new Task(() => { _reader.CaptureData(); });
            t.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _reader.StopCapturingData();
        }

        /*private int CalculatePointOnCircle(int r, int y)
        {
            return (int)Math.Sqrt((r * r) - (y * y));
        }
        */
        private Point GetPoint(int index, int blocksOnColumn)
        {
            var y = (index * ConfigVariables.BlockHeight) % Height;
            y += ConfigVariables.UpperMargin;
            var x = ConfigVariables.BlockWidth * (index / (blocksOnColumn));
            x += ConfigVariables.LeftMargin;
            return new Point(x,y);
        }
        private void RefreshMap(object sender, EventArgs e)
        {
            lock (_graphics)
            {
                _graphics.Clear(Color.FromArgb(0xF0, 0xF0, 0xF0));
                var blocksOnColumn = Height / ConfigVariables.BlockHeight;
                foreach (var addr in _network.Addresses)
                {
                    var point = GetPoint(addr.Value, blocksOnColumn);
                    _graphics.DrawString(addr.Key, Font, Brushes.Black, point.X, point.Y, format);
                }
                var pen = new Pen(Brushes.Black);
                foreach (var con in _network.Connections)
                {
                    var id1 = _network.Addresses[con.Item1];
                    var id2 = _network.Addresses[con.Item2];
                    var point1 = GetPoint(id1, blocksOnColumn);
                    var point2 = GetPoint(id2, blocksOnColumn);
                    if (point2.X > point1.X)
                    {
                        (point1, point2) = (point2, point1);
                    }
                    if (point1.X > point2.X)
                    {
                        point2.X += ConfigVariables.BlockWidth * 2 / 3;
                    }
                    point1.Y += ConfigVariables.BlockHeight / 2;
                    point2.Y += ConfigVariables.BlockHeight / 2;
                    if (point1.X == point2.X)
                    {
                        var point3 = new Point(point1.X - ConfigVariables.LeftMargin / 2, (point1.Y + point2.Y) / 2);
                        _graphics.DrawCurve(pen, new Point[] { point1, point3, point2 });
                    }
                    else
                    {
                        var point3 = new Point((point1.X + point2.X) / 2, ((point1.Y + point2.Y) / 2) - ConfigVariables.BlockHeight);
                        _graphics.DrawCurve(pen, new Point[] { point1, point3, point2 });
                    }

                }
            }
        }


        private void DataReceived(object sender, UnifiedPacket packet)
        {
            if (string.IsNullOrEmpty(packet.IPAddressDst) || string.IsNullOrEmpty(packet.IPAddressSrc))
                return;
            lock (_graphics)
            {
                var hostSrc = new Host();
                var addr1 = packet.IPAddressSrc;
                var addr2 = packet.IPAddressDst;
                if (string.Compare(packet.IPAddressDst, packet.IPAddressSrc) < 0)
                {
                    addr1 = packet.IPAddressDst;
                    addr2 = packet.IPAddressSrc;
                }
                if (!_network.Connections.Contains((addr1, addr2)))
                {
                    _network.Connections.Add((addr1, addr2));
                }
                if (!_network.Addresses.ContainsKey(addr1))
                {
                    _network.Addresses.Add(addr1,_network.Addresses.Count);
                }
                if (!_network.Addresses.ContainsKey(addr2))
                {
                    _network.Addresses.Add(addr2, _network.Addresses.Count);
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            lock (_graphics)
            {
                _graphics.Clear(Color.FromArgb(0xF0, 0xF0, 0xF0));
            }
        }
        private void OnResize(object sender, EventArgs e)
        {
            if (_graphics != null)
            {
                lock (_graphics)
                {
                    _graphics = CreateGraphics();
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Pcap Files (*.pcap)|*.pcap",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var writer = new CaptureFileWriterDevice(sfd.FileName);
                writer.Open();
                foreach (var packet in _reader.RawCapturedPacked)
                {
                    writer.Write(packet);
                }
                writer.Close();
            }
        }

        private void Combine_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "pcap | *.pcap";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var capFile = openFileDialog.FileName;
                    try
                    {
                        var device = new CaptureFileReaderDevice(capFile);
                        device.Open();
                        device.OnPacketArrival += new PacketArrivalEventHandler(CombineArrivalEvent);
                        device.StartCapture();
                    }
                    catch (Exception exc)
                    {

                    }
                }
            }
        }

        private void CombineArrivalEvent(object sender, CaptureEventArgs e)
        {
            _reader.OnPacketReceive(sender, e);
        }

    }
}

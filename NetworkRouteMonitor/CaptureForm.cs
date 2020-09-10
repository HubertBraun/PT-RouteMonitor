using NetworkRouteMonitor.Helper;
using NetworkRouteMonitor.Model;
using PacketDotNet;
using SharpPcap;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkRouteMonitor
{
    public partial class CaptureForm : Form
    {
        private DataReader _reader = new DataReader();
        public CaptureForm()
        {
            InitializeComponent();
            Frames.Size = new Size(Width - 35, Height - 80);
        }

        private void StartCapturing(object sender, EventArgs e)
        {
            _reader.DataReceived += DataReceived;
            var t = new Task(() => { _reader.CaptureData(); });
            t.Start();
        }

        private string Pad(object o, int padding)
        {
            return (o != null) ? o.ToString().PadRight(padding) : "".PadRight(padding);
        }

        private void DataReceived(object sender, UnifiedPacket packet)
        {
            Frames.Invoke(new MethodInvoker(delegate
            {
                var frame = "";
                if (packet.ARP)
                {
                    frame = $"{Pad("ARP", 20)} {Pad(packet.IPAddressSrc, 40)} {Pad(packet.IPAddressDst, 40)} {Pad(packet.CaptureTime, 20)}";
                }
                else
                {
                    frame = $"{Pad(packet.Type, 20)} {Pad(packet.IPAddressSrc, 40)} {Pad(packet.IPAddressDst, 40)} {Pad(packet.CaptureTime, 20)}";
                }
                Frames.Items.Add(frame);
            }));
        }

        private void StopCapturing(object sender, EventArgs e)
        {
            _reader.StopCapturingData();
        }

        private void Frames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void Clear_Click(object sender, EventArgs e)
        {
            Frames.Items.Clear();
        }
        private void ResizeForm(object sender, EventArgs e)
        {
            if(Width > 200 && Height > 200)
                Frames.Size = new Size(Width - 35, Height - 80);
        }

        private void MapButton_Click(object sender, EventArgs e)
        {
            var frm = new MapForm
            {
                Location = Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { Show(); };
            frm.Show();
            Hide();
        }
    }
}

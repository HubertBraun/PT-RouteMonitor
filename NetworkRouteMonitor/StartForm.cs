using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkRouteMonitor.Helper;
using NetworkRouteMonitor.Model;
using SharpPcap;
using SharpPcap.LibPcap;

namespace NetworkRouteMonitor
{
    public partial class StartForm : Form
    {
        private DataReader _reader = new DataReader();
        public StartForm()
        {
            InitializeComponent();
            if (InterfacesList.Items != null)
            {
                InterfacesList.Items.Clear();
                InterfacesList.Items.Add("Loading interfaces");
            }
            var t = new Task(() =>
            {
                _reader = new DataReader();
                var interfaces = _reader.GetInterfaces();
                // dostęp do elementu winforms musi odbywać się z tego samego wątku, z którego element zostął utworzony
                InterfacesList.Invoke(new MethodInvoker(delegate
                {
                    InterfacesList.Items.Clear();
                }));
                foreach (var i in interfaces)
                {
                    InterfacesList.Invoke(new MethodInvoker(delegate
                    {
                        InterfacesList.Items.Add(i);
                    }));
                }
            });
            t.Start();
        }

        private void SelectInterface(object sender, EventArgs e)
        {
            var index = InterfacesList.SelectedIndex;
            _reader.SetDevice(index);
            var frm = new MapForm
            {
                Location = Location,
                StartPosition = FormStartPosition.Manual
            };
            frm.FormClosing += delegate { Show(); };
            frm.Show();
            Hide();
        }

        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
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
                        //device.Open();
                        _reader.SetDevice(device);
                        var frm = new MapForm
                        {
                            Location = Location,
                            StartPosition = FormStartPosition.Manual
                        };
                        frm.FormClosing += delegate { Show(); };
                        frm.Show();
                        Hide();
                    }
                    catch (Exception exc)
                    {

                    }
                }
            }
        }
    }
}

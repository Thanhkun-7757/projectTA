using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProjectTA
{
    public partial class Form1 : Form
    {
        private string deviceId;
        private string scrcpyPath = @"C:\Users\Admin\Documents\job\projectTA\ProjectTA\scrcpy-win64-v3.1\scrcpy-win64-v3.1\scrcpy.exe";

        public Form1(string deviceId)
        {
            //InitializeComponent();
            this.deviceId = deviceId;
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // Thiết lập layout
        //    Panel streamPanel = new Panel
        //    {
        //        Dock = DockStyle.Left,
        //        Width = this.Width / 2,
        //        BorderStyle = BorderStyle.FixedSingle
        //    };

        //    Panel logPanel = new Panel
        //    {
        //        Dock = DockStyle.Fill,
        //        BorderStyle = BorderStyle.FixedSingle
        //    };

        //    RichTextBox logBox = new RichTextBox
        //    {
        //        Dock = DockStyle.Fill
        //    };
        //    logPanel.Controls.Add(logBox);

        //    this.Controls.Add(logPanel);
        //    this.Controls.Add(streamPanel);

        //    // Bắt đầu stream điện thoại đầu tiên
        //    StartScrcpyForPhone(deviceId, streamPanel, streamPanel.Width, streamPanel.Height, scrcpyPath);
        //}

        private void StartScrcpyForPhone(string deviceId, Panel phonePanel, int panelWidth, int panelHeight, string scrcpyPath)
        {
            try
            {
                Process scrcpyProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = scrcpyPath,
                        Arguments = $"--serial {deviceId} --window-title scrcpy_{deviceId} --window-borderless",
                        UseShellExecute = true,
                        CreateNoWindow = false
                    }
                };
                scrcpyProcess.Start();

                System.Threading.Thread.Sleep(2000);

                IntPtr scrcpyHandle = IntPtr.Zero;
                int retries = 0;

                while (scrcpyHandle == IntPtr.Zero && retries < 10)
                {
                    scrcpyHandle = FindWindow(null, $"scrcpy_{deviceId}");
                    System.Threading.Thread.Sleep(500);
                    retries++;
                }

                if (scrcpyHandle != IntPtr.Zero)
                {
                    SetParent(scrcpyHandle, phonePanel.Handle);
                    MoveWindow(scrcpyHandle, 0, 0, panelWidth, panelHeight, true);
                }
                else
                {
                    MessageBox.Show("Không thể lấy handle của scrcpy.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chạy scrcpy: " + ex.Message);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Panel streamPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = this.Width / 2,
                BorderStyle = BorderStyle.FixedSingle
            };

            Panel logPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            RichTextBox logBox = new RichTextBox
            {
                Dock = DockStyle.Fill
            };
            logPanel.Controls.Add(logBox);

            this.Controls.Add(logPanel);
            this.Controls.Add(streamPanel);

            // Bắt đầu stream điện thoại đầu tiên
            StartScrcpyForPhone(deviceId, streamPanel, streamPanel.Width, streamPanel.Height, scrcpyPath);
        }
    }
}

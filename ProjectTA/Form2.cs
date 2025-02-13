using System;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;



namespace ProjectTA
{
    public partial class Form2 : Form
    {
        //public Form2()
        //{
        //    InitializeComponent();
        //}
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Khi nhấn "Chụp tọa độ"
        private void buttonCapture_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int x1 = rnd.Next(100, 500);
            int y1 = rnd.Next(100, 500);
            int x2 = rnd.Next(100, 500);
            int y2 = rnd.Next(100, 500);

            textBoxCoordinates.Text = $"({x1}:{y1},{x2}:{y2})";
        }

        // Khi nhấn "Bắt đầu"
        private void buttonStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kịch bản đã bắt đầu: " + textBoxScript.Text);
        }

        // Khi nhấn "Lưu tọa độ"
        private void buttonSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tọa độ đã lưu: " + textBoxCoordinates.Text);
        }
        
        private Size GetPhoneScreenSize(string deviceId)
        {
            try
            {
                Process adbProcess = new Process();
                adbProcess.StartInfo.FileName = "cmd.exe";
                adbProcess.StartInfo.Arguments = $"/C adb -s {deviceId} shell wm size";
                adbProcess.StartInfo.RedirectStandardOutput = true;
                adbProcess.StartInfo.UseShellExecute = false;
                adbProcess.StartInfo.CreateNoWindow = true;
                adbProcess.Start();

                string output = adbProcess.StandardOutput.ReadToEnd();
                adbProcess.WaitForExit();

                // Định dạng đầu ra: "Physical size: 1080x1920"
                string[] parts = output.Split(new[] { "Physical size: " }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    string[] resolution = parts[1].Trim().Split('x');
                    int width = int.Parse(resolution[0]);
                    int height = int.Parse(resolution[1]);
                    return new Size(width, height);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy kích thước màn hình điện thoại: " + ex.Message);
            }
            return new Size(0, 0);
        }
        private void StartScrcpyForPhone(string deviceId, Panel phonePanel, int panelWidth, int panelHeight, string scrcpyPath)
        {
            try
            {
                // Lấy kích thước thực tế của màn hình điện thoại
                Size phoneSize = GetPhoneScreenSize(deviceId);
                int phoneWidth = phoneSize.Width;
                int phoneHeight = phoneSize.Height;

                if (phoneWidth == 0 || phoneHeight == 0)
                {
                    MessageBox.Show($"Không thể lấy kích thước màn hình cho {deviceId}");
                    return;
                }

                // Tính toán tỷ lệ để Scrcpy hiển thị đúng kích thước
                float aspectRatio = (float)phoneWidth / phoneHeight;
                int adjustedWidth = panelWidth;
                int adjustedHeight = (int)(panelWidth / aspectRatio);

                if (adjustedHeight > panelHeight)
                {
                    adjustedHeight = panelHeight;
                    adjustedWidth = (int)(panelHeight * aspectRatio);
                }

                // Dùng crop để hiển thị đúng khung hình
                string cropSize = $"{phoneWidth}:{phoneHeight}:0:0";
                string scrcpyArgs = $"--serial {deviceId} --window-title scrcpy_{deviceId} --window-borderless --crop {cropSize} --max-size {Math.Max(adjustedWidth, adjustedHeight)}";

                Process scrcpyProcess = new Process();
                scrcpyProcess.StartInfo.FileName = scrcpyPath;
                scrcpyProcess.StartInfo.Arguments = scrcpyArgs;
                scrcpyProcess.StartInfo.UseShellExecute = true;
                scrcpyProcess.StartInfo.CreateNoWindow = false;
                scrcpyProcess.Start();

                System.Threading.Thread.Sleep(5000);

                // Tìm cửa sổ scrcpy
                IntPtr scrcpyHandle = IntPtr.Zero;
                int retries = 0;
                while (scrcpyHandle == IntPtr.Zero && retries < 10)
                {
                    scrcpyHandle = FindWindow(null, $"scrcpy_{deviceId}");
                    System.Threading.Thread.Sleep(1000);
                    retries++;
                }

                if (scrcpyHandle != IntPtr.Zero)
                {
                    SetParent(scrcpyHandle, phonePanel.Handle);
                    MoveWindow(scrcpyHandle, 0, 0, adjustedWidth, adjustedHeight, true);
                }
                else
                {
                    MessageBox.Show($"Không thể lấy handle của scrcpy cho thiết bị {deviceId}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chạy scrcpy: " + ex.Message);
            }
        }

        private void StartMultipleScreenStreaming()
        {
            try
            {
                string scrcpyPath = @"D:\projectTA\scrcpy-win64-v3.1\scrcpy.exe";

                // Lấy danh sách thiết bị đang kết nối
                List<string> devices = GetConnectedDevices();
                int deviceCount = devices.Count;

                if (deviceCount == 0)
                {
                    MessageBox.Show("Không có điện thoại nào được kết nối.");
                    return;
                }

                int maxPhones = Math.Min(deviceCount, 20); // Giới hạn tối đa 20 điện thoại

                // Xóa hết nội dung Panel trước khi thêm mới
                tableLayoutPanelPhones.Controls.Clear();
                tableLayoutPanelPhones.RowStyles.Clear();
                tableLayoutPanelPhones.ColumnStyles.Clear();

                // Tính toán số hàng & số cột phù hợp để hiển thị maxPhones thiết bị
                int rows = (int)Math.Ceiling(Math.Sqrt(maxPhones));
                int cols = (int)Math.Ceiling((double)maxPhones / rows);

                tableLayoutPanelPhones.RowCount = rows;
                tableLayoutPanelPhones.ColumnCount = cols;

                for (int i = 0; i < rows; i++)
                {
                    tableLayoutPanelPhones.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
                }
                for (int j = 0; j < cols; j++)
                {
                    tableLayoutPanelPhones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));
                }

                // Tính toán kích thước từng ô Panel
                int panelWidth = tableLayoutPanelPhones.Width / cols;
                int panelHeight = tableLayoutPanelPhones.Height / rows;

                // Khởi chạy Scrcpy cho từng thiết bị
                for (int i = 0; i < maxPhones; i++)
                {
                    Panel phonePanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = devices[i] // Gắn Tag để dễ nhận diện
                    };
                    tableLayoutPanelPhones.Controls.Add(phonePanel, i % cols, i / cols);

                    StartScrcpyForPhone(devices[i], phonePanel, panelWidth, panelHeight, scrcpyPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi chạy nhiều điện thoại: " + ex.Message);
            }
        }


        private List<string> GetConnectedDevices()
        {
            List<string> deviceList = new List<string>();
            try
            {
                Process adbProcess = new Process();
                adbProcess.StartInfo.FileName = "cmd.exe";
                adbProcess.StartInfo.Arguments = "/C adb devices";
                adbProcess.StartInfo.RedirectStandardOutput = true;
                adbProcess.StartInfo.UseShellExecute = false;
                adbProcess.StartInfo.CreateNoWindow = true;
                adbProcess.Start();

                string output = adbProcess.StandardOutput.ReadToEnd();
                adbProcess.WaitForExit();

                string[] lines = output.Split('\n');
                foreach (string line in lines)
                {
                    if (line.Contains("\tdevice"))
                    {
                        deviceList.Add(line.Split('\t')[0]); // Lấy ID thiết bị
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách thiết bị: " + ex.Message);
            }
            return deviceList;
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            StartMultipleScreenStreaming();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            foreach (Control control in tableLayoutPanelPhones.Controls)
            {
                if (control is Panel phonePanel)
                {
                    IntPtr scrcpyHandle = FindWindow(null, $"scrcpy_{phonePanel.Tag}");
                    if (scrcpyHandle != IntPtr.Zero)
                    {
                        MoveWindow(scrcpyHandle, 0, 0, phonePanel.Width, phonePanel.Height, true);
                    }
                }
            }
        }
    }
}

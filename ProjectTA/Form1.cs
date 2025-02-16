using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ProjectTA
{
    public partial class Form1 : Form
    {
        private string deviceId;
        private string scrcpyPath = @"C:\Users\Admin\Documents\job\projectTA\ProjectTA\scrcpy-win64-v3.1\scrcpy-win64-v3.1\scrcpy.exe";
        // Biến để lưu tọa độ vẽ hình chữ nhật
        private Point startPoint;
        private Rectangle selectionRectangle;
        private bool isDrawing = false;
        Panel streamPanel;
        public Form1(string deviceId)
        {
            InitializeComponent();
            this.deviceId = deviceId;
        }

        private void StartScrcpyForPhone(string deviceId, Panel phonePanel, string scrcpyPath)
        {
            try
            {
                // Lấy độ phân giải màn hình của điện thoại
                Process getResolutionProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/C adb -s {deviceId} shell wm size",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                getResolutionProcess.Start();
                string output = getResolutionProcess.StandardOutput.ReadToEnd();
                getResolutionProcess.WaitForExit();

                // Xử lý kết quả lấy được
                Match match = Regex.Match(output, @"Physical size: (\d+)x(\d+)");
                int phoneWidth = 800, phoneHeight = 600; // Giá trị mặc định nếu không lấy được
                if (match.Success)
                {
                    phoneWidth = int.Parse(match.Groups[1].Value);
                    phoneHeight = int.Parse(match.Groups[2].Value);
                }

                float scaleX = (float)phonePanel.Width / phoneWidth;
                float scaleY = (float)phonePanel.Height / phoneHeight;
                float scale = Math.Min(scaleX, scaleY); // Giữ tỷ lệ

                int newWidth = (int)(phoneWidth * scale);
                int newHeight = (int)(phoneHeight * scale);
                // Chạy Scrcpy
                Process scrcpyProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = scrcpyPath,
                        Arguments = $"--serial {deviceId} --window-title scrcpy_{deviceId} --max-size {Math.Max(newWidth, newHeight)} --window-borderless ",

                        //Arguments = $"--serial {deviceId} --window-title scrcpy_{deviceId} --window-borderless",
                        UseShellExecute = true,
                        CreateNoWindow = false
                    }
                };
                scrcpyProcess.Start();

                Thread.Sleep(2000);

                IntPtr scrcpyHandle = IntPtr.Zero;
                int retries = 0;

                while (scrcpyHandle == IntPtr.Zero && retries < 10)
                {
                    scrcpyHandle = FindWindow(null, $"scrcpy_{deviceId}");
                    Thread.Sleep(500);
                    retries++;
                }

                if (scrcpyHandle != IntPtr.Zero)
                {
                    SetParent(scrcpyHandle, phonePanel.Handle);
                    MoveWindow(scrcpyHandle, 0, 0, newWidth, newHeight, true);
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
            streamPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = this.Width / 2,
                BorderStyle = BorderStyle.None,
            };

            //Panel logPanel = new Panel
            //{
            //    Dock = DockStyle.Fill,
            //    BorderStyle = BorderStyle.FixedSingle
            //};

            RichTextBox logBox = new RichTextBox
            {
                Dock = DockStyle.Fill
            };
            //logPanel.Controls.Add(logBox);

            //this.Controls./Add(logPanel);
            this.Controls.Add(streamPanel);
            streamPanel.MouseDown += streamPanel_MouseDown;
            streamPanel.MouseMove += streamPanel_MouseMove;
            streamPanel.MouseUp += streamPanel_MouseUp;
            streamPanel.Paint += streamPanel_Paint;

            // Bắt đầu stream điện thoại đầu tiên
            StartScrcpyForPhone(deviceId, streamPanel, scrcpyPath);
        }

        private void buttonCapture_Click(object sender, EventArgs e)
        {
            isDrawing = true;
            streamPanel.Cursor = Cursors.Cross; // Đổi con trỏ chuột thành dấu "+"
        }
        // Sự kiện khi nhấn chuột xuống để bắt đầu vẽ
        private void streamPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                startPoint = e.Location; // Lưu vị trí bắt đầu
                selectionRectangle = new Rectangle(startPoint.X, startPoint.Y, 0, 0);
            }
        }

        // Sự kiện khi kéo chuột để vẽ hình chữ nhật
        private void streamPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                int width = e.X - startPoint.X;
                int height = e.Y - startPoint.Y;
                selectionRectangle = new Rectangle(startPoint.X, startPoint.Y, width, height);
                streamPanel.Invalidate(); // Vẽ lại panel để hiển thị vùng chọn
            }
        }

        // Sự kiện khi thả chuột để hoàn tất vẽ hình chữ nhật
        private void streamPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                streamPanel.Cursor = Cursors.Default; // Trả lại con trỏ bình thường

                // Lấy tọa độ vùng chọn
                int x1 = selectionRectangle.Left;
                int y1 = selectionRectangle.Top;
                int x2 = selectionRectangle.Right;
                int y2 = selectionRectangle.Bottom;

                MessageBox.Show($"Tọa độ vùng chọn: ({x1}, {y1}) -> ({x2}, {y2})");
            }
        }

        // Vẽ hình chữ nhật khi kéo chuột
        private void streamPanel_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, selectionRectangle);
                }
            }
        }

        // Đăng ký sự kiện cho Panel trong Form_Load
        private void Form1_Load(object sender, EventArgs e)
        {
            streamPanel.MouseDown += streamPanel_MouseDown;
            streamPanel.MouseMove += streamPanel_MouseMove;
            streamPanel.MouseUp += streamPanel_MouseUp;
            streamPanel.Paint += streamPanel_Paint;
        }
    }
}

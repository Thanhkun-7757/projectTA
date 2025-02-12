using System;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;



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
        //private void StartScreenStreaming()
        //{
        //    try
        //    {
        //        string scrcpyPath = @"D:\projectTA\scrcpy-win64-v3.1\scrcpy.exe"; // Cập nhật đường dẫn scrcpy

        //        Process scrcpyProcess = new Process();
        //        scrcpyProcess.StartInfo.FileName = scrcpyPath;
        //        scrcpyProcess.StartInfo.Arguments = "--window-title \"Màn hình điện thoại\""; // Tuỳ chọn khác: --fullscreen, --bit-rate 8M, --max-size 1024
        //        scrcpyProcess.StartInfo.UseShellExecute = false;
        //        scrcpyProcess.StartInfo.CreateNoWindow = true;

        //        scrcpyProcess.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi chạy scrcpy: " + ex.Message);
        //    }
        //}.
        private void StartScreenStreaming()
        {
            try
            {
                // Đường dẫn đến scrcpy.exe
                string scrcpyPath = @"D:\projectTA\scrcpy-win64-v3.1\scrcpy.exe"; // Cập nhật đường dẫn scrcpy

                // Tạo tiến trình scrcpy
                Process scrcpyProcess = new Process();
                scrcpyProcess.StartInfo.FileName = scrcpyPath;
                scrcpyProcess.StartInfo.Arguments = "--window-borderless --max-size 1024"; // Không có viền, độ phân giải 1024
                scrcpyProcess.StartInfo.UseShellExecute = false;
                scrcpyProcess.StartInfo.CreateNoWindow = true;

                scrcpyProcess.Start();
                scrcpyProcess.WaitForInputIdle(); // Chờ scrcpy chạy xong

                // Nhúng scrcpy vào Panel
                IntPtr scrcpyHandle = scrcpyProcess.MainWindowHandle;
                if (scrcpyHandle != IntPtr.Zero)
                {
                    SetParent(scrcpyHandle, tableLayoutPanelPhones.Handle); // Đặt cửa sổ scrcpy vào Panel

                    // Cập nhật kích thước scrcpy để khớp với Panel
                    MoveWindow(scrcpyHandle, 0, 0, tableLayoutPanelPhones.Width, tableLayoutPanelPhones.Height, true);
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


        private void Form2_Load(object sender, EventArgs e)
        {
            StartScreenStreaming();
        }
    }
}

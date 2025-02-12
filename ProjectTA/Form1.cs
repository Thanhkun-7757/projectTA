using System;
using System.Windows.Forms;

namespace ProjectTA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Xử lý sự kiện khi nhấn nút "X" để đóng bảng hiển thị tọa độ
        private void buttonClose_Click(object sender, EventArgs e)
        {
            panelCoordinates.Visible = false; // Ẩn bảng tọa độ
        }

        // Xử lý sự kiện khi nhấn "Chụp hình mới"
        private void buttonCapture_Click(object sender, EventArgs e)
        {
            panelCoordinates.Visible = true; // Hiển thị lại bảng tọa độ nếu bị ẩn

            // Giả lập tọa độ mới (có thể thay thế bằng dữ liệu thực tế)
            Random rnd = new Random();
            int x1 = rnd.Next(100, 300);
            int y1 = rnd.Next(100, 300);
            int x2 = rnd.Next(100, 300);
            int y2 = rnd.Next(100, 300);

            labelCoordinates.Text = $"({x1}:{y1}, {x2}:{y2})";
        }
    }
}

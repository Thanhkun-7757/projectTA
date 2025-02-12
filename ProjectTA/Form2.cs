using System;
using System.Windows.Forms;


namespace ProjectTA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

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
    }
}

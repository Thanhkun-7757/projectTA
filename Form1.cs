using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Chụp hình mới";
            this.Size = new Size(300, 200);
            this.BackColor = Color.LightBlue;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label label = new Label
            {
                Text = "Bảng hiển thị điểm tọa độ",
                Location = new Point(50, 50),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            Label coordinatesLabel = new Label
            {
                Text = "(130.150, 160.180)",
                Location = new Point(50, 80),
                AutoSize = true,
                Font = new Font("Arial", 10),
                ForeColor = Color.DarkRed
            };

            this.Controls.Add(label);
            this.Controls.Add(coordinatesLabel);
        }
    }
}
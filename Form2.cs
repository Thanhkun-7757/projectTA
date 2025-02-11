using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Text = "Nhập kịch bản";
            this.Size = new Size(400, 300);
            this.BackColor = Color.LightGreen;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label scriptLabel = new Label
            {
                Text = "Nhập kịch bản:",
                Location = new Point(50, 50),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            TextBox scriptTextBox = new TextBox
            {
                Text = "ALL_Slow_slow_low",
                Location = new Point(50, 80),
                Size = new Size(200, 20),
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Arial", 10)
            };

            Label coordinatesLabel = new Label
            {
                Text = "Chụp tọa độ: (AA1;)A1,xA2;)A2, (xB2;)**",
                Location = new Point(50, 120),
                AutoSize = true,
                Font = new Font("Arial", 10),
                ForeColor = Color.DarkRed
            };

            Button saveButton = new Button
            {
                Text = "Lưu tọa độ",
                Location = new Point(50, 150),
                Size = new Size(100, 30),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Click += SaveButton_Click;

            this.Controls.Add(scriptLabel);
            this.Controls.Add(scriptTextBox);
            this.Controls.Add(coordinatesLabel);
            this.Controls.Add(saveButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tọa độ đã được lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
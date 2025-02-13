using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectTA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            //AddPhoneLabels(); // Gọi phương thức thêm Labels sau khi khởi tạo form
        }

        private void InitializeComponent()
        {
            this.textBoxScript = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCoordinates = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelPhones = new System.Windows.Forms.TableLayoutPanel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxScript
            // 
            this.textBoxScript.Location = new System.Drawing.Point(20, 20);
            this.textBoxScript.Name = "textBoxScript";
            this.textBoxScript.Size = new System.Drawing.Size(300, 22);
            this.textBoxScript.TabIndex = 0;
            this.textBoxScript.Text = "ALL,Slow,slow,low";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.Blue;
            this.buttonStart.ForeColor = System.Drawing.Color.White;
            this.buttonStart.Location = new System.Drawing.Point(330, 20);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 30);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Bắt đầu";
            this.buttonStart.UseVisualStyleBackColor = false;
            // 
            // buttonCapture
            // 
            this.buttonCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCapture.ForeColor = System.Drawing.Color.Red;
            this.buttonCapture.Location = new System.Drawing.Point(450, 20);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(100, 30);
            this.buttonCapture.TabIndex = 2;
            this.buttonCapture.Text = "Chụp tọa độ";
            this.buttonCapture.Click += new System.EventHandler(this.buttonCapture_Click_1);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Blue;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(770, 20);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 30);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Lưu tọa độ";
            this.buttonSave.UseVisualStyleBackColor = false;
            // 
            // textBoxCoordinates
            // 
            this.textBoxCoordinates.Location = new System.Drawing.Point(560, 20);
            this.textBoxCoordinates.Name = "textBoxCoordinates";
            this.textBoxCoordinates.Size = new System.Drawing.Size(200, 22);
            this.textBoxCoordinates.TabIndex = 3;
            this.textBoxCoordinates.Text = "(xA1:yA1,xA2:yA2), (xB2:yB2)";
            // 
            // tableLayoutPanelPhones
            // 
            this.tableLayoutPanelPhones.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelPhones.ColumnCount = 4;
            this.tableLayoutPanelPhones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPhones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPhones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPhones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPhones.Location = new System.Drawing.Point(20, 56);
            this.tableLayoutPanelPhones.Name = "tableLayoutPanelPhones";
            this.tableLayoutPanelPhones.RowCount = 2;
            this.tableLayoutPanelPhones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPhones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPhones.Size = new System.Drawing.Size(1413, 726);
            this.tableLayoutPanelPhones.TabIndex = 5;
            // 
            // labelDescription
            // 
            this.labelDescription.ForeColor = System.Drawing.Color.Black;
            this.labelDescription.Location = new System.Drawing.Point(20, 280);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(500, 30);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Bảng view phone, có thể click vào phone";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(1470, 866);
            this.Controls.Add(this.textBoxScript);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonCapture);
            this.Controls.Add(this.textBoxCoordinates);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tableLayoutPanelPhones);
            this.Controls.Add(this.labelDescription);
            this.Name = "Form2";
            this.Text = "Phone View";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Resize += new System.EventHandler(this.Form2_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Thêm các Label (phone) vào tableLayoutPanel
        private void AddPhoneLabels()
        {
            for (int i = 0; i < 8; i++)
            {
                Label label = new Label();
                label.Text = "phone";
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Dock = DockStyle.Fill;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.ForeColor = Color.Green;

                this.tableLayoutPanelPhones.Controls.Add(label);
            }
        }

        private System.Windows.Forms.TextBox textBoxScript;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCapture;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCoordinates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPhones;
        private System.Windows.Forms.Label labelDescription;
    }
}

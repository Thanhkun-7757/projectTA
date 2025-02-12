namespace ProjectTA
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelPhone = new System.Windows.Forms.Panel();
            this.labelPhone = new System.Windows.Forms.Label();
            this.buttonCapture = new System.Windows.Forms.Button();
            this.panelCoordinates = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelCoordinates = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panelPhone.SuspendLayout();
            this.panelCoordinates.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPhone
            // 
            this.panelPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPhone.Controls.Add(this.labelPhone);
            this.panelPhone.Location = new System.Drawing.Point(50, 40);
            this.panelPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelPhone.Name = "panelPhone";
            this.panelPhone.Size = new System.Drawing.Size(250, 96);
            this.panelPhone.TabIndex = 0;
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.labelPhone.ForeColor = System.Drawing.Color.Green;
            this.labelPhone.Location = new System.Drawing.Point(95, 40);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(86, 29);
            this.labelPhone.TabIndex = 0;
            this.labelPhone.Text = "phone";
            // 
            // buttonCapture
            // 
            this.buttonCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCapture.Font = new System.Drawing.Font("Arial", 12F);
            this.buttonCapture.ForeColor = System.Drawing.Color.Red;
            this.buttonCapture.Location = new System.Drawing.Point(320, 64);
            this.buttonCapture.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonCapture.Name = "buttonCapture";
            this.buttonCapture.Size = new System.Drawing.Size(150, 32);
            this.buttonCapture.TabIndex = 1;
            this.buttonCapture.Text = "Chụp hình mới";
            // 
            // panelCoordinates
            // 
            this.panelCoordinates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCoordinates.Controls.Add(this.labelTitle);
            this.panelCoordinates.Controls.Add(this.labelCoordinates);
            this.panelCoordinates.Controls.Add(this.buttonClose);
            this.panelCoordinates.Location = new System.Drawing.Point(500, 32);
            this.panelCoordinates.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelCoordinates.Name = "panelCoordinates";
            this.panelCoordinates.Size = new System.Drawing.Size(350, 104);
            this.panelCoordinates.TabIndex = 2;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.Red;
            this.labelTitle.Location = new System.Drawing.Point(27, 7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(252, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Bảng hiển thị điểm tọa độ";
            // 
            // labelCoordinates
            // 
            this.labelCoordinates.AutoSize = true;
            this.labelCoordinates.Font = new System.Drawing.Font("Arial", 12F);
            this.labelCoordinates.ForeColor = System.Drawing.Color.Red;
            this.labelCoordinates.Location = new System.Drawing.Point(65, 48);
            this.labelCoordinates.Name = "labelCoordinates";
            this.labelCoordinates.Size = new System.Drawing.Size(180, 23);
            this.labelCoordinates.TabIndex = 1;
            this.labelCoordinates.Text = "(130:150, 160:180)";
            // 
            // buttonClose
            // 
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.buttonClose.ForeColor = System.Drawing.Color.Red;
            this.buttonClose.Location = new System.Drawing.Point(320, 2);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(25, 20);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "X";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 320);
            this.Controls.Add(this.panelPhone);
            this.Controls.Add(this.buttonCapture);
            this.Controls.Add(this.panelCoordinates);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Giao diện chụp hình";
            this.panelPhone.ResumeLayout(false);
            this.panelPhone.PerformLayout();
            this.panelCoordinates.ResumeLayout(false);
            this.panelCoordinates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPhone;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.Button buttonCapture;
        private System.Windows.Forms.Panel panelCoordinates;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelCoordinates;
        private System.Windows.Forms.Button buttonClose;
    }
}

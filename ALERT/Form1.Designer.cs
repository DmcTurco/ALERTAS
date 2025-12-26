namespace ALERT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelHeader = new Panel();
            lblTitle = new Label();
            lsvAlert = new ListView();
            pnl_msm = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            lbl_totallItem = new Label();
            label3 = new Label();
            btnExit = new Button();
            panelHeader.SuspendLayout();
            pnl_msm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(44, 62, 80);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(758, 49);
            panelHeader.TabIndex = 0;
            panelHeader.MouseMove += panelHeader_MouseMove;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(109, 13);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "🔔 Sistema de Alertas";
            // 
            // lsvAlert
            // 
            lsvAlert.Location = new Point(12, 104);
            lsvAlert.Name = "lsvAlert";
            lsvAlert.Size = new Size(734, 293);
            lsvAlert.TabIndex = 2;
            lsvAlert.UseCompatibleStateImageBehavior = false;
            // 
            // pnl_msm
            // 
            pnl_msm.BackColor = Color.White;
            pnl_msm.Controls.Add(pictureBox1);
            pnl_msm.Controls.Add(label1);
            pnl_msm.Location = new Point(12, 55);
            pnl_msm.Name = "pnl_msm";
            pnl_msm.Size = new Size(734, 419);
            pnl_msm.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(299, 150);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(108, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Tomato;
            label1.Location = new Point(187, 293);
            label1.Name = "label1";
            label1.Size = new Size(352, 33);
            label1.TabIndex = 0;
            label1.Text = "THERE ARE NO ALERTS";
            // 
            // lbl_totallItem
            // 
            lbl_totallItem.AutoSize = true;
            lbl_totallItem.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_totallItem.ForeColor = Color.DimGray;
            lbl_totallItem.Location = new Point(719, 83);
            lbl_totallItem.Name = "lbl_totallItem";
            lbl_totallItem.Size = new Size(24, 18);
            lbl_totallItem.TabIndex = 4;
            lbl_totallItem.Text = "00";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DimGray;
            label3.Location = new Point(624, 83);
            label3.Name = "label3";
            label3.Size = new Size(89, 18);
            label3.TabIndex = 5;
            label3.Text = "Total Items :";
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.Cursor = Cursors.Hand;
            btnExit.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.FromArgb(149, 165, 166);
            btnExit.Location = new Point(627, 403);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(119, 71);
            btnExit.TabIndex = 6;
            btnExit.Text = "EXIT";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(758, 486);
            Controls.Add(pnl_msm);
            Controls.Add(btnExit);
            Controls.Add(label3);
            Controls.Add(lbl_totallItem);
            Controls.Add(lsvAlert);
            Controls.Add(panelHeader);
            Font = new Font("Microsoft Sans Serif", 8.25F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(20, 25);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SISTEM ALERT";
            Load += Form1_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            pnl_msm.ResumeLayout(false);
            pnl_msm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private ListView lsvAlert;
        private Panel pnl_msm;
        private Label label1;
        private PictureBox pictureBox1;
        private Label lbl_totallItem;
        private Label label3;
        private Button btnExit;
    }
}

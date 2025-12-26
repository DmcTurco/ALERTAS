using System;
using System.Drawing;
using System.Windows.Forms;

namespace ALERT
{
    public partial class FormNotification : Form
    {
        private Alert alert;
        private DatabaseHelper db;
        private WebBrowser webMessage;
        private System.Windows.Forms.Timer timerAutoClose; // ⭐ Timer para cierre automático

        public FormNotification()
        {
            InitializeComponent();
        }

        public FormNotification(Alert alert, DatabaseHelper db)
        {
            InitializeComponent();
            this.alert = alert;
            this.db = db;
            InicializarWebBrowser();
            LLenarDatosAlerta();
            ConfigurarNotificacion();
            ConfigurarCierreAutomatico();
        }

        private void ConfigurarCierreAutomatico()
        {
            timerAutoClose = new System.Windows.Forms.Timer();
            timerAutoClose.Interval = 5000; // 5 segundos antes de cerrarse
            timerAutoClose.Tick += (s, e) =>
            {
                timerAutoClose.Stop();
                CerrarNotificacion();
            };
            timerAutoClose.Start();
        }

        private void InicializarWebBrowser()
        {
            webMessage = new WebBrowser
            {
                Name = "webMessage",
                Dock = DockStyle.None,
                Location = new Point(15, 70),
                Size = new Size(410, 150),       // Tamaño fijo
                ScrollBarsEnabled = true,        // ⭐ Habilitar scroll
                IsWebBrowserContextMenuEnabled = false,
                WebBrowserShortcutsEnabled = false,
                AllowNavigation = false,
                ScriptErrorsSuppressed = true
            };

            this.Controls.Add(webMessage);
            webMessage.BringToFront();
        }

        private void LLenarDatosAlerta()
        {
            if (alert == null) return;

            lblType.Text = WindowMover.GetTypeText(alert.type);
            lblIcon.Text = WindowMover.GetIconByType(alert.type);

            webMessage.DocumentText = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Arial, sans-serif;
                            font-size: 14pt;
                            margin: 0;
                            padding: 8px;
                            background: white;
                            color: #333;
                            line-height: 1.5;  /* ⭐ Mejor espaciado */
                        }}
                        b, strong {{ color: #2c3e50; }}
                        br {{ margin: 5px 0; }}
                    </style>
                </head>
                <body>{alert.message}</body>
                </html>";

            Color colorTipo = WindowMover.GetColorByType(alert.type);
            panel1.BackColor = colorTipo;
        }

        private void ConfigurarNotificacion()
        {
            if (alert == null || db == null) return;

            WindowMover.ApplyRoundedCorners(this, 10);

            Screen screen = Screen.PrimaryScreen;
            this.StartPosition = FormStartPosition.Manual;
            this.Left = screen.WorkingArea.Right - this.Width - 10;
            this.Top = screen.WorkingArea.Bottom;

            int targetY = screen.WorkingArea.Bottom - this.Height - 10;
            WindowMover.AnimateSlideUp(this, targetY, speed: 5);
        }

        private void CerrarNotificacion()
        {
            WindowMover.AnimateFadeOutAndClose(this);
        }

        //private void btnExit_Click(object sender, EventArgs e)
        //{

        //    if(alert != null && db != null)
        //    {
        //        db.MarkAsRead(alert.cd);
        //    }

        //    WindowMover.AnimateFadeOutAndClose(this);
        //}

        private void FormNotification_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                WindowMover.MoveForm(this);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                WindowMover.MoveForm(this);
        }
    }
}
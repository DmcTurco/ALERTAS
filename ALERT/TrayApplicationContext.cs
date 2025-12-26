using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ALERT
{
    internal class TrayApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private DatabaseHelper db;
        private System.Windows.Forms.Timer timerMonitor;
        private Form1 mainForm;
        private List<int> alertasYaMostradas;

        // ⭐ Sistema de cola
        private Queue<Alert> colaNotificaciones;
        private FormNotification notificacionActual;

        public TrayApplicationContext()
        {
            alertasYaMostradas = new List<int>();
            colaNotificaciones = new Queue<Alert>();
            db = new DatabaseHelper();
            db.Initialize();

            ConfigurarTrayIcon();
            ConfigurarTimer();
        }

        private void ConfigurarTrayIcon()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Information,
                Visible = true,
                Text = "Sistema de Alertas - Activo"
            };

            trayIcon.DoubleClick += (s, e) => AbrirPanel();

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("📊 Abrir Panel", null, (s, e) => AbrirPanel());
            menu.Items.Add("🔍 Verificar Ahora", null, (s, e) => VerificarAhora());
            menu.Items.Add("-");
            menu.Items.Add("ℹ️ Acerca de", null, (s, e) => MostrarAcercaDe());
            menu.Items.Add("-");
            menu.Items.Add("❌ Salir", null, (s, e) => Salir());

            trayIcon.ContextMenuStrip = menu;
        }

        private void ConfigurarTimer()
        {
            timerMonitor = new System.Windows.Forms.Timer();
            timerMonitor.Interval = 5000; // 5 segundos
            timerMonitor.Tick += (s, e) => VerificarAlertas();
            timerMonitor.Start();

            VerificarAlertas();
        }

        private void VerificarAlertas()
        {
            try
            {
                var alertasActivas = db.GetActiveAlerts();

                // Filtrar solo las nuevas (que no se han mostrado)
                var nuevasAlertas = alertasActivas
                    .Where(a => !alertasYaMostradas.Contains(a.cd))
                    .ToList();

                if (nuevasAlertas.Count > 0)
                {
                    // Mostrar notificación balloon del sistema
                    trayIcon.BalloonTipTitle = "🔔 Nuevas Alertas";
                    trayIcon.BalloonTipText = $"Tienes {nuevasAlertas.Count} alerta(s) nueva(s)";
                    trayIcon.BalloonTipIcon = ToolTipIcon.Warning;
                    trayIcon.ShowBalloonTip(3000);

                    // ⭐ Agregar a la cola
                    foreach (var alerta in nuevasAlertas)
                    {
                        colaNotificaciones.Enqueue(alerta);
                        alertasYaMostradas.Add(alerta.cd);
                    }

                    // ⭐ Iniciar procesamiento si no hay notificación activa
                    if (notificacionActual == null || notificacionActual.IsDisposed)
                    {
                        MostrarSiguienteNotificacion();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar alertas: {ex.Message}");
            }
        }

        // ⭐ Mostrar siguiente notificación de la cola
        private void MostrarSiguienteNotificacion()
        {
            // Si no hay más en la cola, terminar
            if (colaNotificaciones.Count == 0)
                return;

            // Sacar la siguiente alerta de la cola
            var alerta = colaNotificaciones.Dequeue();

            // Crear y mostrar la notificación
            notificacionActual = new FormNotification(alerta, db);

            // ⭐ Cuando se cierre, mostrar inmediatamente la siguiente
            notificacionActual.FormClosed += (s, e) =>
            {
                notificacionActual = null;

                // Mostrar la siguiente inmediatamente
                if (colaNotificaciones.Count > 0)
                {
                    MostrarSiguienteNotificacion();
                }
            };

            notificacionActual.Show();
        }

        private void AbrirPanel()
        {
            if (mainForm == null || mainForm.IsDisposed)
            {
                mainForm = new Form1();
                mainForm.FormClosed += (s, e) => mainForm = null;
                mainForm.Show();
            }
            else
            {
                mainForm.WindowState = FormWindowState.Normal;
                mainForm.BringToFront();
                mainForm.Activate();
            }
        }

        private void VerificarAhora()
        {
            alertasYaMostradas.Clear();
            VerificarAlertas();
        }

        private void MostrarAcercaDe()
        {
            MessageBox.Show(
                "Sistema de Alertas v1.0\n\n" +
                "⏱️ Verifica alertas cada 5 segundos\n" +
                "🔔 Muestra notificaciones en tiempo real\n" +
                "💾 Base de datos SQLite\n\n" +
                "Desarrollado con C# y Windows Forms por TURCO",
                "Acerca de",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void Salir()
        {
            var resultado = MessageBox.Show(
                "¿Desea cerrar el sistema de alertas?\n\n" +
                "El monitoreo de alertas se detendrá.",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                timerMonitor?.Stop();
                timerMonitor?.Dispose();

                if (mainForm != null && !mainForm.IsDisposed)
                {
                    mainForm.Close();
                }

                if (notificacionActual != null && !notificacionActual.IsDisposed)
                {
                    notificacionActual.Close();
                }

                trayIcon.Visible = false;
                trayIcon.Dispose();

                Application.Exit();
            }
        }
    }
}
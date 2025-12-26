using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace ALERT
{
    internal static class WindowMover
    {
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public static void MoveForm(Form form)
        {
            ReleaseCapture();
            SendMessage(form.Handle, 0x112, 0xf012, 0);
        }

        public static void ApplyRoundedCorners(Form form, int radius = 10)
        {
            IntPtr ptr = CreateRoundRectRgn(0, 0, form.Width, form.Height, radius, radius);
            form.Region = Region.FromHrgn(ptr);
        }

        // ⭐ ANIMACIÓN DE DESLIZAMIENTO HACIA ARRIBA
        public static void AnimateSlideUp(Form form, int targetY, int speed = 5)
        {
            System.Windows.Forms.Timer timerSlide = new System.Windows.Forms.Timer();
            timerSlide.Interval = 10; // Cada 10ms
            timerSlide.Tick += (s, e) =>
            {
                if (form.Top > targetY)
                {
                    form.Top -= speed; // Velocidad del deslizamiento
                }
                else
                {
                    form.Top = targetY;
                    timerSlide.Stop();
                    timerSlide.Dispose();
                }
            };
            timerSlide.Start();
        }

        // ⭐ ANIMACIÓN DE FADE OUT Y CIERRE
        public static void AnimateFadeOutAndClose(Form form, double fadeSpeed = 0.05)
        {
            System.Windows.Forms.Timer timerFadeOut = new System.Windows.Forms.Timer();
            timerFadeOut.Interval = 10;
            timerFadeOut.Tick += (s, e) =>
            {
                if (form.Opacity > 0)
                {
                    form.Opacity -= fadeSpeed;
                }
                else
                {
                    timerFadeOut.Stop();
                    timerFadeOut.Dispose();
                    form.Close();
                }
            };
            timerFadeOut.Start();
        }

        // ⭐ POSICIONAR FORMULARIO EN ESQUINA INFERIOR DERECHA
        public static void PositionBottomRight(Form form, int offsetX = 10, int offsetY = 10, int additionalOffsetY = 0)
        {
            Screen screen = Screen.PrimaryScreen;
            form.StartPosition = FormStartPosition.Manual;
            form.Left = screen.WorkingArea.Right - form.Width - offsetX;
            form.Top = screen.WorkingArea.Bottom - form.Height - offsetY - additionalOffsetY;
        }

        // ⭐ COLORES SEGÚN TIPO DE ALERTA
        public static Color GetColorByType(int type)
        {
            return type switch
            {
                1 => ColorTranslator.FromHtml("#F39C12"), // Warning - Naranja
                2 => ColorTranslator.FromHtml("#3498DB"), // Info - Azul
                3 => ColorTranslator.FromHtml("#E74C3C"), // Error - Rojo
                4 => ColorTranslator.FromHtml("#27AE60"), // Success - Verde
                _ => ColorTranslator.FromHtml("#95A5A6")  // Unknown - Gris
            };
        }

        // ⭐ ICONOS SEGÚN TIPO DE ALERTA
        public static string GetIconByType(int type)
        {
            return type switch
            {
                1 => "⚠️",
                2 => "ℹ️",
                3 => "❌",
                4 => "✅",
                _ => "❓"
            };
        }

        // ⭐ TEXTO SEGÚN TIPO DE ALERTA
        public static string GetTypeText(int type)
        {
            return type switch
            {
                1 => "ADVERTENCIA",
                2 => "INFORMACIÓN",
                3 => "ERROR",
                4 => "ÉXITO",
                _ => "DESCONOCIDO"
            };
        }
    }
}
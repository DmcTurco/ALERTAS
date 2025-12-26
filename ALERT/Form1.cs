using System.Collections.Generic;

namespace ALERT
{
    public partial class Form1 : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db.Initialize();
            configuration_listView();
            load_Data();
        }

        public void RefrescarLista()
        {
            // Verificar que estamos en el hilo correcto (UI thread)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(RefrescarLista));
                return;
            }

            // Recargar los datos
            load_Data();
        }

        private void configuration_listView()
        {
            lsvAlert.Items.Clear();
            lsvAlert.Columns.Clear();
            lsvAlert.View = View.Details;
            lsvAlert.GridLines = false;
            lsvAlert.FullRowSelect = true;
            lsvAlert.Scrollable = true;
            lsvAlert.HideSelection = false;

            lsvAlert.Columns.Add("CD", 50, HorizontalAlignment.Center);
            lsvAlert.Columns.Add("Type", 160, HorizontalAlignment.Center);
            lsvAlert.Columns.Add("Message", 300, HorizontalAlignment.Left);
            lsvAlert.Columns.Add("Date Register", 150, HorizontalAlignment.Left);
            lsvAlert.Columns.Add("Read", 50, HorizontalAlignment.Center);
        }

        private void Llenar_ListView(List<Alert> alerts)
        {
            lsvAlert.Items.Clear();

            foreach (var alert in alerts)
            {
                string typeText = WindowMover.GetTypeText(alert.type);
                string typeIcon = WindowMover.GetIconByType(alert.type);

                var item = new ListViewItem(alert.cd.ToString());
                item.SubItems.Add($"{typeIcon} {typeText}");
                item.SubItems.Add(alert.message ?? "");
                item.SubItems.Add(alert.recordDate.ToString() ?? "");
                item.SubItems.Add(alert.markasRead == 1 ? "🔴" : "✓");

                lsvAlert.Items.Add(item);
            }
            lbl_totallItem.Text = lsvAlert.Items.Count.ToString();
        }

        private void load_Data()
        {
            var alerts = db.GetAllAlerts();

            if (alerts.Count > 0)
            {
                pnl_msm.Visible = false;
                Llenar_ListView(alerts);
            }
            else
            {
                lsvAlert.Items.Clear();
                pnl_msm.Visible = true;
            }
        }

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowMover.MoveForm(this);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            WindowMover.AnimateFadeOutAndClose(this);
        }
    }
}
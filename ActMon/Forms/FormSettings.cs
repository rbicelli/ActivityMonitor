using System;
using System.Windows.Forms;
using ActMon.SettingsManager;
namespace ActMon.Forms
{
    public partial class FormSettings : Form
    {
        private Settings _settings;
        public FormSettings(Settings fSettings)
        {
            _settings = fSettings;
            InitializeComponent();
        }

        private void lbDBDatabase_Click(object sender, EventArgs e)
        {

        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            //Load Settings
            textDBServer.Text = _settings.DBServer;
            textDBDatabase.Text = _settings.DBDatabase;
            textDBUsername.Text = _settings.DBUsername;
            textDBPassword.Text = _settings.DBPassword;
            chkAutostart.Checked = _settings.Autostart;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Save Settings
            _settings.DBServer = textDBServer.Text;
            _settings.DBDatabase = textDBDatabase.Text;
            _settings.DBUsername = textDBUsername.Text;
            _settings.DBPassword = textDBPassword.Text;
            _settings.Autostart = chkAutostart.Checked;
            _settings.WriteSettings();

            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

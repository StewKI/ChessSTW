using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessSTW_Desktop
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            string? rotate = ConfigurationManager.AppSettings.Get("Rotate");
            if (rotate is null)
            {
                rotate = "False";
            }
            rotateBox.Checked = bool.Parse(rotate!);
            IPTextBox.Text = ConfigurationManager.AppSettings.Get("IP");
            PortTextBox.Text = ConfigurationManager.AppSettings.Get("Port");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int port;
            if (int.TryParse(PortTextBox.Text, out port))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Rotate"].Value = rotateBox.Checked.ToString();
                config.AppSettings.Settings["IP"].Value = IPTextBox.Text;
                config.AppSettings.Settings["Port"].Value = PortTextBox.Text;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Port must be a number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

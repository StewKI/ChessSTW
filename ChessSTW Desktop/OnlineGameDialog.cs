using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessSTW_Desktop
{
    public partial class OnlineGameDialog : Form
    {
        public int Color { get; private set; }
        public string Username { get; private set; }
        public string? OpponUsername { get; private set; }

        public OnlineGameDialog()
        {
            InitializeComponent();
            Color = 2;
            Username = "player";
            randomRadio.Checked = true;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void randomRadio_CheckedChanged(object sender, EventArgs e)
        {
            Color = 2;
        }

        private void whiteRadio_CheckedChanged(object sender, EventArgs e)
        {
            Color = 0;
        }

        private void blackRadio_CheckedChanged(object sender, EventArgs e)
        {
            Color = 1;
        }

        private void specOpponBox_CheckedChanged(object sender, EventArgs e)
        {
            opponLabel.Enabled = specOpponBox.Checked;
            opponTextBox.Enabled = specOpponBox.Checked;
            if (!specOpponBox.Checked)
            {
                OpponUsername = null;
            }
            else
            {
                OpponUsername = opponTextBox.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Username = textBox1.Text;
        }

        private void opponTextBox_TextChanged(object sender, EventArgs e)
        {
            OpponUsername = opponTextBox.Text;
        }

        private void OnlineGameDialog_Load(object sender, EventArgs e)
        {

        }
    }
}

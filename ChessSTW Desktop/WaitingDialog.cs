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
    public partial class WaitingDialog : Form
    {
        private bool blockClosing = true;

        public WaitingDialog()
        {
            InitializeComponent();
        }

        private void WaitingDialog_Load(object sender, EventArgs e)
        {

        }

        public void SetText(string text)
        {
            label1.Text = text;
        }

        public void ForceClose()
        {
            blockClosing = false;
            Close();
        }

        private void WaitingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (blockClosing)
            {
                e.Cancel = true;
            }
        }

    }
}

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
    public partial class PromoteDialogue : Form
    {
        public int ReturnValue { get; set; }

        public PromoteDialogue()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReturnValue = 0;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ReturnValue = 1;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            ReturnValue = 2;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ReturnValue = 3;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PromoteDialogue_Load(object sender, EventArgs e)
        {
            ReturnValue = 0;
        }
    }
}

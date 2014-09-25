using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mariinette2
{
    public partial class DisConnectSerial : Form
    {
        public DisConnectSerial()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.pictureBox1.Image = global::Mariinette2.Properties.Resources.cut_usb_yes_over;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Mariinette2.Properties.Resources.cut_usb_yes_default;
            this.Cursor = Cursors.Default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.pictureBox2.Image = global::Mariinette2.Properties.Resources.cut_usb_no_over;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.Image = global::Mariinette2.Properties.Resources.cut_usb_no_default;
            this.Cursor = Cursors.Default;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

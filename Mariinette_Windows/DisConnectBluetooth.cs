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
    public partial class DisConnectBluetooth : Form
    {
        public DisConnectBluetooth()
        {
            InitializeComponent();
        }

        private void pbYes_MouseHover(object sender, EventArgs e)
        {
            this.pbYes.Image = global::Mariinette2.Properties.Resources.cut_blth_yes_over;
        }

        private void pbYes_MouseLeave(object sender, EventArgs e)
        {
            this.pbYes.Image = global::Mariinette2.Properties.Resources.cut_blth_yes_default;
        }

        private void pbYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pbNo_MouseHover(object sender, EventArgs e)
        {
            this.pbNo.Image = global::Mariinette2.Properties.Resources.cut_blth_no_default;
        }

        private void pbNo_MouseLeave(object sender, EventArgs e)
        {
            this.pbNo.Image = global::Mariinette2.Properties.Resources.cut_blth_no_over;
        }

        private void pbNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

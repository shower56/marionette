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
    public partial class popupClient : Form
    {
        public popupClient()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.Image = global::Mariinette2.Properties.Resources.no_default2;
            this.Close();
        }

        private void Ok_MouseHover(object sender, EventArgs e)
        {
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.Image = global::Mariinette2.Properties.Resources.yes_over2;
        }

        private void Ok_MouseLeave(object sender, EventArgs e)
        {
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.Image = global::Mariinette2.Properties.Resources.yes_default2;
        }

        private void No_MouseHover(object sender, EventArgs e)
        {
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.Image = global::Mariinette2.Properties.Resources.no_over2;
        }

        private void No_MouseLeave(object sender, EventArgs e)
        {
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.Image = global::Mariinette2.Properties.Resources.no_default2;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.Image = global::Mariinette2.Properties.Resources.yes_default2;
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

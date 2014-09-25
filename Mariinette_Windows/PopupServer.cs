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
    public partial class PopupServer : Form
    {
        public PopupServer()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.Image = global::Mariinette2.Properties.Resources.yes_default;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Ok_MouseHover(object sender, EventArgs e)
        {
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.Image = global::Mariinette2.Properties.Resources.yes_over;
            this.Cursor = Cursors.Hand;
        }

        private void Ok_MouseLeave(object sender, EventArgs e)
        {
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.Image = global::Mariinette2.Properties.Resources.yes_default;
            this.Cursor = Cursors.Default;
        }

        private void No_Click(object sender, EventArgs e)
        {
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.Image = global::Mariinette2.Properties.Resources.no_default;
            this.Close();
        }

        private void No_MouseHover(object sender, EventArgs e)
        {
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.Image = global::Mariinette2.Properties.Resources.no_over;
            this.Cursor = Cursors.Hand;
        }

        private void No_MouseLeave(object sender, EventArgs e)
        {
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.Image = global::Mariinette2.Properties.Resources.no_default;
            this.Cursor = Cursors.Default;
        }

       
      
    }
}

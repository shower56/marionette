using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mariinette2
{
    public partial class CheckBlueTooth : Form
    {
        public CheckBlueTooth()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Mariinette2.Properties.Resources.blth_check_ok_over;
            
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Mariinette2.Properties.Resources.blth_check_ok_default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

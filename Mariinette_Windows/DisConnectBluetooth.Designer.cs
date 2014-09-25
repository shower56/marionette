namespace Mariinette2
{
    partial class DisConnectBluetooth
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbYes = new System.Windows.Forms.PictureBox();
            this.pbNo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbYes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbYes
            // 
            this.pbYes.BackgroundImage = global::Mariinette2.Properties.Resources.cut_blth_yes_default;
            this.pbYes.Location = new System.Drawing.Point(3, 142);
            this.pbYes.Name = "pbYes";
            this.pbYes.Size = new System.Drawing.Size(152, 45);
            this.pbYes.TabIndex = 0;
            this.pbYes.TabStop = false;
            this.pbYes.Click += new System.EventHandler(this.pbYes_Click);
            this.pbYes.MouseLeave += new System.EventHandler(this.pbYes_MouseLeave);
            this.pbYes.MouseHover += new System.EventHandler(this.pbYes_MouseHover);
            // 
            // pbNo
            // 
            this.pbNo.BackgroundImage = global::Mariinette2.Properties.Resources.cut_blth_no_default;
            this.pbNo.Location = new System.Drawing.Point(156, 142);
            this.pbNo.Name = "pbNo";
            this.pbNo.Size = new System.Drawing.Size(152, 45);
            this.pbNo.TabIndex = 1;
            this.pbNo.TabStop = false;
            this.pbNo.Click += new System.EventHandler(this.pbNo_Click);
            this.pbNo.MouseLeave += new System.EventHandler(this.pbNo_MouseLeave);
            this.pbNo.MouseHover += new System.EventHandler(this.pbNo_MouseHover);
            // 
            // DisConnectBluetooth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mariinette2.Properties.Resources.cut_blth_bg;
            this.ClientSize = new System.Drawing.Size(310, 200);
            this.Controls.Add(this.pbNo);
            this.Controls.Add(this.pbYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DisConnectBluetooth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DisConnectBluetooth";
            ((System.ComponentModel.ISupportInitialize)(this.pbYes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbYes;
        private System.Windows.Forms.PictureBox pbNo;
    }
}
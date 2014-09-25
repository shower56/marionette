namespace Mariinette2
{
    partial class PopupServer
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
            this.Ok = new System.Windows.Forms.PictureBox();
            this.No = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.No)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.BackgroundImage = global::Mariinette2.Properties.Resources.yes_default;
            this.Ok.Location = new System.Drawing.Point(2, 137);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(152, 45);
            this.Ok.TabIndex = 0;
            this.Ok.TabStop = false;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            this.Ok.MouseLeave += new System.EventHandler(this.Ok_MouseLeave);
            this.Ok.MouseHover += new System.EventHandler(this.Ok_MouseHover);
            // 
            // No
            // 
            this.No.BackColor = System.Drawing.Color.Transparent;
            this.No.BackgroundImage = global::Mariinette2.Properties.Resources.no_default;
            this.No.Location = new System.Drawing.Point(157, 138);
            this.No.Name = "No";
            this.No.Size = new System.Drawing.Size(152, 45);
            this.No.TabIndex = 1;
            this.No.TabStop = false;
            this.No.Click += new System.EventHandler(this.No_Click);
            this.No.MouseLeave += new System.EventHandler(this.No_MouseLeave);
            this.No.MouseHover += new System.EventHandler(this.No_MouseHover);
            // 
            // PopupServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mariinette2.Properties.Resources.dialogbg_server;
            this.ClientSize = new System.Drawing.Size(310, 200);
            this.Controls.Add(this.No);
            this.Controls.Add(this.Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PopupServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PopupServer";
            
            ((System.ComponentModel.ISupportInitialize)(this.Ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.No)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Ok;
        private System.Windows.Forms.PictureBox No;
    }
}
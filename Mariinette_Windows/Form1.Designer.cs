namespace Mariinette2
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pb_Title = new System.Windows.Forms.PictureBox();
            this.pbPhone = new System.Windows.Forms.PictureBox();
            this.pbUseConn = new System.Windows.Forms.PictureBox();
            this.pbBtConn = new System.Windows.Forms.PictureBox();
            this.pbServerCom = new System.Windows.Forms.PictureBox();
            this.pbConn = new System.Windows.Forms.PictureBox();
            this.pbClientCom = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUseConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbServerCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb_Title
            // 
            this.pb_Title.BackColor = System.Drawing.Color.Transparent;
            this.pb_Title.Image = global::Mariinette2.Properties.Resources.marionette;
            this.pb_Title.Location = new System.Drawing.Point(95, 30);
            this.pb_Title.Name = "pb_Title";
            this.pb_Title.Size = new System.Drawing.Size(310, 40);
            this.pb_Title.TabIndex = 6;
            this.pb_Title.TabStop = false;
            // 
            // pbPhone
            // 
            this.pbPhone.BackColor = System.Drawing.Color.Transparent;
            this.pbPhone.Image = global::Mariinette2.Properties.Resources._2phone;
            this.pbPhone.Location = new System.Drawing.Point(184, 119);
            this.pbPhone.Name = "pbPhone";
            this.pbPhone.Size = new System.Drawing.Size(133, 133);
            this.pbPhone.TabIndex = 7;
            this.pbPhone.TabStop = false;
            // 
            // pbUseConn
            // 
            this.pbUseConn.BackColor = System.Drawing.Color.Transparent;
            this.pbUseConn.Image = global::Mariinette2.Properties.Resources.usb_fail;
            this.pbUseConn.Location = new System.Drawing.Point(119, 260);
            this.pbUseConn.Name = "pbUseConn";
            this.pbUseConn.Size = new System.Drawing.Size(64, 71);
            this.pbUseConn.TabIndex = 8;
            this.pbUseConn.TabStop = false;
            this.pbUseConn.Click += new System.EventHandler(this.pbUseConn_Click);
            this.pbUseConn.MouseLeave += new System.EventHandler(this.pbUseConn_MouseLeave);
            this.pbUseConn.MouseHover += new System.EventHandler(this.pbUseConn_MouseHover);
            // 
            // pbBtConn
            // 
            this.pbBtConn.BackColor = System.Drawing.Color.Transparent;
            this.pbBtConn.Image = global::Mariinette2.Properties.Resources.bluetooth_fail;
            this.pbBtConn.Location = new System.Drawing.Point(316, 260);
            this.pbBtConn.Name = "pbBtConn";
            this.pbBtConn.Size = new System.Drawing.Size(64, 71);
            this.pbBtConn.TabIndex = 9;
            this.pbBtConn.TabStop = false;
            this.pbBtConn.Click += new System.EventHandler(this.pbBtConn_Click);
            this.pbBtConn.MouseLeave += new System.EventHandler(this.pbBtConn_MouseLeave);
            this.pbBtConn.MouseHover += new System.EventHandler(this.pbBtConn_MouseHover);
            // 
            // pbServerCom
            // 
            this.pbServerCom.BackColor = System.Drawing.Color.Transparent;
            this.pbServerCom.Image = global::Mariinette2.Properties.Resources.server_default;
            this.pbServerCom.Location = new System.Drawing.Point(40, 343);
            this.pbServerCom.Name = "pbServerCom";
            this.pbServerCom.Size = new System.Drawing.Size(133, 133);
            this.pbServerCom.TabIndex = 10;
            this.pbServerCom.TabStop = false;
            this.pbServerCom.Click += new System.EventHandler(this.pbServerCom_Click);
            this.pbServerCom.MouseLeave += new System.EventHandler(this.pbServerCom_MouseLeave);
            this.pbServerCom.MouseHover += new System.EventHandler(this.pbServerCom_MouseHover);
            // 
            // pbConn
            // 
            this.pbConn.BackColor = System.Drawing.Color.Transparent;
            this.pbConn.Image = global::Mariinette2.Properties.Resources.connect_fail;
            this.pbConn.Location = new System.Drawing.Point(224, 390);
            this.pbConn.Name = "pbConn";
            this.pbConn.Size = new System.Drawing.Size(93, 61);
            this.pbConn.TabIndex = 11;
            this.pbConn.TabStop = false;
            // 
            // pbClientCom
            // 
            this.pbClientCom.BackColor = System.Drawing.Color.Transparent;
            this.pbClientCom.Image = global::Mariinette2.Properties.Resources.client_default;
            this.pbClientCom.Location = new System.Drawing.Point(327, 343);
            this.pbClientCom.Name = "pbClientCom";
            this.pbClientCom.Size = new System.Drawing.Size(133, 133);
            this.pbClientCom.TabIndex = 12;
            this.pbClientCom.TabStop = false;
            this.pbClientCom.Click += new System.EventHandler(this.pbClientCom_Click);
            this.pbClientCom.MouseLeave += new System.EventHandler(this.pbClientCom_MouseLeave);
            this.pbClientCom.MouseHover += new System.EventHandler(this.pbClientCom_MouseHover);
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Image = global::Mariinette2.Properties.Resources.bt_close_default;
            this.pbClose.Location = new System.Drawing.Point(470, 11);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(19, 19);
            this.pbClose.TabIndex = 25;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            this.pbClose.MouseLeave += new System.EventHandler(this.pbClose_MouseLeave);
            this.pbClose.MouseHover += new System.EventHandler(this.pbClose_MouseHover);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onShowToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 48);
            // 
            // onShowToolStripMenuItem
            // 
            this.onShowToolStripMenuItem.Name = "onShowToolStripMenuItem";
            this.onShowToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.onShowToolStripMenuItem.Text = "Marionette";
            this.onShowToolStripMenuItem.Click += new System.EventHandler(this.onShowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mariinette2.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.pbClientCom);
            this.Controls.Add(this.pbConn);
            this.Controls.Add(this.pbServerCom);
            this.Controls.Add(this.pbBtConn);
            this.Controls.Add(this.pbUseConn);
            this.Controls.Add(this.pbPhone);
            this.Controls.Add(this.pb_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Marionette";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUseConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbServerCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Title;
        private System.Windows.Forms.PictureBox pbPhone;
        private System.Windows.Forms.PictureBox pbUseConn;
        private System.Windows.Forms.PictureBox pbBtConn;
        private System.Windows.Forms.PictureBox pbServerCom;
        private System.Windows.Forms.PictureBox pbConn;
        private System.Windows.Forms.PictureBox pbClientCom;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem onShowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}


using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection; 
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Ports;
using InTheHand.Net.Bluetooth;

namespace Mariinette2
{
    public partial class Form1 : Form, ConnectState
    {
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string filename);

        [DllImport("user32.dll")]
        static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll")]
        static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);




        static public bool serialon = false;    // 현재 시리얼이 연결되어있는지 부울
        static public bool bluetoothon = false;  // 현재 블루투스가 연결되어있는지 부울

        static public bool bluetoothClipReceiveFlag = true;



        private Point mousePoint;
        private Marionette_KeyBoardMouseHook kmh;
        private BlueToothConn bluetoothConn;

        private const Int32 WM_CHANGECHAIN = 0x30D;
        private const Int32 WM_DRAWMESSAGE = 0x308;

        private IntPtr ClipboardViwerNext;

        private NotifyIcon trayicon = new NotifyIcon();
        private ContextMenu trayMenu = new ContextMenu();


        public Form1()
        {  
            InitializeComponent();
            ClipboardViwerNext = SetClipboardViewer(this.Handle);
            trayicon.Text = "MarioNette";
            trayicon.Icon = new Icon(Mariinette2.Properties.Resources._1402568262_118854, 40, 40);

        
            trayicon.ContextMenuStrip = this.contextMenuStrip1;
            trayicon.Visible = true;

            this.Cursor = Cursors.Default;

            
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeClipboardChain(this.Handle, ClipboardViwerNext);
            
        }

        public void SerialConnect()
        {
            serialCOm();
            ConnenctionOn();
            
        }
        public void SerialConnectOFF()
        {
            serialCoff();
            ConnenctionOFF();
            
        }
        public void BluetoothConnect()
        {
            BlueotoothCom();
            ConnenctionOn();
        }
        public void BluetoothConnectOFF()
        {
            
            BluetoothCoff();
            ConnenctionOFF();
        }
     
   


        protected override void WndProc(ref Message m)
        {
             base.WndProc(ref m);
            if (m.Msg == WM_DRAWMESSAGE)
            {
                    if (Clipboard.ContainsText())
                    {
                        String txt = Clipboard.GetText();
                        Console.WriteLine("Form 1> Clipboard GetText");
                        
                        //console.WriteLine("Form1> Clipboard txt : " + txt);
                        try
                        {
                            //console.WriteLine(serialon.ToString() + " " + bluetoothon.ToString());
                            if (kmh != null && serialon && bluetoothon == false)
                            {
                                kmh.senClipbooardText(txt);
                                //console.WriteLine("Form1> 시리얼로 안드로이드에게 클립 메시지 보냄");
                            }
                            else if (bluetoothConn != null && bluetoothon && serialon == false && bluetoothClipReceiveFlag)
                            {
                                //console.WriteLine("Form1> 블루투스로 보내겠습니다. "+bluetoothClipReceiveFlag);
                                bluetoothConn.sendClipdata(txt);
                                Console.WriteLine("Form1> 블루투스로 안드로이드에게 클립 메시지 보냄");
                            }
                            if (bluetoothClipReceiveFlag == false)
                            {
                                //console.WriteLine("Form1> 블루투스로 보내겠습니다. " + bluetoothClipReceiveFlag);
                                bluetoothClipReceiveFlag = true;
                            }
                        }
                        catch ( Exception e1){
                            //console.WriteLine("Form1> WndProc에서 예외 발생");
                            //console.WriteLine(e1.Message);
                        }
                    }
                    //SendMessage(ClipboardViwerNext, m.Msg, m.WParam, m.LParam);
            }
            else if (m.Msg ==  WM_CHANGECHAIN)
            {
                Console.WriteLine("여기오나?");
                    if ( m.WParam == ClipboardViwerNext)
                    {
                        ClipboardViwerNext = m.LParam;
                    }
                    else
                    {
                        SendMessage(ClipboardViwerNext,m.Msg,m.WParam ,m.LParam);
                    }
            }
        }
       


        

        #region 컴포넌트 UI
        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Image = global::Mariinette2.Properties.Resources.bt_close_default;
        }

        private void pbClose_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.Image = global::Mariinette2.Properties.Resources.bt_close_over;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X), this.Top - (mousePoint.Y - e.Y));
            }
        }

        private void pbSerOffCon_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pbSerOffCon_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        private void pbServerCom_MouseHover(object sender, EventArgs e)
        {
            this.pbServerCom.Image = global::Mariinette2.Properties.Resources.server_over;
            this.Cursor = Cursors.Hand;
        }

        private void pbServerCom_MouseLeave(object sender, EventArgs e)
        {
            this.pbServerCom.Image = global::Mariinette2.Properties.Resources.server_default;
            this.Cursor = Cursors.Default;
        }

        private void pbClientCom_MouseHover(object sender, EventArgs e)
        {
            this.pbClientCom.Image = global::Mariinette2.Properties.Resources.client_over;
            this.Cursor = Cursors.Hand;
        }

        private void pbClientCom_MouseLeave(object sender, EventArgs e)
        {
            this.pbClientCom.Image = global::Mariinette2.Properties.Resources.client_default;
            this.Cursor = Cursors.Default;
        }

        #endregion
        
        private void pbServerCom_Click(object sender, EventArgs e)
        {
            if (serialon)
                return;
            PopupServer ps = new PopupServer();
            if (ps.ShowDialog() == DialogResult.OK)
            {
                if (kmh == null)
                {
                    kmh = new Marionette_KeyBoardMouseHook(this as ConnectState);
                    kmh.hookStart();
                }
                else
                {
                    //console.Write("new kmf reference");
                    kmh.shutDown();
                    kmh.SocketComShutDown();
                    kmh.hookStop();
                    kmh = null;
                    kmh = new Marionette_KeyBoardMouseHook(this as ConnectState);
                    kmh.hookStart();
                    //console.Write("new kmf referenced");
                }
            }          
        }
        bool turnonSeri = false;
        bool turnofBlue = false;
        public void serialCOm()
        {
            this.pbUseConn.Image = global::Mariinette2.Properties.Resources.usb_suc; 
                 turnonSeri = true;
        }
        public void serialCoff()
        {
            this.pbUseConn.Image = global::Mariinette2.Properties.Resources.usb_fail;
            
            serialon = false;
            turnonSeri = false;
            this.pbConn.Image = global::Mariinette2.Properties.Resources.connect_fail;
        }
        public void BlueotoothCom()
        {
            turnofBlue = true;
            this.pbBtConn.Image = global::Mariinette2.Properties.Resources.bluetooth_suc;
        }
        public void BluetoothCoff()
        {
            this.pbBtConn.Image = global::Mariinette2.Properties.Resources.bluetooth_fail;
            bluetoothon = false;
            turnofBlue = false;
            this.pbConn.Image = global::Mariinette2.Properties.Resources.connect_fail;
        }
        public void ConnenctionOn()
        {
            if (turnonSeri && turnofBlue)
            {
                this.pbConn.Image = global::Mariinette2.Properties.Resources.connect_suc;
            }
        }
        public void ConnenctionOFF()
        {
            this.pbConn.Image = global::Mariinette2.Properties.Resources.connect_fail;
        }
        

        private void pbClientCom_Click(object sender, EventArgs e)
        {
            if (bluetoothon)
                return;
            popupClient pc = new popupClient();
            if (pc.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (bluetoothConn == null)
                    {
                        bluetoothConn = new BlueToothConn(this as ConnectState);
                    }
                    else
                    {
                        bluetoothConn.stop();
                        bluetoothConn = null;
                        bluetoothConn = new BlueToothConn(this as ConnectState);
                    }
                    CheckBlueTooth cb = new CheckBlueTooth();
                    cb.ShowDialog();
                }
                catch (Exception ex)
                {
                    //console.WriteLine(ex.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
           
          
        }

        private void onShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                kmh.shutDown();
                bluetoothConn.stop();
            }
            catch (Exception e1)
            { }
            this.Dispose();
            this.Close();
            Application.Exit();
        }

        private void pbUseConn_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pbUseConn_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pbBtConn_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pbBtConn_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pbUseConn_Click(object sender, EventArgs e)
        {
            if (serialon)
            {
                DisConnectSerial dis = new DisConnectSerial();
                if (dis.ShowDialog() == DialogResult.OK)
                {
                    kmh.shutDown();
                    serialCoff();
                }
            }
            
        }      
        
        private void pbBtConn_Click(object sender, EventArgs e)
        {
            if(bluetoothon)
            {
                 DisConnectBluetooth dis = new DisConnectBluetooth();
            
                if (dis.ShowDialog() == DialogResult.OK)
                {
                    //console.WriteLine("111");
                    bluetoothConn.stop();
                    BluetoothCoff();
                }
            }
        }

    }
}

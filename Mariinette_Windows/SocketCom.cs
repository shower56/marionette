using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Mariinette2
{

    class SocketCom 
    {
        private Process p;
        private ProcessStartInfo psi;
        private IPEndPoint ipep;
        private Socket socket;
        private int cnt = 0;
        private ConnectState cs;

        Thread serialReadThread;
        
        
        public SocketCom(ConnectState cs)
        {
            Console.WriteLine("SecketCom is Create");
            Form1.serialon = true;
            this.cs = cs;
            psi = new ProcessStartInfo();
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.FileName = @"cmd";
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;

            p = new Process();
            p.StartInfo = psi;
            p.Start();
            p.StandardInput.AutoFlush = true;

            Console.WriteLine("adb forward tcp:6549 tcp:6549");
            String command = "adb forward tcp:6549 tcp:6549";

            p.StandardInput.WriteLine(command);
            p.Close();

            ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6549);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

          

            try
            {
                
                socket.Connect(ipep);
                
                string[] str = new string[2];
                string fulName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                str = fulName.Split('\\');
                String myPcName = str[1];
                byte[] buf = new byte[128];
                buf[0] = 2;
                buf[1] = (byte)myPcName.Length;
                for (int i = 0; i < myPcName.Length; i++)
                    buf[i+2] = (byte)myPcName[i];

                socket.Send(buf);
                Console.WriteLine("소켓 연결 성공");
                cs.SerialConnect();
                
                serialReadThread = new Thread(new ThreadStart(SerialReceive));
                serialReadThread.SetApartmentState(ApartmentState.STA);                    
                serialReadThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("소켓연결 에러 "+ex);
                Console.WriteLine("소켓연결 에러 입니다");
                

            }
        }
        
        public void SerialReceive()
        {
            while (true)
            {
                
                try
                {
                    Byte[] bytes = new Byte[20000];
                    socket.Receive(bytes);
                    for (int i = 0; i < 40; i++)
                    {
                        Console.Write(bytes[i] + " ");
                    }
                    if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0 && bytes[6] == 0)   // 시리얼로  000 이 들어오면 시리얼 끊기
                    {
                        cnt++;
                        if (cnt >= 200)
                        {
                            Console.WriteLine("Serial Reading Thread is Shutdown");
                            cs.SerialConnectOFF();
                            cs.BluetoothConnectOFF();
                            Form1.serialon = false;
                            cnt = 0;
                            break;
                        }
                    }
                    if (bytes[0] == 3)     //  시리얼로 3 이 들어오면 클립보드 데이터가 들어온것
                    {
                        Console.WriteLine("Serial> ClipBoard Receive");
                        char[] charray = new char[bytes[1]];
                        for (int i = 0; i < bytes[1]; i++)
                        {
                            charray[i] = (char)bytes[i + 2];
                        }
                        String txt = new String(charray);
                        Console.WriteLine("Serial>  " + txt);
                        if (txt == Clipboard.GetText())
                            continue;
                        try
                        {
                            Clipboard.SetText(txt);
                            
                        }
                        catch (Exception ee) { Console.WriteLine(ee.Message); }
                      
                        Console.WriteLine("Data into Clipbaoard");
                    }
                    // 시리얼로 101 이 들어오면 블루투스가 연결 된것임
                    else if (bytes[0] == 101 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0)     
                    {
                        Console.WriteLine("블루투스 연결됨");
                        cs.BluetoothConnect();
                    }
                    // 시리얼로 100 이 들어오면 블루투스가 끊긴 것임
                    else if (bytes[0] == 100 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0)         
                    {
                        Console.WriteLine("블루투스 연결끊김");
                        cs.BluetoothConnectOFF();
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Serial Thread Exception");
                    Console.WriteLine(ee.Message);
                    cs.SerialConnectOFF();
                    
                    break;
                }
            }
        }
        
        public void socketClose()
        {
            socket.Close();
            Console.WriteLine("소켓 끊기 성공");
            cs.SerialConnectOFF();
            

        }
        public void socketSendClipText(String txt)  //클립보드 데이터 type는 3 즉, 클립보드 데이터를 보낼때는 3으로 보냄
        {
            int i;
            byte[] buf = new byte[20000];
            buf[0] = 3;
            buf[1] = (byte)((txt.Length/128)+1);
            for (i = 0; i < txt.Length  ; i++)
            {
                buf[i + 2] = (byte)txt[i];
            }
            Console.WriteLine("ClipData Serial Send");
            try
            {
                socket.Send(buf);
                
            }
            catch (Exception ee1)
            {
                cs.SerialConnectOFF();
                Console.WriteLine("클립보드 데이터 보내다가 예외 발생");
                Console.WriteLine(ee1.Message);
                
            }
        }
        
        public void socketSendMouseMsg(byte[] buf) // 마우스 데이터 type는 1 즉, 마우스 데이터를 보낼때는 1로 보냄
        {
            try
            {
                socket.Send(buf);
               // Console.WriteLine("소켓 전송 데이터 :" + buf[0] + " " + buf[1] + " " + buf[2] + " " + buf[3] + " " + buf[4] + " " + buf[5] + " " + buf[6]);
            }
            catch (Exception ee1)
            {
                cs.SerialConnectOFF();
                Console.WriteLine("마우스 데이터 보내다가 예외발생");
                Console.WriteLine(ee1.Message);
                
            }
        }
        public void socketSendKeyMsg(int type,int arg1,int arg2)   // 키보드 데이터 type는 9  즉, 키보드 데이터를 보낼때는 9 로 보냄
        {
            byte[] buf = new byte[128];
            buf[0] = (byte)type;
            buf[1] = (byte)arg1;
            buf[2] = (byte)arg2;
            try
            {
                socket.Send(buf);
                //Console.WriteLine("소켓 전송 데이터 :" + buf[0] + " " + buf[1] + " " + buf[2]);
            }
            catch (Exception ee1)
            {
                cs.SerialConnectOFF();
                Console.WriteLine("키보드 데이터 보내다가 예외발생");
                Console.WriteLine(ee1.Message);
                
            }
        }
           
    }
}

using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mariinette2
{
    class BlueToothConn
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtaInfo);
        
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        


        private const uint LBDOWN = 0x00000002; // 왼쪽 마우스 버튼 눌림
        private const uint LBUP = 0x00000004; // 왼쪽 마우스 버튼 떼어짐
        private const uint RBDOWN = 0x00000008; // 오른쪽 마우스 버튼 눌림
        private const uint RBUP = 0x000000010; // 오른쪽 마우스 버튼 떼어짐

        private ConnectState cs;

        private int cnt;

        Thread AcceptAndListeningThread;

        Boolean isConnected = false;

        BluetoothClient btClient;
        BluetoothListener btListener;
        NetworkStream stream;

        public BlueToothConn(ConnectState cs)
        {
            this.cs = cs;
            Form1.bluetoothon = true;
            cnt = 0;
            if (!BluetoothRadio.IsSupported)
            {
                throw new System.Exception("This Device not support Bluetooth");
            }
            else
            {
                AcceptAndListeningThread = new Thread(AcceptAndListen);
                AcceptAndListeningThread.SetApartmentState(ApartmentState.STA);
                AcceptAndListeningThread.Start();
            }
        }
      
        public void sendClipdata(String txt)
        {
            
            NetworkStream stream = btClient.GetStream();
            byte[] buf = new byte[20000];
            buf[0] = 3;
            buf[1] = (byte)txt.Length;
            for (int i = 0; i < txt.Length; i++)
            {
                buf[i + 2] = (byte)txt[i];
            }
            try
            {
                stream.Write(buf, 0, 20000);
                stream.Flush();
            }
            catch (Exception eee)
            {
                //Console.WriteLine("클라이언트 컴에서 블루투스 메시지 보내다가 에러발생");
                //Console.WriteLine(eee.Message);
            }
        }

        public void AcceptAndListen()     // 블루투스에서 받는 메세지 처리 함수
        {
                  
            while (true)
            {
                if (isConnected)
                {
                   
                    try
                    {
                        Byte[] bytes = new Byte[20000];

                        stream.Read(bytes, 0, 128);                          
                        
                        //stream.Flush();

                        //Console.WriteLine("Thread Read> " + bytes[0] + " " + bytes[1] + " " + bytes[2] + " " + bytes[4] + " " + bytes[5]);
                        for (int i =0; i < 20000; i++)
                        {
                            if(bytes[i] !=0)
                                continue;
                            else
                            {
                                //Console.WriteLine("Thread Read> bytes length : "+i);
                                break;
                            }
                        }
                        // 블루투스로 9 가 들어오면 키보드 데이터가 들어온것임
                        if (bytes[0] == 9)  
                        {
                            //Console.WriteLine("Thread> Key Message " + bytes[1] + " " +bytes[2]);

                            if (bytes[2] == 0)
                                keybd_event(bytes[1], 0, 0, 0);   // 키 누름
                            else if (bytes[2] == 1)
                                keybd_event(bytes[1], 0, 0x0002, 0);   // 키 땜                           
                                
                        }
                        // 시리얼이 끊어져있거나 끊어지게 되면 120을 수신받음
                        if (bytes[0] == 120 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0 && bytes[6] == 0 && bytes[7] == 0 && bytes[8] == 0 && bytes[9] == 0)  
                        {
                            //Console.WriteLine("Serial Disconnect");
                            cs.SerialConnectOFF();
                        }
                        // 시리얼이 연결되어있으면 121을 수신 받음
                        if (bytes[0] == 121 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0 && bytes[6] == 0 && bytes[7] == 0 && bytes[8] == 0 && bytes[9] == 0)  
                        {
                            //Console.WriteLine("Serial Connected");
                            cs.SerialConnect();
                        }
                        // 블루투스로 100이 들어오면 블루투스 끊기
                        if (bytes[0] == 100 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0 && bytes[6] == 0 && bytes[7] == 0 && bytes[8] == 0 && bytes[9] == 0) 
                        {
                            Form1.bluetoothon = false;
                            cs.BluetoothConnectOFF();
                            //Console.WriteLine("Bluetooth shutDown");
                            break;
                        }
                        // 블루투스로 0,0,0,0,0,0,0 이런 데이터가 100번 들어오면 끊긴것으로 간주하고 불루투스 끊음
                        if (bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0 && bytes[3] == 0 && bytes[4] == 0 && bytes[5] == 0 && bytes[6] == 0 && bytes[7] == 0 && bytes[8] == 0 && bytes[9] == 0) // 블루투스로 0 이 계속 들어오면 블루투스 끊기
                        {
                            cnt++;
                            if (cnt >= 200)
                            {
                                Form1.bluetoothon = false;
                                
                                //Console.WriteLine(cnt + " is Full");
                                //Console.WriteLine("Bluetooth shutDown");
                                cnt = 0;
                                cs.BluetoothConnectOFF();
                                cs.SerialConnectOFF();
                                break;
                            }
                        }
                        // 블루투스로 1이 들어오면 마우스 데이터가 들어온것임
                        if (bytes[0] == 1)  
                        {
                            int ptx = bytes[3] * 100 + bytes[4];
                            int pty = bytes[5] * 100 + bytes[6];
                            //Console.WriteLine("Thread> Mouse Message " + bytes[1] + " " + bytes[2] + " " + ptx + " " + pty);
                            if (bytes[1] == 0)
                                SetCursorPos(ptx, pty);
                            else if (bytes[1] == 1) //좌 다운
                                mouse_event(LBDOWN, 0, 0, 0, 0);
                            else if (bytes[1] == 2) // 좌 업
                                mouse_event(LBUP, 0, 0, 0, 0);
                            else if (bytes[1] == 4) // 우 다운
                                mouse_event(RBDOWN, 0, 0, 0, 0);
                            else if (bytes[1] == 5) // 우업
                                mouse_event(RBUP, 0, 0, 0, 0);

                        }
                        // 블루투스로 3이 들어오면 키보드 데이터가 들어온것임 
                        if (bytes[0] == 3)  
                        {
                            int j;
                            //Console.WriteLine("Thread> ClipBoard Receive");
                            for(j = 2; j < 126; j++)
                            {
                                if (bytes[j] != 0)
                                    continue;
                                else if ( bytes[j] == 0 )
                                    break;
                            }
                            String txt = Encoding.UTF8.GetString(bytes,2,j-2);
                            Console.WriteLine(txt.Length);
                            StringBuilder motherTxt = new StringBuilder(txt);
                            int temp = bytes[1];

                            if (temp > 1) 
                                for(int i=0; i<temp-1; i++) {
                                    stream.Read(bytes, 0, 128);         
                                    txt = Encoding.UTF8.GetString(bytes,0,128);
                                    motherTxt.Append(txt);
                                }
                            //Console.WriteLine("Thread> Mothertxt : " + motherTxt);

                            

                            try
                            {
                                Form1.bluetoothClipReceiveFlag = false;
                                
                                //Console.WriteLine("Thread> 블루투스로 받은거 다시 보낼꺼냐? "+Form1.bluetoothClipReceiveFlag);  
                               

                                if (Clipboard.GetText().Equals(motherTxt.ToString()))
                                {
                                    
                                    Console.WriteLine("Blutooth REC> 내용이 같음");
                                    
                                    continue;
                                }
                                else if(!Clipboard.GetText().Equals(motherTxt.ToString()))
                                {
                                    
                                    //Console.WriteLine("Bluetooth REC> 클립보드에서 텍스트 꺼내옴 ");
                                    Console.WriteLine("Bluetooth REC> " + Clipboard.GetText().Length +" "+ Clipboard.GetText());
                                    //Console.WriteLine("Bluetooth REC> 클립보드에 넣을 텍스트 ");
                                    Console.WriteLine("Bluetooth REC> " + motherTxt.ToString().Length + " " + motherTxt.ToString());
                                    
                                    Clipboard.SetText(motherTxt.ToString());
                                }
                                 
                                

                            }
                            catch (Exception eee) { //Console.WriteLine(eee.Message); 
                            }
                            ////Console.WriteLine("Thread> Data into Clipbaoard");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Thread> There is an error while listening connection");
                        //Console.WriteLine(ex.Message);
                        isConnected = btClient.Connected;
                        //Form1.bluetoothon = false;
                    }
                }
                else
                {
                    try
                    {
                        //btListener = new BluetoothListener(BluetoothService.s);
                        btListener = new BluetoothListener(BluetoothService.SerialPort);

                        //Console.WriteLine("Thread> Listener created with TCP Protocol service " + BluetoothService.SerialPort);
                        //Console.WriteLine("Thread> Starting Listener….");
                        btListener.Start();
                        //Console.WriteLine("Thread> Listener Started!");
                        //Console.WriteLine("Thread> Accepting incoming connection….");


                        //Console.WriteLine("Serial> "+Form1.serialon.ToString());
                        btClient = btListener.AcceptBluetoothClient();
                        //Console.WriteLine("Serial> " + Form1.serialon.ToString());



                        isConnected = btClient.Connected;
                        stream = btClient.GetStream();



                        //Console.WriteLine("Thread> A Bluetooth Device Connected!");
                        
                        cs.BluetoothConnect();
                        


                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine("Thread> There is an error while accepting connection");
                        //Console.WriteLine(e.Message);
                        //Console.WriteLine("Thread> Retrying….");
                        cs.BluetoothConnectOFF();
                        sendDisConnectMsg();
                        Form1.bluetoothon = false;
                    }
                }
            }
        }
        private void sendDisConnectMsg()
        {
            UTF8Encoding encoder = new UTF8Encoding();
            NetworkStream stream = btClient.GetStream();
            byte[] buf = new byte[128];
            buf[0] = 100;
            stream.Write(buf, 0, 1);
            stream.Flush();
        }

        public void stop()
        {
            try
            {
                //Console.WriteLine("1");
                cs.BluetoothConnectOFF();
                AcceptAndListeningThread.Abort();
                btClient.GetStream().Close();
                btClient.Dispose();
                btListener.Stop();
                Form1.bluetoothon = false;
            }
            catch (Exception)
            {
            }
        }
        
    }

    
}

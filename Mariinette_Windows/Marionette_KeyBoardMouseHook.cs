using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Mariinette2
{
    class Marionette_KeyBoardMouseHook
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wparam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelKeyboardProc _KeyBoackHookproc;
        private static LowLevelMouseProc _MouseHookproc;

        private IntPtr _KeyBoardhookID = IntPtr.Zero;
        private IntPtr _MousehookID = IntPtr.Zero;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private const int WM_LBUTTONDOWN = 0x0201;  //513
        private const int WM_LBUTTONUP = 0x0202;    //514
        private const int WM_MOUSEMOVE = 0x0200;    //512
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;

        private const int WH_MOUSE_LL = 14;

        private bool flag = false;
        private SocketCom SC;
       
       

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public Marionette_KeyBoardMouseHook(ConnectState cs)
        {
            try
            {
                SC = new SocketCom(cs);   // 소켓 연결
                Console.WriteLine("SocketCom object referenced");
                cs.SerialConnect();
            }
            catch (Exception ee) {
                Console.WriteLine("kmh constructor");
                Console.WriteLine(ee.Message);
            }
            _KeyBoackHookproc = KeyBoardHookCallback;
            _MouseHookproc = MouseHookCallback;
        }
        public void senClipbooardText(String txt)
        {
            SC.socketSendClipText(txt);
        }

        public void hookStart()
        {            
            _KeyBoardhookID = SetKeyBoardHook(_KeyBoackHookproc);
            _MousehookID = SetMouseHook(_MouseHookproc);
        }
        public void hookStop()
        {
            UnhookWindowsHookEx(_KeyBoardhookID);
            UnhookWindowsHookEx(_MousehookID);

        }
        public void SocketComShutDown()
        {
            SC = null;
        }

       

        private IntPtr SetKeyBoardHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }


        private IntPtr SetMouseHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void shutDown()
        {
            SC.socketSendKeyMsg(100, 0, 0);
        }

        // nCode = 이벤트가 있으면 0이상값, wParam( 키 업=256 다운=257) , lParam( 어떤키)
        private IntPtr KeyBoardHookCallback(int nCode, IntPtr wParam, IntPtr lParam) 
        {
            int vkCode = Marshal.ReadInt32(lParam);
            if (flag && nCode >= 0)
            {
                Console.WriteLine("키보드움직임");
                
                SC.socketSendKeyMsg(9, vkCode, wParam.ToInt32());
                
                return (IntPtr)1;
            }
            return CallNextHookEx(_KeyBoardhookID, nCode, wParam, lParam);
        }


        TransParentArea a = new TransParentArea();
        

        Boolean mouseFocuesflagging = true;
        //wParam(MOVE : 512, Ldown : 513 ,Lup :514, Rdown : 516, Rup : 517, wheel : 522) mouseData(wheelup : 7864320, wheeldown: 4287102976)        
        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam) 
        {
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

            if (hookStruct.pt.x > Screen.PrimaryScreen.Bounds.Width && Form1.serialon )

            {
                flag = true;                    
                a.Show();
                if (mouseFocuesflagging)
                {
                    SetCursorPos(1, hookStruct.pt.y);
                    mouseFocuesflagging = false;
                    return (IntPtr)1;
                }
                
            }
            else if (hookStruct.pt.x < 0)
            {
                a.Hide();
                flag = false;
                if (!mouseFocuesflagging)
                {
                    SetCursorPos(Screen.PrimaryScreen.Bounds.Width, hookStruct.pt.y);
                    mouseFocuesflagging = true;
                    return (IntPtr)1;
                }
                
            }
            
            if (flag && nCode >= 0)
            {

                byte[] result = new byte[128];
                result[0] = 1;
                result[1] = (byte)wParam.ToInt32();
                result[2] = (byte)hookStruct.mouseData;
                result[3] = (byte)(hookStruct.pt.x / 100);
                result[4] = (byte)(hookStruct.pt.x % 100);
                result[5] = (byte)(hookStruct.pt.y / 100);
                result[6] = (byte)(hookStruct.pt.y % 100);

                if (SC == null) 
                    Console.WriteLine("null referance");
                SC.socketSendMouseMsg(result);
                
                //return (IntPtr)1;
            } 

            return CallNextHookEx(_MousehookID, nCode, wParam, lParam);
        }
    }
}

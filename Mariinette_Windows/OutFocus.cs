using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.InteropServices;
        

namespace Mariinette2
{
    class OutFocus
    {

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private IntPtr nowHWnd;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private const int WM_LBUTTONDOWN = 0x0201;  //513
        private const int WM_LBUTTONUP = 0x0202;    //514
        private const int WM_MOUSEMOVE = 0x0200;    //512
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;

        public OutFocus()
        {

        }
        
        public void MessageTransfer(int msg, int wParam, int lParam)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                SendMessage(GetModuleHandle(curModule.ModuleName), msg, wParam, lParam);
            }
        }
    }
}

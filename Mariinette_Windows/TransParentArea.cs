using System;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Mariinette2
{
    public partial class TransParentArea : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string filename);

         [DllImport("user32.dll")﻿]
        public static extern int ShowCursor(bool bShow); 



        public TransParentArea()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Opacity = 0.01;

            Cursor mycursor = new Cursor(Cursor.Current.Handle);
            IntPtr colorcursorhandle = LoadCursorFromFile(@"C:\\Users\\Ahn\\Desktop\\Mariinette2\\cur.cur");
            mycursor.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, mycursor, new object[] { colorcursorhandle });
            this.Cursor = mycursor;
            this.TopMost = true;
            
        }    

    }
}

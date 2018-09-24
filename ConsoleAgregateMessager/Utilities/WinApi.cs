using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleAgregateMessager
{ 
   static class WinApi
    {
        public static UInt32 WM_MOUSEMOVE = 0x0200;
        public static UInt32 WM_LBUTTONDOWN = 0x0201;
        public static UInt32 MK_LBUTTON = 0x0001;
        public static UInt32 WM_LBUTTONUP = 0x0202;
        public static UInt32 WM_CHAR = 0x0102;
        public static UInt32 WM_MOUSEACTIVATE = 0x0021;
        public static UInt32 WM_KEYDOWN = 0x0100;
        public static UInt32 VK_RETURN = 0x0D;
        public static UInt32 WM_KEYUP = 0x0101;
        public static UInt32 VK_BACK = 0x08;
        public static UInt32 WM_CLOSE = 0x0010;
        public static UInt32 WM_PASTE = 0x0302;
        public static UInt32 VK_CONTROL = 0x11;
        public static UInt32 VK_SHIFT = 0x10;
        public static UInt32 VK_OEM_PLUS=0xBB;
        public static UInt32 VK_OEM_MINUS=0xBD;
        public static UInt32 VK_LCONTROL = 0xA2;
        public static UInt32 WM_CLEAR = 0x0303;




        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]

        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        // Delegate to filter which windows to include 

       

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, UInt32 nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

      private static IntPtr hwnd;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public static bool StartWork()
        {

            SendMessage(FindWindow("Qt5QWindowIcon", "Viber"), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            hwnd = IntPtr.Zero;
            hwnd = FindWindow("Qt5QWindowOwnDCIcon", null); // получаем хендл окна вайбера       
            while(hwnd == IntPtr.Zero)
                hwnd = FindWindow("Qt5QWindowOwnDCIcon", null);

            Thread.Sleep(500);
            int capacity = GetWindowTextLength(hwnd)+1;
            StringBuilder stringBuilder = new StringBuilder(capacity);
            GetWindowText(hwnd, stringBuilder, stringBuilder.Capacity);
            var test = stringBuilder.ToString();
            if (stringBuilder.ToString() == "Viber")
                return false;

            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 800, 600, 0x0040);
         
            return true;
        }

        public static void ClickNumber()
        {
        do { 
                    if (FindWindow("Qt5QWindowIcon", "Viber") != IntPtr.Zero)
                    {
                        SendMessage(FindWindow("Qt5QWindowIcon", "Viber"), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        Thread.Sleep(100);
                    }
                    SetForegroundWindow(hwnd);
                    SendCtrlhotKey('D');
            }
            while (!WaitSubject(257, 133));
                            
        }

       public static void EnterNumber(string number)
        {

            do
            {
                if (FindWindow("Qt5QWindowIcon", "Viber") != IntPtr.Zero)
                {
                    SendMessage(FindWindow("Qt5QWindowIcon", "Viber"), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    Thread.Sleep(100);
                }
                SendMessage(hwnd, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam(264, 134));
                SendMessage(hwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(264, 134));
                SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(254, 134));

                Clipboard.SetText(number);
                SetForegroundWindow(hwnd);
                SendCtrlhotKey('A');
                SendCtrlhotKey('V');
                Thread.Sleep(100);
            }
            while (!WaitSubject(288, 165));
               
              
            

        }
       
        public static void ClickMessage()
        {
            do
            {
                if (FindWindow("Qt5QWindowIcon", "Viber") != IntPtr.Zero)
                {
                    SendMessage(FindWindow("Qt5QWindowIcon", "Viber"), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    Thread.Sleep(200);
                }
                SendMessage(hwnd, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam(199, 477));
                Thread.Sleep(50);
                SendMessage(hwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(199, 477));
                SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(199, 477));
            }
            while (!WaitSubject(20, 104));
         
          
        }
        public static bool SendMsg(string text, string path = null, bool flag = false)
        {
            if (FindWindow("Qt5QWindowIcon", "Viber") != IntPtr.Zero)
            {
                SendMessage(FindWindow("Qt5QWindowIcon", "Viber"), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(100);
            }
            Thread.Sleep(50);
            SendMessage(hwnd, WM_MOUSEMOVE, (IntPtr)MK_LBUTTON, MakeLParam(1053, 61));
            SendMessage(hwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(1053, 61));
            SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(1053, 61));


            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 800, 600, 0x0040);
            if (flag)
                SendFile(path);
            // Thread.Sleep(5000);
            if (!string.IsNullOrEmpty( text))
            Clipboard.SetText(text);
            SetForegroundWindow(hwnd);
            Thread.Sleep(50);
            SendMessage(hwnd, WM_MOUSEMOVE, (IntPtr)MK_LBUTTON, MakeLParam(467, 546));
            SendMessage(hwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, MakeLParam(467, 546));
            SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(467, 546));

            SendCtrlhotKey('V');

            Thread.Sleep(50);

            if (FindWindow("Qt5QWindowIcon", "Viber") != IntPtr.Zero)
            {
                SendMessage(FindWindow("Qt5QWindowIcon", "Viber"), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                Thread.Sleep(100);
            }

            SendMessage(hwnd, WM_KEYDOWN, (IntPtr)VK_RETURN, (IntPtr)(MapVirtualKey(VK_RETURN, 0) << 16 | 1));



            SetForegroundWindow(hwnd);
            SendKeys.SendWait("~");

            Thread.Sleep(600);
            IntPtr errorhwnd = FindWindow("Qt5QWindowIcon", "Viber");
            if (errorhwnd != IntPtr.Zero)
            {
                SendMessage(errorhwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                return false;
            }
            else
            {
                return true;
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private static void SendCtrlhotKey(char key)
        {
            keybd_event(0x11, 0, 0, 0);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, 0x2, 0);
            keybd_event(0x11, 0, 0x2, 0);
        }

       private static Random r = new Random();
        static  private void SendFile(string path)
        {
           var mas = Directory.GetFiles(path).Where(f=>!f.Contains(".db")).ToArray();
            path = mas[r.Next(0, mas.Length)];

            Thread.Sleep(300);
             IntPtr hwnd = FindWindow("Qt5QWindowOwnDCIcon", null);
       
            SendMessage(hwnd, WM_MOUSEMOVE, IntPtr.Zero, MakeLParam(335, 544));
            Thread.Sleep(100);
				SendMessage(hwnd, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON,MakeLParam(335, 544));
            Thread.Sleep(50);
               SendMessage(hwnd, WM_LBUTTONUP, IntPtr.Zero, MakeLParam(335, 544));

            Thread.Sleep(500);
		        IntPtr dialog = FindWindow(null, "Отправить файл");
            if (dialog == IntPtr.Zero)
                dialog = FindWindow(null, "Send a File");
            while (dialog != IntPtr.Zero)
            {
                IntPtr file=FindWindowEx(dialog, IntPtr.Zero, "ComboBoxEx32", "");
				file=FindWindowEx(file, IntPtr.Zero, "ComboBox", "");
				file=FindWindowEx(file, IntPtr.Zero, "Edit", "");
				Clipboard.SetText(path);


                SendMessage(file, WM_CLEAR, IntPtr.Zero, IntPtr.Zero);
                SendMessage(file, WM_PASTE, IntPtr.Zero, IntPtr.Zero);
           
             IntPtr button = FindWindowEx(dialog, IntPtr.Zero, "Button", null);
             SendMessage(button, 0x00F5, IntPtr.Zero, IntPtr.Zero);
            Thread.Sleep(100);
			
                 dialog = FindWindow(null, "Отправить файл");
                if (dialog == IntPtr.Zero)
                    dialog = FindWindow(null, "Send a File");
                if (dialog != IntPtr.Zero)
                {
                    SetForegroundWindow(dialog);

                    SendKeys.SendWait("~");
                }

            }
            WaitSubject(442, 335, true);
				
		}

        [DllImport("user32.dll", EntryPoint = "SendMessage",  CharSet = CharSet.Auto)]
        static extern int SendMessage4(IntPtr hwndControl, uint Msg,  int wParam, int lParam);
        static int GetTextBoxTextLength(IntPtr hTextBox)
        {
            // helper for GetTextBoxText
            uint WM_GETTEXTLENGTH = 0x000E;
            int result = SendMessage4(hTextBox, WM_GETTEXTLENGTH,
              0, 0);
            return result;
        }



        public static bool WaitSubject(int x, int y, bool flag = false)
        {
            if (flag)
                Thread.Sleep(300);
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(printscreen as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            //if (flag)
            //    MessageBox.Show(printscreen.GetPixel(x, y).R + "");
            int kol = 0;
            while ((flag && printscreen.GetPixel(x, y).R < 221)|| (printscreen.GetPixel(x, y).R > 250 ))
            {
                graphics.Dispose();
                printscreen.Dispose();
                printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                graphics = Graphics.FromImage(printscreen as Image);

                graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

                Thread.Sleep(100);
                kol++;
                if(!flag)
                x += 2;
                if (kol > 10)
                    return false;
            }

            graphics.Dispose();
            printscreen.Dispose();
            return true;
          
        }

      
    }
    

   

}

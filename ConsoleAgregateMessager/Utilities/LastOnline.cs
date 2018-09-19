using Patagames.Ocr;
using Patagames.Ocr.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Utilities
{
    public static class CommonCode
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        public static int WndTopMost = -1;
        public static Int32 SWP_NOSIZE = 0x0001;
        public static Int32 SWP_SHOWWINDOW = 0x0040;
        public static Int32 SWP_HIDEWINDOW = 0x0080;

    }

    public static class LastOnline
    {
        private static IntPtr hwnd = IntPtr.Zero;

        public static async Task<string> GetLastSeen()
        {
            return await Task.Run(() =>
            {
                string lastonline;
                while (true)
                {
                    IntPtr hwnd = CommonCode.FindWindow("Qt5QWindowOwnDCIcon", null);
                    CommonCode.SetWindowPos(hwnd, CommonCode.WndTopMost, 0, 0, 0, 0, CommonCode.SWP_NOSIZE | CommonCode.SWP_SHOWWINDOW);
                    Bitmap screen = new Bitmap(167, 14);
                    using (Graphics g = Graphics.FromImage(screen))
                    {
                        g.CopyFromScreen(375, 113, 0, 0, screen.Size);
                        screen.Save("image1.png");
                        float width = screen.Width * (float)1.4;
                        float height = screen.Height * (float)1.4;
                        var brush = new SolidBrush(Color.White);
                        var bmp = new Bitmap((int)width, (int)height);
                        var graph = Graphics.FromImage(bmp);
                        graph.InterpolationMode = InterpolationMode.High;
                        graph.CompositingQuality = CompositingQuality.HighQuality;
                        graph.SmoothingMode = SmoothingMode.AntiAlias;
                        var scaleWidth = (int)(width);
                        var scaleHeight = (int)(height);
                        graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
                        graph.DrawImage(screen, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            for (int y = 0; y < bmp.Height; y++)
                            {
                                var pixel = bmp.GetPixel(x, y);
                                var color = Color.FromArgb(pixel.A, pixel.R, pixel.G, pixel.B);
                                if (color.R == 255 && color.G == 255 && color.B == 255 || color == Color.White)
                                    continue;
                                var brightness = color.GetBrightness();
                                if (brightness < 0.83 && x > 0)
                                {
                                    bmp.SetPixel(x, y, Color.Black);
                                    bmp.SetPixel(x - 1, y, Color.Black);
                                }
                                else if (brightness > 0.83)
                                {
                                    bmp.SetPixel(x, y, Color.White);
                                }
                            }
                        }
                        bmp.Save("image.png");
                    }
                    var api = OcrApi.Create();
                    api.Init(Languages.English);
                    string text = api.GetTextFromImage("image.png").ToLower();
                    if (text.Contains("mm ago"))
                        text = text.Replace("mm ago", "min ago");
                    if (text.Contains("last seen a long"))
                        lastonline = "Last seen a long time ago";
                    else
                        lastonline = text.Trim();
                    if (text.Contains("last seen") || text.Contains("online"))
                        break;
                    screen.Dispose();
                    api.Dispose();
                }
                return lastonline;
            });
        }
    }
}
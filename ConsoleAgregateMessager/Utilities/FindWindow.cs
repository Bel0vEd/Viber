using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAgregateMessager
{
    public static class FindWindow
    {

        public static string GetWindowText(IntPtr hWnd)
            
        {
            int size = WinApi.GetWindowTextLength(hWnd);
            if (size > 0)
            {
                var builder = new StringBuilder(size + 1);
                WinApi.GetWindowText(hWnd, builder, builder.Capacity);
                return builder.ToString();
            }

            return String.Empty;
        }

        /// <summary> Find all windows that match the given filter </summary>
        /// <param name="filter"> A delegate that returns true for windows
        ///    that should be returned and false for windows that should
        ///    not be returned </param>
        private static IEnumerable<IntPtr> FindWindows(WinApi.EnumWindowsProc filter)
        {
            IntPtr found = IntPtr.Zero;
            List<IntPtr> windows = new List<IntPtr>();

            
            WinApi.EnumWindows(delegate (IntPtr wnd, IntPtr param)
            {
                if (filter(wnd, param))
                {
                    // only add the windows that pass the filter
                    windows.Add(wnd);
                }

                // but return true here so that we iterate all windows
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary> Find all windows that contain the given title text </summary>
        /// <param name="titleText"> The text that the window title must contain. </param>
        public static IEnumerable<IntPtr> FindWindowsWithText(string titleText)
        {
            return FindWindows(delegate (IntPtr wnd, IntPtr param)
            {
                return GetWindowText(wnd).Contains(titleText);

            });
        }






    }
}

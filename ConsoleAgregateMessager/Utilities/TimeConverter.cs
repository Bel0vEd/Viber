using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleAgregateMessager
{
    public static class TimeConverter
        //Конвертит UnixTime в обычное время 
    {
        public static  string ConvertTime(string time)
        {
            var DoubleTime = Convert.ToDouble(time);
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(DoubleTime).ToLocalTime();
            var b = Convert.ToString(dtDateTime.TimeOfDay);
            return b;
        }

    }
}

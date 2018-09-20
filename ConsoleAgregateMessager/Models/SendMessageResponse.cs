using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    public class Data4
    {
        public string channel { get; set; }
        public string origin { get; set; }
        public string to { get; set; }
        public string type { get; set; }
        public string text { get; set; }
    }

    public class SendMsgResponse
    {
        public string e { get; set; }
        public string status { get; set; }
        public Data4 data { get; set; }
    }
}

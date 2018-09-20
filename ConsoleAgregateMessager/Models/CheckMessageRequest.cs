using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    public class Data5
    {
        public string channel { get; set; }
        public string origin { get; set; }
    }
    public class CheckMsgRequest
    {
        public Data5 data { get; set; }
    }
}

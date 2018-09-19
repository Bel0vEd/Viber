using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    public class CheckMessageRequest
    {
        public class Data
        {
            public string channel { get; set; }
            public string origin { get; set; }
        }
        public class CheckMsgRequest
        {

            public Data data { get; set; }
        }
    }
}

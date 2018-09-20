using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    public class Data2
    {
        public string origin { get; set; }
        public string to { get; set; }
        public string channel { get; set; }
        public string name { get; set; }
        public string lastseen { get; set; }
        public string messagestatus { get; set; }
    }
    public class StatusUserResponse
    {
        public string e { get; set; }
        public string status { get; set; }
        public Data2 data { get; set; }
    }
}

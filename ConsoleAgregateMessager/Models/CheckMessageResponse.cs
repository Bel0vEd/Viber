using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    public class sender
    {
        public string name { get; set; }
        public string msisdn { get; set; }
    }
    public class message
    {
        public string type { get; set; }
        public string text { get; set; }
    }
    public class messages
    {
        public sender sender { get; set; }
        public message message { get; set; }
    }
    public class Data6
    {
        public NumberOfMSG messages { get; set; }
        public string origin { get; set; }
        public string channel { get; set; }

    }
    public class NumberOfMSG
    {
        public int length { get; set; }
        public List<messages> AllMessages { get; set; }
    }
    public class CheckMsgResponse
    {
        public string e { get; set; }
        public string status { get; set; }
        public Data6 data { get; set; }
    }
}

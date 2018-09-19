using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    class UserStatusRequest
    {
        public class Data
        {
            public string channel { get; set; }
            public string origin { get; set; }
            public string to { get; set; }
        }

        public class StatusUserRequest
        {
            public string e { get; set; }
            public Data data { get; set; }
        }
    }
}

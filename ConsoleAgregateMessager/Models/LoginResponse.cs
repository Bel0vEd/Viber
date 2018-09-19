using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Models
{
    public class __invalid_type__1
    {
        public string origin { get; set; }
        public string channel { get; set; }
    }

    public class Origins
    {
        public int length { get; set; }
        public __invalid_type__1 __invalid_name__1 { get; set; }
    }

    public class Data
    {
        public Origins origins { get; set; }
    }

    public class LoginResponse
    {
     
            public string e { get; set; }
            public string status { get; set; }
            public Data data { get; set; }
        
    }
}

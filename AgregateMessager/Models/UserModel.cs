using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgregateMessager
{
    public enum StatusValue { none, erroe, ok }
    public enum MessageStatus { delivered, seen, none }

    public class UserModel
    {
     public StatusValue Status { get; set; }
        public string Name { get; set; }
        public string LastSeen { get; set; }
        public MessageStatus MessageStatus { get; set; }

    }
   
}

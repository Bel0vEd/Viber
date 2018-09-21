using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager
{
    class Program
    {
        static void Main(string[] args)
        
        {
            var names = opendb.Names();
            var numbers = opendb.Numbers();
            var vibercontacts = opendb.ViberContacts();
            var messagestatus = opendb.MessageStatuses();
            var time = opendb.TimeStamps();
            var chatid = opendb.ChatId();
            var contactid = opendb.ContactId();
            var contactid1 = opendb.ContactId1();
            var chatidmsg = opendb.ChatIdMsg();
        }
    }
}

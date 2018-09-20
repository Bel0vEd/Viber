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
            List<string> cc = opendb.Numbers();
            List<string> aa = opendb.Names();
            List<string> bb =  opendb.ViberContacts();
            
            
            Console.ReadKey();


        }
    }
}

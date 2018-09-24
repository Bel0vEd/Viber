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
            for (int i = 0; i < opendb.TimeStamps().Count; i++)
            {
                var b = opendb.MessageStatuses()[i];
                var c = opendb.TimeStamps()[i];
                var d = opendb.TextMessages()[i];

                Console.WriteLine(c + "\t" + b + "\t" +d);
                
            }
            Console.ReadKey();









        }


       
    }
    }



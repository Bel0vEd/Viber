using ConsoleAgregateMessager.Utilities;
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
            var a = General.Status(new Models.StatusUserRequest
            {
                data = new Models.Data1
                {
                    to = "+79511929402"
                }
            });
        }
    }
}

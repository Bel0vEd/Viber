using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgregateMessager.Controllers
{
    public class ViberController : ApiController
    {
       public void SendMessage(string text)
        {
           
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddMilliseconds(1536582718861).ToLocalTime();
            return dtDateTime;
        }

    }
}
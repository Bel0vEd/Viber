using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AgregateMessager.Controllers
{
    public class ViberController : ApiController
    {
     [System.Web.Http.HttpPost]
         public void SendMessage([FromBody]Message MSG)
        {

                Thread th = new Thread(()=>
                {
                WinApi.StartWork();
                WinApi.ClickNumber();
                WinApi.EnterNumber(MSG.to);
                WinApi.SendMsg(MSG.text);
                });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    
    
   
    }
}
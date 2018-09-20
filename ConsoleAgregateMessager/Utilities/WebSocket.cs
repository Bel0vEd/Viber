using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleAgregateMessager.Utilities
{
    public  class WebSocket
    {
        CancellationTokenSource source = new CancellationTokenSource();
        
        Uri siteUri = new Uri("https://websocket.org/echo.html");
        ClientWebSocket client = new ClientWebSocket();
        public void OpenSocket()
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            CancellationToken token = source.Token;
            client.ConnectAsync(siteUri, token);
            var b = client.ReceiveAsync(buffer, token);
           


        }
     
    }
}
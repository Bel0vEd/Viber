using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace ConsoleAgregateMessager
{
    class Program
    {
        public static bool IsConnected = true;
        public static string otvet = "";
        public static string vconsol = "";
        public static long timelast = 0;
        static void Main(string[] args)
        {
            int viber1 = 0;
            while (true)
            {
                bool viber = General.ViberWork();
                if (viber)
                {
                    viber1 = 0;
                    WebSocket();
                }
                else
                {
                    if (viber1 == 0)
                        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + "Viber не запущен");
                    viber1 = 1;
                    Thread.Sleep(60000);
                    continue;
                }
            }
        }
        public static void WebSocket()
        {
            using (var ws = new WebSocket("ws://messenger.happl.com:8080"))
            {
                string vremya = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ");
                ws.OnOpen += (sender, e) =>
                {
                    IsConnected = true;
                };
                TimerCallback tm = new TimerCallback(Count);
                System.Threading.Timer timer = new System.Threading.Timer(tm, null, 10000, 60000);
                void Count(object obj)
                {
                    otvet = General.Check(timelast);
                    timelast = Convert.ToInt64(Regex.Match(otvet, ".*(?={\"e\":\")").Value);
                    otvet = Regex.Match(otvet, "{\"e.*").Value;
                    string lengh = Regex.Match(otvet, "(?<=length\": ).*(?=}}})").Value;
                    if (lengh != "0,")
                    {
                        ws.Send(otvet);
                        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + FindNumber.Useak() + " Новые сообщения");
                    }
                    return;
                }
                Thread.Sleep(3000);
                string telo = "{\"e\":\"login\",\"data\":{\"origins\":{\"" + "79093449994" + "\":{\"channels\":[\"viber\"]}}}}";
                telo = telo.Replace("\\", "");
                ws.OnMessage += (sender, e) =>
                {
                    if (e.Data.Contains("\"e\":\"status\",\"data\":{\"channel\":\"viber\"".Replace("\\", "")) && General.ViberWork())
                    {
                        otvet = General.Status(e.Data);
                        otvet = otvet.Replace("msisdn", FindNumber.Useak());
                        ws.Send(otvet);
                        vconsol = Regex.Match(otvet, "(?<=to\":\").*?(?=\")").Value;
                        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + FindNumber.Useak() + " Статус номера " + vconsol);
                        return;
                    }
                    else if (e.Data.Contains("\"e\":\"status\",\"data\":{\"channel\":\"viber\"".Replace("\\", "")) && !General.ViberWork())
                    {
                        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + " Viber не авторизован ");
                        return;
                    }
                    else if (e.Data.Contains("{\"e\":\"send\",\"data\":{\"channel\":\"viber\"".Replace("\\", "")) && General.ViberWork())
                    {
                        otvet = General.Send(e.Data);
                        otvet = otvet.Replace("msisdn", FindNumber.Useak());
                        vconsol = Regex.Match(otvet, "(?<=to\":\").*?(?=\")").Value;
                        ws.Send(otvet);
                        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + FindNumber.Useak() + " Сообщение на номер " + vconsol);
                        return;
                    }
                    else if (e.Data.Contains("\"e\":\"status\",\"data\":{\"channel\":\"viber\"".Replace("\\", "")) && !General.ViberWork())
                    {
                        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + " Viber не авторизован ");
                        return;
                    }
                    Console.WriteLine(e.Data);
                };
                ws.OnClose += (sender, e) =>
                {
                    IsConnected = false;
                    Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss ") + "Ошибка соединения");
                };
                ws.Connect();
                ws.Send(telo);
                while (IsConnected)
                {
                    Thread.Sleep(300);
                }
            }
        }
    }
}




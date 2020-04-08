using ConsoleAgregateMessager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleAgregateMessager
{
    public static class General
    {
        public static string Status(string zapros)
        {
            string last = "";
            string status = "";
            string msgstatus = "";
            string chat = "";
            int contact = 0;
            string nomer = Regex.Match(zapros, "(?<=to\":\").*(?=\")").Value;
            try
            {
                WinApi.StartWork();
                WinApi.ClickNumber();
                WinApi.EnterNumber(nomer);
                WinApi.ClickMessage();
                last = LastOnline.GetLastSeen();
            }
            catch (Exception)
            {
                status = "error";
            }
            if (!last.Contains("last"))
            {
                last = "";
            }
            if (last.Contains("a long time ago"))
            {
                status = "error";
            }
            var avatars = opendb.Avatars();
            var numbers = opendb.Numbers();
            var clientname = opendb.ClientName();
            var vibercontacts = opendb.ViberContacts();
            var names = opendb.Names();
            var contactid = opendb.ContactId();
            var chatid = opendb.ChatId();
            var chatidmsg = opendb.ChatIdMsg();
            var messagestatus = opendb.MessageStatuses();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i].Contains(nomer))
                    contact = i;
            }
            if (vibercontacts[contact] == "0")
                status = "none";
            else
                status = "ok";
            string avatar = avatars[contact];
            string name = names[contact];
            if (name == "")
                name = clientname[contact];
            contact = contact + 2;
            for (int i = 0; i < contactid.Count; i++)
            {
                if (contactid[i].Contains(contact.ToString()))
                {
                    contact = i;
                    break;
                }
            }
            try
            {
                chat = chatid[contact];
                for (int i = chatidmsg.Count - 1; i > 0; i--)
                {
                    if (chatidmsg[i].Contains(chat) && messagestatus[i] != "0")
                    {
                        msgstatus = messagestatus[i];
                        break;
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                msgstatus = "nomessage";
            }
            try
            {
                if (msgstatus == "130" || msgstatus == "133")
                    msgstatus = "read";
                else if (msgstatus == "129")
                    msgstatus = "delivered";
                else if (msgstatus == "")
                    msgstatus = "nomessage";
                else if (Convert.ToInt32(msgstatus) > 0)
                    msgstatus = "notdelivered";
            }
            catch (FormatException)
            {
            }
            /*DateTime last1 = new DateTime();
            if (last.Contains("last seen today at"))
            {
                last = Regex.Match(last, "(?<=at ).*").Value;
                last1 = DateTime.Parse(last);
                status = "ok";
            }
            if (last.Contains("last seen yesterday at"))
            {
                last = Regex.Match(last, "(?<=at ).*").Value;
                last1 = DateTime.Parse(last).AddDays(-1);
                status = "ok";
            }
            if (last.Contains("min ago"))
            {
                last = Regex.Match(last, "(?<=seen ).*(?= min )").Value;
                double last2 = double.Parse(last);
                last1 = DateTime.Now.AddMinutes(-last2);
                status = "ok";
            }
            if (last.Contains("last seen on"))
            {
                last = Regex.Match(last, "(?<=on ).*").Value;
                last1 = DateTime.Parse(last);
                status = "ok";
            }
            if (last.Contains("a long time ago"))
            {
                status = "error";
            }
            if (last.Contains("moment ago"))
            {
                last1 = DateTime.Now.AddMinutes(-1);
                status = "ok";
            }*/
            string status1 = "{\"e\":\"status\",\"status\":\"" + status + "\",\"data\":{\"origin\":\"msisdn\",\"to\":\"+7" + nomer + "\",\"channel\":\"viber\",\"name\":\"" + name + "\",\"lastseen\":\"" + last + "\",\"messagestatus\":\"" + msgstatus + "\",\"avatar\":\"" + avatar + "\"}}";
            return status1;
        }
        public static string Send(string zapros)
        {
            string status = "";
            int contact = 0;
            string type = "";
            string text = Regex.Match(zapros, "(?<=text\":\").*?(?=\")").Value;
            string nomer = Regex.Match(zapros, "(?<=to\":\").*?(?=\")").Value;
            string path = Regex.Match(zapros, "(?<=file\":\").*?(?=\")").Value;
            var numbers = opendb.Numbers();
            var vibercontacts = opendb.ViberContacts();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i].Contains(nomer))
                    contact = i;
            }
            try
            {
                if (vibercontacts[contact] == "1")
                {
                    WinApi.StartWork();
                    WinApi.ClickNumber();
                    WinApi.EnterNumber(nomer);
                    WinApi.ClickMessage();
                    if (path != "")
                    {
                        type = "file";
                        WinApi.SendMsg(text, path, true);
                    }
                    else
                    {
                        type = "text";
                        WinApi.SendMsg(text, path, false);
                    }
                    status = "ok";
                }
                else
                    status = "error";
            }
            catch (Exception)
            {
                status = "error";
            }
            string send = "{\"e\":\"send\",\"status\":\"" + status + "\",\"data\":{\"channel\":\"viber\",\"origin\":\"msisdn\",\"to\":\"+7" + nomer + "\",\"type\":\"" + type + "\",\"text\":\"" + text + "\",\"file\":\"" + path + "\"}}";
            return send;
        }
        public static string Check(long timelast)
        {
            string type = "";
            int z = 0;
            string check = "";
            var chatidmsg = opendb.ChatIdMsg();
            var typemsg = opendb.Type();
            var images = opendb.Image();
            var info = opendb.Info();
            var timestamps = opendb.TimeStamps();
            var isread = opendb.IsRead();
            var contactidmsg = opendb.ContactIdMsg();
            var body = opendb.Body();
            var contactid = opendb.ContactId();
            var numbers = opendb.Numbers();
            var vibercontacts = opendb.ViberContacts();
            var names = opendb.Names();
            var messagestatus = opendb.MessageStatuses();
            int count = contactidmsg.Count;
            string[] newchat = new string[chatidmsg.Count];
            for (int i = 0; i < isread.Count; i++)
            {
                if (isread[i] == "0" && messagestatus[i] == "0" && timelast < Convert.ToInt64(timestamps[i]))
                {
                    timelast = Convert.ToInt64(timestamps[timestamps.Count - 1]);
                    count = i;
                    break;
                }
            }
            for (int i = count; i < contactidmsg.Count; i++)
            {
                if (!newchat.Contains(contactidmsg[i]) && contactidmsg[i] != "1" && isread[i] == "0")
                {
                    newchat[z] = contactidmsg[i];
                    z++;
                }
            }
            string[] newchat1 = new string[z];
            string[] name = new string[z];
            string[] nomer = new string[z];
            string[] messages = new string[z];
            for (int i = 0; i < z; i++)
            {
                name[i] = names[Convert.ToInt32(newchat[i]) - 2];
                nomer[i] = numbers[Convert.ToInt32(newchat[i]) - 2];
                newchat1[i] = newchat[i];
            }
            string letter = "";
            string letter1 = "";
            for (int i = 0; i < z; i++)
            {
                int nomermsg = 0;
                string letter2 = "";
                string[] tipi = new string[body.Count];
                for (int ii = count; ii < body.Count; ii++)
                {
                    if (isread[ii] == "0" && messagestatus[ii] == "0" && contactidmsg[ii] == newchat1[i])
                    {
                        if (typemsg[ii] == "1")
                        {
                            type = "text";
                            letter2 = letter2 + "\"message" + nomermsg + "\":{\"type\":\"" + type + "\",\"text\":\"" + body[ii] + "\"},";
                            nomermsg++;
                        }
                        if (typemsg[ii] == "6")
                        {
                            type = "voice";
                            letter2 = letter2 + "\"message" + nomermsg + "\":{\"type\":\"" + type + "\",\"text\":\"" + body[ii] + "\"},";
                            nomermsg++;
                        }
                        if (typemsg[ii] == "11")
                        {
                            type = "file";
                            info[ii] = Regex.Match(info[ii], "(?<=FileName\": \").*?(?=\",)").Value;
                            letter2 = letter2 + "\"message" + nomermsg + "\":{\"type\":\"" + type + "\",\"text\":\"" + info[ii] + "\"},";
                            nomermsg++;
                        }
                        if (typemsg[ii] == "3")
                        {
                            type = "video";
                            letter2 = letter2 + "\"message" + nomermsg + "\":{\"type\":\"" + type + "\",\"text\":\"" + body[ii] + "\"},";
                            nomermsg++;
                        }
                        if (typemsg[ii] == "2")
                        {
                            type = "image";
                            images[ii] = Regex.Match(images[ii], "(?<=ViberDownloads/).*").Value;
                            letter2 = letter2 + "\"message" + nomermsg + "\":{\"type\":\"" + type + "\",\"text\":\"" + body[ii] + "\",\"file\":\"" + images[ii] + "\"},";
                            nomermsg++;
                        }
                        if (body[ii].Contains("https://maps.yandex.ru"))
                        {
                            type = "location";
                            body[ii] = Regex.Match(body[ii], "(?<=ActionBody\":\").*?(?=\")").Value;
                            letter2 = letter2 + "\"message" + nomermsg + "\":{\"type\":\"" + type + "\",\"text\":\"" + body[ii] + "\"},";
                            nomermsg++;
                        }
                    }
                    letter1 = "\"" + i + "\":{\"sender\":{\"name\":\"" + name[i] + "\",\"msisdn\":\"" + nomer[i] + "\"}," + letter2 + "}";
                }
                if(letter1!="")
                    letter1 = letter1.Substring(0, letter1.Length - 2) + "}";
                letter = letter.Insert(letter.Length, letter1 + ",");
            }
            if (letter != "")
                letter = letter.Substring(0, letter.Length - 1);
            check = timelast + "{\"e\":\"messages\",\"status\":\"ok\",\"data\":{\"origin\":\"" + FindNumber.Useak() + "\",\"channel\":\"viber\",\"messages\":{\"length\": " + z + "," + letter + "}}}".Replace("\\", ""); ;
            return check;
        }
        public static bool ViberWork()
        {
            bool runviber = false;
            Process[] procList = Process.GetProcesses();
            for (int i = 0; i < procList.Length; i++)
            {
                if ("Viber" == procList[i].ProcessName)
                {
                    runviber = true;
                }
            }
            return runviber;
        }
    }
}

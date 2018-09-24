using ConsoleAgregateMessager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAgregateMessager.Utilities
{
    public static class General
    {
        public static LoginResponse Login(LoginRequest login) //ВМ
        {
            LoginResponse login1 = new LoginResponse();
            Data data1 = new Data();
            Origins origins1 = new Origins();
            __invalid_type__1 invalid_type_1_1 = new __invalid_type__1();
            var AllOrigins = FindWindow.FindWindowsWithText("Viber").ToList();
            for (int i = 0; i < AllOrigins.Count; i++)
            {
                invalid_type_1_1.origin = FindWindow.GetWindowText(AllOrigins[i]);
            }
            invalid_type_1_1.channel = "Viber";
            origins1.__invalid_name__1 = invalid_type_1_1;
            origins1.length = AllOrigins.Count;
            data1.origins = origins1;
            login1.e = "login";
            if (AllOrigins.ToString().Contains(login.e))
              {
                login1.status = "Ok";
              }
            else
              {
                login1.status = "Error";
              }
            
            login1.data = data1;
            return login1;
        }
        //ВМ
        public static async Task<StatusUserResponse> Status(StatusUserRequest user)
        {
            string text = await LastOnline.GetLastSeen();
            var chatidmsg = opendb.ChatIdMsg();
            var chatid = opendb.ChatId();
            var contactid = opendb.ContactId();
            var numbers = opendb.Numbers();
            var vibercontacts = opendb.ViberContacts();
            var names = opendb.Names();
            var messagestatus = opendb.MessageStatuses();
            var nomer = user.data.to;
            var a = numbers.FirstOrDefault(d => d == nomer);
            if (a == null) return null;//Error
            var index = numbers.IndexOf(a);
            string index1 = index.ToString();
            a = contactid.FirstOrDefault(d => d == index1);
            if (a == null) return null;
            int index2 = contactid.IndexOf(a);
            string chatid1 = chatid[index2];
            var t = messagestatus.Select(d => new Tuple<string, string>(d, chatidmsg[messagestatus.IndexOf(d)])).ToList();
            var msgstatusTuple = t.LastOrDefault(d => d.Item1 != "0" && d.Item2 == chatid1);
            if (msgstatusTuple == null) return null;
            var msgstatus = msgstatusTuple.Item1;//status
            StatusUserResponse status1 = new StatusUserResponse
            {
                e = user.e,
                status = vibercontacts[index],
                data = new Data2
                {
                    channel = "viber",
                    lastseen = text,
                    messagestatus = msgstatus,
                    name = names[index],
                    origin = user.data.origin,
                    to = user.data.to,
                }
            };
            return status1;
        }
        public static SendMsgResponse Send(SendMsgRequest text) //ВМ
        {
            SendMsgResponse sendMSG1 = new SendMsgResponse();
            Data4 data1 = new Data4();
            data1.channel = "Viber";
            var AllOrigins = FindWindow.FindWindowsWithText("Viber").ToList();
            for (int i = 0; i < AllOrigins.Count; i++)
            {
                data1.origin = FindWindow.GetWindowText(AllOrigins[i]);
            }  
            data1.type = "text";
         
            data1.to = //opendb.Numbers()[i] какой-то номер;
            
            data1.text = "0"; //TODO:набираемый текст;
            sendMSG1.e ="send";
            sendMSG1.status = "0";///
            sendMSG1.data = data1;
            return sendMSG1;

            
        }
        //ВМ
        public static CheckMsgResponse Check(CheckMsgRequest origin)
        {
            CheckMsgResponse check1 = new CheckMsgResponse
            {
                e = "",
                status = "",
                data = new Data6
                {
                    origin = "",
                    channel = "",
                    messages = new NumberOfMSG
                    {
                        length = 0,
                        AllMessages = new List<messages>()
                    }
                }
            };
            return check1;
        }
    }
}

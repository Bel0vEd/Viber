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
            invalid_type_1_1.origin = "0";//TODO:приделать поиск всех открытых окон вайбера;
            invalid_type_1_1.channel = "Viber";
            origins1.__invalid_name__1 = invalid_type_1_1;
            origins1.length = 0;    //TODO:приделать количество открытых окон вайбера;;
            data1.origins = origins1;
            login1.e = "0";
            login1.status = "0";
            login1.data = data1;
            return login1;
        }
        public static async Task<StatusUserResponse> Status(StatusUserRequest user)
        {
            string text = await LastOnline.GetLastSeen();
            StatusUserResponse status1 = new StatusUserResponse
            {
                e = "",
                status = "",
                data = new Data2
                {
                    channel = "viber",
                    lastseen = text,
                    messagestatus = "",
                    name = "",
                    origin = "",
                    to = "",
                }
            };
            return status1;
        }
        public static SendMsgResponse Send(SendMsgRequest text) //ВМ
        {
            SendMsgResponse sendMSG1 = new SendMsgResponse();
            Data4 data1 = new Data4();
            data1.channel = "Viber";
            data1.origin= "0"; //TODO:приделать поиск всех открытых окон вайбера;
            data1.type = "text";
            data1.to = "0"; //TODO:приделать i номер из БД;
            data1.text = "0"; //TODO:набираемый текст;
            sendMSG1.e = "0";
            sendMSG1.status = "0";
            sendMSG1.data = data1;
            return sendMSG1;

            
        }
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

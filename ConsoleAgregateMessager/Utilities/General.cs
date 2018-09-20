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
        public static UserStatusResponse Status(UserStatusRequest.StatusUserRequest user)
        {
            return null;
        }
        public static SendMessageResponse Send(SendMessageRequest.SendMsgRequest text) //ВМ
        {


            return null;
        }
        public static CheckMessageResponse Check(CheckMessageRequest.CheckMsgRequest origin)
        {
            return null;
        }
    }
}

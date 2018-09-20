using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleAgregateMessager
{
    public static class opendb
    {
        private static SQLiteDataAdapter sql = new SQLiteDataAdapter();
        
        private static BindingSource bs = new BindingSource();
        private static string path_config = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates)) + "Users\\" + Environment.UserName + "\\AppData\\Roaming\\ViberPC\\" + FindNumber.Number().First() + "\\viber.db";
        private static SQLiteFactory factory = null;
        private static SQLiteConnection connection = null;
        
        public static List<String> Names()
       
        //Создает лист Names из имён списка контактов
        {
            List<string> Names = new List<string>();

            try
            {
                DataTable dt = new DataTable();
        factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM Contact";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var Name = dt.Columns["Name"];
                foreach (DataRow dr in dt.Rows)
                {
                   
                        Names.Add(dr[Name].ToString());
                }


                connection.Close();
            }
            catch { }
            Names.RemoveAt(0);
            return Names;
        }
        public static List<String> Numbers()
        //Создает лист Numbers из номеров списка контактов
        {
            List<string> Numbers = new List<string>();

            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM Contact";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var Number = dt.Columns["Number"];
                foreach (DataRow dr in dt.Rows)
                {
                
                        Numbers.Add(dr[Number].ToString());
                }

                
                connection.Close();
            }
            catch { }
            Numbers.RemoveAt(0);
            return Numbers;
        }

        public static List<String> ViberContacts()
        //Создает лист ViberContacts , в котором указано, использует ли контакт Viber; 1-использует, 0 - не использует
        {
            List<string> ViberContacts = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM Contact";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var ViberContact = dt.Columns["ViberContact"];
                foreach (DataRow dr in dt.Rows)
                { 
                        ViberContacts.Add(dr[ViberContact].ToString());
                }

                connection.Close();
            }
            catch { }
            ViberContacts.RemoveAt(0);
            return ViberContacts;


        }
        public static List<String> MessageStatuses()
        //Создает лист MessageStatuses , в котором указаны статусы сообщений:
        //130-отправлено.но не прочитано;
        //133- отправлено и прочитано;
        //128 - нет подключения к Интернету; 
        //134-ошибка отправления, предлагает отправить заново
        //0 - полученные
        {
            List<string> MessageStatuses = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM MessageInfo";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var MessageStatus = dt.Columns["MessageStatus"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr[MessageStatus].ToString()))
                        MessageStatuses.Add(dr[MessageStatus].ToString());
                }


                connection.Close();
            }
            catch { }
            return MessageStatuses;
        }
            public static List<String> TimeStamps()
        //Создает лист TimeStamps, содержащий время отправки и получения сообщений в unix времени
        // Встроен конвертер unixtime в обычное время
        {
            List<string> TimeStamps = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM MessageInfo";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var TimeStamp = dt.Columns["TimeStamp"];
                foreach (DataRow dr in dt.Rows)
                {
                    TimeStamps.Add(TimeConverter.ConvertTime(dr[TimeStamp].ToString()));
                }


                connection.Close();
            }
            catch { }
            return TimeStamps;
        }


    }
}

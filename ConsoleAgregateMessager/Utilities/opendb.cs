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
        private static string path_config = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates)) + "Users\\" + Environment.UserName + "\\AppData\\Roaming\\ViberPC\\" + FindNumber.Useak() + "\\viber.db";
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
                var Name = dt.Columns ["Name"];
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
            catch(Exception e) {

            }
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

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
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

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var TimeStamp = dt.Columns["TimeStamp"];
                foreach (DataRow dr in dt.Rows)
                {
                    TimeStamps.Add(dr[TimeStamp].ToString());
                }
                connection.Close();
            }
            catch { }
            return TimeStamps;
        }
        public static List<String> ChatId()
        //создает лист с колонкой ChatId(номер чата) из ChatRelation
        {
            List<string> ChatId = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM ChatRelation";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var chatid = dt.Columns["ChatID"];
                foreach (DataRow dr in dt.Rows)
                {
                    ChatId.Add(dr[chatid].ToString());
                }

                connection.Close();
            }
            catch { }
            return ChatId;
        }
        public static List<String> ContactId()
        //создает лист с колонкой ContactId из ChatRelation
        {
            List<string> ContactId = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM ChatRelation";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var contactid = dt.Columns["ContactID"];
                foreach (DataRow dr in dt.Rows)
                {
                    ContactId.Add(dr[contactid].ToString());
                }

                connection.Close();
            }
            catch { }
            return ContactId;
        }
        public static List<String> ChatIdMsg()
        //создает лист с колонкой ChatId(номер чата с каким либо пользователем) из MessageInfo
        {
            List<string> ChatIdMsg = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var chatid = dt.Columns["ChatID"];
                foreach (DataRow dr in dt.Rows)
                {
                    ChatIdMsg.Add(dr[chatid].ToString());
                }

                connection.Close();
            }
            catch { }
            return ChatIdMsg;
        }
        public static List<String> ContactIdMsg()
        //создает лист с колонкой ContactId(номер пользователя) из MessageInfo
        {
            List<string> ContactIdMsg = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var contactid = dt.Columns["ContactID"];
                foreach (DataRow dr in dt.Rows)
                {
                    ContactIdMsg.Add(dr[contactid].ToString());
                }

                connection.Close();
            }
            catch { }
            return ContactIdMsg;
        }
        public static List<String> Body()
        //создает лист со всеми сообщениями
        {
            List<string> Body = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var body = dt.Columns["Body"];
                foreach (DataRow dr in dt.Rows)
                {
                    Body.Add(dr[body].ToString());
                }

                connection.Close();
            }
            catch { }
            return Body;
        }
        public static List<String> Type()
        //создает лист с типами сообщений
        {
            List<string> Type = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["MessageType"];
                foreach (DataRow dr in dt.Rows)
                {
                    Type.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            return Type;
        }
        public static List<String> IsRead()
        //создает лист со значениями IsRead из MessageInfo
        {
            List<string> IsRead = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["IsRead"];
                foreach (DataRow dr in dt.Rows)
                {
                    IsRead.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            return IsRead;
        }
        public static List<String> ClientName()
        //создает лист с именами на сервере
        {
            List<string> ClientName = new List<string>();
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
                var type = dt.Columns["ClientName"];
                foreach (DataRow dr in dt.Rows)
                {
                    ClientName.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            ClientName.RemoveAt(0);
            return ClientName;
        }
        public static List<String> Avatars()
        //создает лист аватарами
        {
            List<string> Avatars = new List<string>();
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
                var type = dt.Columns["DownloadID"];
                foreach (DataRow dr in dt.Rows)
                {
                    Avatars.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            Avatars.RemoveAt(0);
            return Avatars;
        }
        public static List<String> Image()
        //создает лист c картинками
        {
            List<string> Image = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["PayloadPath"];
                foreach (DataRow dr in dt.Rows)
                {
                    Image.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            return Image;
        }
        public static List<String> Info()
        //создает лист c информацией о сообщении
        {
            List<string> Info = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["MessageInfo"];
                foreach (DataRow dr in dt.Rows)
                {
                    Info.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            return Info;
        }
        public static List<String> Subject()
        //создает лист c информацией о сообщении
        {
            List<string> Subject = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["Subject"];
                foreach (DataRow dr in dt.Rows)
                {
                    Subject.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            return Subject;
        }
        public static List<String> IdMes()
        //создает лист c информацией о сообщении
        {
            List<string> IdMes = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT * FROM MessageInfo ORDER BY TimeStamp";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["EventID"];
                foreach (DataRow dr in dt.Rows)
                {
                    IdMes.Add(dr[type].ToString());
                }

                connection.Close();
            }
            catch { }
            return IdMes;
        }
        public static long LastMessageId()
        //создает лист c информацией о сообщении
        {
            List<string> LastMessageId = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = CheckMessage.db";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM Message";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["IdMessage"];
                foreach (DataRow dr in dt.Rows)
                {
                    LastMessageId.Add(dr[type].ToString());
                }
                connection.Close();
            }
            catch { }
            return Convert.ToInt64(LastMessageId.Last());
        }
        public static long LastMessageTime()
        //создает лист c информацией о сообщении
        {
            List<string> LastMessageTime = new List<string>();
            try
            {
                DataTable dt = new DataTable();
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = CheckMessage.db";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM Message";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var type = dt.Columns["Time"];
                foreach (DataRow dr in dt.Rows)
                {
                    LastMessageTime.Add(dr[type].ToString());
                }
                connection.Close();
            }
            catch { }
            return Convert.ToInt64(LastMessageTime.Last());
        }
        public static int Insert(long id, long time)
        //создает лист c информацией о сообщении
        {
            List<string> LastMessageTime = new List<string>();
            try
            {
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();
                connection.ConnectionString = "Data Source = CheckMessage.db";
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = @"INSERT INTO Message ('IdMessage', 'Time') values ('" + id + "', '" + time + "')";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                sql.SelectCommand = command;
                connection.Close();
            }
            catch { }
            return 1;
        }
    }
}

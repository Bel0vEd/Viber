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
    public static class FindNumber
    {
        private static SQLiteDataAdapter sql = new SQLiteDataAdapter();
        private static DataTable dt = new DataTable();
        private static BindingSource bs = new BindingSource();
        private static string path_config = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.CommonTemplates)) + "Users\\" + Environment.UserName + "\\AppData\\Roaming\\ViberPC\\config.db";
        private static SQLiteFactory factory = null;
        private static SQLiteConnection connection = null;
        public static List <String> Number()
        {
            List<string> IDs = new List<string>();
            try
            {
                factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
                connection = (SQLiteConnection)factory.CreateConnection();

                connection.ConnectionString = "Data Source = " + path_config;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = @"SELECT  * FROM Accounts";
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

                sql.SelectCommand = command;
                sql.Fill(dt);

                bs.DataSource = dt;
                var ID = dt.Columns["ID"];
                
                 foreach (DataRow dr in dt.Rows)
                {
                    IDs.Add (dr[ID].ToString());  
                }
                 
                connection.Close();
            }
            catch { }
            return IDs;
        }
    }
}

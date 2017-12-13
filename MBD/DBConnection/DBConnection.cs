using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MBD.DBConnection
{
    static class DBConnection
    {
        private static SQLiteConnection connection;


        public static SQLiteConnection openDB()
        {
            if (!File.Exists("database.db"))
            {
                SQLiteConnection.CreateFile("database.db");
            }
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            if (File.Exists("database.db"))
            {
                connection = new SQLiteConnection("Data Source=database.db;Version=3;");
                connection.Open();

                if (connection.State.Equals(ConnectionState.Open))
                {
                    Console.WriteLine("Database Open");
                }
            }
            

            return connection;
        }

        public static void closeDB()
        {
            if (!connection.State.Equals(ConnectionState.Closed))
            {
                Console.WriteLine("Database in State: " + connection.State);
                Console.WriteLine("Database closing...");
                connection.Close();
                Console.WriteLine("Database closed...");
            }
        }

        public static SQLiteConnection getConnection()
        {
            return connection;
        }
    }
}

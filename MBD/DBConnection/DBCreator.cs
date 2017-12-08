using MBD.Model.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.DBConnection
{
    class DBCreator
    {
        public void createSchema(SQLiteConnection connection)
        {
            string dropTable = "drop table if exists texts";
            string createTextTable = "CREATE TABLE texts (id integer primary key, title VARCHAR(128), content TEXT)";

            SQLiteCommand dropTableCommand = new SQLiteCommand(dropTable, connection);
            dropTableCommand.ExecuteNonQuery();

            SQLiteCommand createTextTableCommand = new SQLiteCommand(createTextTable, connection);
            createTextTableCommand.ExecuteNonQuery();
        }

        public int insertText(String title, String  content, SQLiteConnection connection)
        {
            string sql = "insert into texts (title, content) values (@title, @content)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SQLiteParameter("@title", title));
            command.Parameters.Add(new SQLiteParameter("@content", content));
            int result = command.ExecuteNonQuery();
            return result;
        }

        public void insertTextList(List<Data>  datas, SQLiteConnection connection)
        {
            SQLiteTransaction transaction = connection.BeginTransaction();
            foreach(var data in datas)
            {
                insertText(data.filename, data.file, connection);
            }
            transaction.Commit();
        }
    }
}

using MBD.Model.DB;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.DBConnection
{
    public class DBDataCreator
    {

        public void createData()
        {
            SQLiteConnection connection = DBConnection.openDB();
            DBCreator dbCreator = new DBCreator();
            dbCreator.createSchema(connection);

            List<Data> datas = new List<Data>();

            Data data1 = new Data();
            data1.file = "file text";
            data1.filename = "file1";
            datas.Add(data1);
            dbCreator.insertTextList(datas, connection);
            DBConnection.closeDB();

        }

    }
}

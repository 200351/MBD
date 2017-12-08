using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBD.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparator.Tests
{
    [TestClass()]
    public class DBCreateDataTest
    {
        public void createData()
        {
            DBDataCreator dbdata = new DBDataCreator();
            dbdata.createData();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.DBConnection
{
   public  class SQLCommand
    {
        public string query { get; set; }
        public Dictionary<string, string> parameters = new Dictionary<string, string>();
     }
}

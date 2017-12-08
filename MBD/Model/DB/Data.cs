using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.Model.DB
{
    public class Data
    {
        public long id { get; set; }
        public String file { get; set; }
        public String filename { get; set; }

        public String toString()
        {
            return "Id: " + id + " Filename: " + filename + " File: " + file;
        }
    }
}

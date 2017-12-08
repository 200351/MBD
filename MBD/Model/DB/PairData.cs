using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.Model.DB
{
    public class PairData
    {
        public Data data1 { get; set; }
        public Data data2 { get; set; }
        public double score { get; set; }

        public String toString()
        {
            return data1.toString() + "\n" + data2.toString() + "\n" + "Score: " + score;
        }
    }
}

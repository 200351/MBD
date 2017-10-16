using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD
{
    public class ComparationResult
    {
        public double weigth { get; set; }
        public double score { get; set; }
        public String filename1 { get; set; }
        public String filename2 { get; set; }
        public List<ComparationDetail> details { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD
{
    public class ComparationDetail
    {
        public int file1_sentence_number { get; set; }
        public String file1_full_sentence { get; set; }

        public int file2_sentence_number { get; set; }
        public String file2_full_sentence { get; set; }

        public IList<String> similarity { get; set; }
       

    }
}

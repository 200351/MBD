using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD
{
    public class ComparationDetail
    {
        private int file1_sentence_number { get; set; }
        private String file1_full_sentence { get; set; }

        private int file2_sentence_number { get; set; }
        private String file2_full_sentence { get; set; }

        private IList<String> similarity { get; set; }
       

    }
}

using MBD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD
{
    public interface IComparator
    {
         ComparationResult compare(ComparationInput input);

    }
}

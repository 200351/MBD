using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model;
using MBD.Controller.Impl;

namespace MBD.Controller.Comparator.Impl
{
    public class NumerOfLetters : AbstractComparator, IComparator
    {

        public override ComparationResult compare(ComparationInput input)
        {
            String text1toAnalyze = removeAllPunctuation(removeWhitespace(input.file1));
            String text2toAnalyze = removeAllPunctuation(removeWhitespace(input.file2));

            ComparationResult result = new ComparationResult();
            result.filename1 = input.filename1;
            result.filename2 = input.filename2;
            result.weigth = weight;
            result.score = text1toAnalyze.Length.Equals(text2toAnalyze.Length) ? 1 : 0;
            return result;
        }

        protected override double weight
        {
            get
            {
                return 1;
            }
        }
    }
}

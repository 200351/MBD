using MBD.Controller.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model;

namespace MBD.Controller.Comparator.Impl
{
    public class NumberOfSentenceComparator : AbstractComparator, IComparator
    {

        public override ComparationResult compare(ComparationInput input)
        {
            List<String> text1toAnalyze = splitToSentence(input.file1);
            List<String> text2toAnalyze = splitToSentence(input.file2);

            ComparationResult result = new ComparationResult();
            result.filename1 = input.filename1;
            result.filename2 = input.filename2;
            result.weigth = weight;
            result.score = text1toAnalyze.Count.Equals(text2toAnalyze.Count) ? 1 : 0;
            return result;
        }

        protected override double weight
        {
            get
            {
                return 0.5;
            }
            set
            {
                weight = value;
            }
        }
    }
}

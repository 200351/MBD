using MBD.Controller.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model;

namespace MBD.Controller.Comparator.Impl
{
    class SameSentenceComparator: AbstractComparator, IComparator
    {

        public override ComparationResult compare(ComparationInput input)
        {

            String text1LowerCase = toLowerCase(input.file1);
            String text2LowerCase = toLowerCase(input.file2);

            String text1RemovedManySpaces = changeManySpacesToOne(text1LowerCase);
            String text2RemovedManySpaces = changeManySpacesToOne(text2LowerCase);

            List<String> text1Sentences = splitToSentence(text1RemovedManySpaces);
            List<String> text2Sentences = splitToSentence(text2RemovedManySpaces);
            
            List<String> text1ToAnalyze = reduceListByEmpty(trim(text1Sentences));
            List<String> text2ToAnalyze = reduceListByEmpty(trim(text2Sentences));


            ComparationResult result = new ComparationResult();
            result.filename1 = input.filename1;
            result.filename2 = input.filename2;
            result.details = new List<ComparationDetail>();
            for(int i = 0; i < text1Sentences.Count(); i++)
            {
                for (int j = 0; j < text2Sentences.Count(); j++)
                {
                    var sentence1 = text1Sentences[i];
                    var sentence2 = text2Sentences[j];
                    if (sentence1.Equals(sentence2))
                    {
                        ComparationDetail detail = new ComparationDetail();
                        detail.file1_full_sentence = sentence1;
                        detail.file1_sentence_number = i;
                        detail.file2_full_sentence = sentence2;
                        detail.file2_sentence_number = j;
                        detail.similarity = new List<String>(sentence1.Split(space));
                        result.details.Add(detail);
                    }
                }
            }
            result.score = result.details.Count / (text1Sentences.Count + text2Sentences.Count);

            return result;
        }

        protected override double weight
        {
            get
            {
                return 3;
            }
        }
    }
}

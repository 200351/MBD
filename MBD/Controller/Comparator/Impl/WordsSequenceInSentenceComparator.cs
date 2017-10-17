using MBD.Controller.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model;

namespace MBD.Controller.Comparator.Impl
{
    public class WordsSequenceInSentenceComparator: AbstractComparator, IComparator
    {

        public override ComparationResult compare(ComparationInput input)
        {
            List<string> text1ToAnalyze, text2ToAnalyze;
            prepareTextsTOAnalyze(input, out text1ToAnalyze, out text2ToAnalyze);

            ComparationResult result = new ComparationResult();
            result.filename1 = input.filename1;
            result.filename2 = input.filename2;
            result.details = new List<ComparationDetail>();

            int countOfWords = input.countOfWord;
            int countOfSequence = 0;
            for (int i = 0; i < text1ToAnalyze.Count(); i++)
            {
                for (int j = 0; j < text2ToAnalyze.Count(); j++)
                {
                    var sentence1 = text1ToAnalyze[i];
                    var sentence2 = text2ToAnalyze[j];

                    List<String> words1 = splitToWord(sentence1);
                    List<String> words2 = splitToWord(sentence2);

                    if (words1.Count > countOfWords - 1 && words2.Count > countOfWords - 1)
                    {
                        List<String> sequence1 = createSequence(words1, countOfWords);
                        List<String> sequence2 = createSequence(words2, countOfWords);
                        countOfSequence += sequence1.Count() + sequence2.Count();
                        List<String> commonParts = sequence1.Where(item => sequence2.Contains(item)).ToList();
                        List<ComparationDetail> detailsPart = prepareDetails(i, j, sentence1, sentence2, commonParts);
                        result.details.AddRange(detailsPart);
                    }
                }
            }
            result.score = countScore(result, countOfSequence);
            result.weigth = prepareWeight(countOfWords);
            return result;
        }

        private double prepareWeight(int countOfWords)
        {
            double weight = (double)((double)countOfWords * 3.0) / ((double)countOfWords + 2.0);
            return weight;
        }

        private double countScore(ComparationResult result, int countOfSequence)
        {
            double score = 0;
            if (countOfSequence > 0)
            {
                score = (double)((2.0 * (double)result.details.Count) / (double)countOfSequence);
            }
            return Math.Round(score, 2, MidpointRounding.AwayFromZero); ;
        }

        private List<ComparationDetail> prepareDetails(int i, int j, string sentence1, string sentence2, List<string> commonParts)
        {
            List<ComparationDetail> details = new List<ComparationDetail>();
            foreach (String part in commonParts)
            {
                ComparationDetail detail = prepareDetail(i, j, sentence1, sentence2, part);
                details.Add(detail);
            }
            return details;
        }

        private ComparationDetail prepareDetail(int i, int j, string sentence1, string sentence2, string part)
        {
            ComparationDetail detail = new ComparationDetail();
            detail.file1_full_sentence = sentence1;
            detail.file1_sentence_number = i;
            detail.file2_full_sentence = sentence2;
            detail.file2_sentence_number = j;
            detail.similarity = new List<string>(part.Split(SPACE_CHAR));
            return detail;
        }

        private void prepareTextsTOAnalyze(ComparationInput input, out List<string> text1ToAnalyze, out List<string> text2ToAnalyze)
        {
            String text1LowerCase = toLowerCase(input.file1);
            String text2LowerCase = toLowerCase(input.file2);

            String text1RemovedManySpaces = changeManySpacesToOne(text1LowerCase);
            String text2RemovedManySpaces = changeManySpacesToOne(text2LowerCase);

            List<String> text1Sentences = splitToSentence(text1RemovedManySpaces);
            List<String> text2Sentences = splitToSentence(text2RemovedManySpaces);

            text1ToAnalyze = reduceListByEmpty(trim(text1Sentences));
            text2ToAnalyze = reduceListByEmpty(trim(text2Sentences));
        }

        protected override double weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }
    }
}

using MBD.Controller.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model;

namespace MBD.Controller.Comparator.Impl
{
    public class WordsIndependentOrderInSentenceComparator : AbstractComparator, IComparator
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
            int countOfParts = 0;
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
                        IEnumerable<IEnumerable<String>> parts1 = GetPermutations<String>(words1.AsEnumerable(), countOfWords);
                        IEnumerable<IEnumerable<String>> parts2 = GetPermutations<String>(words2.AsEnumerable(), countOfWords);
                        countOfParts += parts1.Count() + parts2.Count();

                        List<List<String>> commonParts = new List<List<string>>();
                        foreach (var part1 in parts1)
                        {
                            foreach (var part2 in parts2)
                            {
                                if (part1.All(p => part2.Contains(p)))
                                {
                                    commonParts.Add(part1.ToList());
                                }
                            }
                        }

                        List<ComparationDetail> detailsPart = prepareDetails(i, j, sentence1, sentence2, commonParts);
                        result.details.AddRange(detailsPart);
                    }
                }
            }

            result.score = countScore(result, countOfParts);
            result.weigth = prepareWeight(countOfWords);
            return result;
        }

        private double prepareWeight(int countOfWords)
        {
            double weight = (double)((double)countOfWords * 2.0) / ((double)countOfWords + 2.0);
            return Math.Round(weight, 2, MidpointRounding.AwayFromZero);
        }

        private double countScore(ComparationResult result, int countOfCommonParts)
        {
            double score = 1;
            if (countOfCommonParts > 0)
            {
                score = (double)((2.0 * (double)result.details.Count) / (double)countOfCommonParts);
            }
            return Math.Round(score, 2, MidpointRounding.AwayFromZero);
        }

        private List<ComparationDetail> prepareDetails(int i, int j, string sentence1, string sentence2,List<List<String>> commonParts)
        {
            List<ComparationDetail> details = new List<ComparationDetail>();
            foreach (List<String> part in commonParts)
            {
                ComparationDetail detail = prepareDetail(i, j, sentence1, sentence2, part);
                details.Add(detail);
            }
            return details;
        }

        private ComparationDetail prepareDetail(int i, int j, string sentence1, string sentence2, List<String> part)
        {
            ComparationDetail detail = new ComparationDetail();
            detail.file1_full_sentence = sentence1;
            detail.file1_sentence_number = i;
            detail.file2_full_sentence = sentence2;
            detail.file2_sentence_number = j;
            detail.similarity = part;
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

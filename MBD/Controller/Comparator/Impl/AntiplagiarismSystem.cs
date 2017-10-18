using MBD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.Controller.Comparator.Impl
{
    public class AntiplagiarismSystem
    {
        private List<int> countOfWord = new List<int>() { 2, 3, 4 };
        private static IComparator numberOfLetters = new NumerOfLettersComparator();
        private static IComparator numberOfWords = new NumberOfWordComparator();
        private static IComparator numberOfSentence = new NumberOfSentenceComparator();
        private static IComparator sameSentence = new SameSentenceComparator();
        private static IComparator wordsSequence = new WordsSequenceInSentenceComparator();
        private static IComparator wordIndependenceOrder = new WordsIndependentOrderInSentenceComparator();

        private List<IComparator> comparators = 
            new List<IComparator> {
                numberOfLetters, numberOfWords, numberOfSentence, sameSentence,
            };

        List<IComparator> comparatorsAdvance =
           new List<IComparator> {
                wordsSequence, wordIndependenceOrder
           };

        public double run(ComparationInput input)
        {
            List<ComparationResult> results =
                new List<ComparationResult>(comparators.Count() + comparatorsAdvance.Count());
            runComparators(input, results);
            runAdvanceComparators(input, results);
            double result = countResult(results);
            return result;
        }

        private double countResult(List<ComparationResult> results)
        {
            double numerator = 0;
            double denumerator = 0;
            foreach (var result in results)
            {
                numerator += (double) (result.score * result.weigth);
                denumerator += (double) result.weigth;
            }

            double score = denumerator == 0 ? 0 : (numerator / denumerator);
            return Math.Round(score, 2, MidpointRounding.AwayFromZero);
        }

        private void runAdvanceComparators(ComparationInput input, List<ComparationResult> results)
        {
            Parallel.ForEach(comparatorsAdvance, 
                c => 
                {
                    Parallel.ForEach(countOfWord, cow =>
                    {
                        ComparationInput newInput = new ComparationInput();
                        newInput.file1 = input.file1;
                        newInput.file2 = input.file2;
                        newInput.filename1 = input.filename1;
                        newInput.filename2 = input.filename2;
                        newInput.countOfWord = cow;
                        results.Add(c.compare(newInput));
                    });
                });
        }

        private void runComparators(ComparationInput input, List<ComparationResult> results)
        {
            Parallel.ForEach(comparators, 
                c => 
                {
                    results.Add(c.compare(input));
                });
        }
    }
}

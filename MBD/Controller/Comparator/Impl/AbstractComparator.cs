using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model;
using System.Text.RegularExpressions;

namespace MBD.Controller.Impl
{
    public abstract class AbstractComparator : IComparator
    {
        abstract protected double weight { get; set; }
        protected const Char POINT_CHAR = '.';
        protected const Char COMMA_CHAR = ',';
        protected const char SPACE_CHAR = ' ';
        protected const String EMPTY = "";
        protected const String SPACE = " ";


        public abstract ComparationResult compare(ComparationInput input);

        protected List<String> splitToSentence(String text)
        {
            List<String> result = null;
            if (text != null)
            {
                char[] newline = Environment.NewLine.ToCharArray();
                char[] split = newline.Concat(new Char[] { POINT_CHAR, COMMA_CHAR }).ToArray();
                result = new List<String>(text.Split(split, StringSplitOptions.RemoveEmptyEntries));
            }
            return result == null ? new List<String>() : result;
        }


        protected List<String> splitToWord(String text)
        {
            String[] split = text == null ? new String[] { } : text.Split(new Char[] { POINT_CHAR, COMMA_CHAR , SPACE_CHAR } );
            List<String> splitWithoutSpaces = trim(new List<String>(split));
            List<String> splitWithoutEmpty = reduceListByEmpty(splitWithoutSpaces);
            return splitWithoutEmpty;
        }

        protected String changeManySpacesToOne(String toConvert)
        {
            if (toConvert != null)
            {
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                toConvert = regex.Replace(toConvert, " ");
                return toConvert;
            }
            return EMPTY;
        }

        protected String removeAllPunctuation(String input)
        {
            return input == null ? EMPTY : new String(input.ToCharArray()
               .Where(c => Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c))
               .ToArray());
        }

        public String removeWhitespace(String input)
        {
            return input == null ? EMPTY : new String(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        protected String toLowerCase(String toConvert)
        {
            if (toConvert != null)
            {
                return toConvert.ToLower();
            }
            return EMPTY;
        }

        protected String trim(String toConvert)
        {
            if (toConvert != null)
            {
                return toConvert.Trim();
            }
            return EMPTY;
        }

        protected List<String> trim(List<String> toConvert)
        {
            if (toConvert != null)
            {
                toConvert = toConvert.Select(c => c.Trim()).ToList();
            }
            return toConvert;
            
        }

        protected List<String> reduceListByEmpty(List<String> toReduce)
        {
            if (toReduce != null)
            {
                toReduce = toReduce.Where(r => r.Trim().Length > 0).ToList();
            }
            return toReduce;
        }

        protected List<String> createSequence(List<String> words, int countOfWords)
        {
            List<String> sequences = new List<String>();
            for (int i = 0; i < words.Count - countOfWords + 1; i++)
            {
                String sequence = "";
                for (int j = i; j < i + countOfWords; j++)
                {
                    if (j < words.Count + 1)
                    {
                        sequence += words[j];
                        sequence += SPACE;
                    }
                }
                sequences.Add(sequence.TrimEnd());
            }

            return sequences;
        }
    }
}

﻿using System;
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
        abstract protected double weight { get; }
        private const Char point = '.';
        private const Char comma = ',';
        private const char space = ' ';
        private const String Empty = "";

        public abstract ComparationResult compare(ComparationInput input);

        protected List<String> splitToSentence(String text)
        {
            List<String> result = null;
            if (text != null)
            {
                result = new List<String>(text.Split(new Char[] { point, comma }));
            }
            return result == null ? new List<String>() : result;
        }


        protected List<String> splitToWord(String text)
        {
            String[] split = text == null ? new String[] { } : text.Split(new Char[] { point, comma , space } );
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
            return Empty;
        }

        protected String removeAllPunctuation(String input)
        {
            return input == null ? Empty : new String(input.ToCharArray()
               .Where(c => !Char.IsPunctuation(c))
               .ToArray());
        }

        public String removeWhitespace(String input)
        {
            return input == null ? Empty : new String(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        protected String toLowerCase(String toConvert)
        {
            if (toConvert != null)
            {
                return toConvert.ToLower();
            }
            return Empty;
        }

        protected String trim(String toConvert)
        {
            if (toConvert != null)
            {
                return toConvert.Trim();
            }
            return Empty;
        }

        protected List<String> trim(List<String> toConvert)
        {
            if (toConvert != null)
            {
                toConvert.ForEach(c => c.Trim());
            }
            return toConvert;
            
        }

        protected List<String> reduceListByEmpty(List<String> toReduce)
        {
            if (toReduce != null)
            {
                toReduce.RemoveAll(r => r.Trim().Length < 1);
            }
            return toReduce;
        }
    }
}

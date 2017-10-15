using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBD.Controller.Comparator.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD;
using MBD.Model;

namespace Comparator.Tests
{
    [TestClass()]
    public class NumberOfLettersTests
    {
        [TestMethod()]
        public void testIsEqualsLenghtTest()
        {
            IComparator comparator = new NumerOfLettersComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "testtesttest";
            input.file2 = "esttesttestt";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(1, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);

        }

        [TestMethod()]
        public void testIsEqualsLenghtRemoveWhiteSpacedAndPunctuationTest()
        {
            IComparator comparator = new NumerOfLettersComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "test,te   stt.est";
            input.file2 = "e   st,test.testt";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(1, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);

        }

        [TestMethod()]
        public void testIsNotEqualsLenghtTest()
        {
            IComparator comparator = new NumerOfLettersComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "testtesttest";
            input.file2 = "esttesttest";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0, result.score);
            Assert.AreEqual(1, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);

        }
    }
}
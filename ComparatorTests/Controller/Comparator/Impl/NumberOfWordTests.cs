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
    public class NumberOfWordTests
    {
        [TestMethod()]
        public void testIsEqualsNumerOfWordDots()
        {
            IComparator comparator = new NumberOfWordComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "test.test.test";
            input.file2 = "estt.estte.stt";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(0.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testIsEqualsNumerOfWordMix()
        {
            IComparator comparator = new NumberOfWordComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "test,test.test";
            input.file2 = "estt.estte stt";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(0.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testIsNotEqualsNumerOfWordMix()
        {
            IComparator comparator = new NumberOfWordComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "testtest.test";
            input.file2 = "estt.estte   stt";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0, result.score);
            Assert.AreEqual(0.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testIsEqualsNumerOfWordNullCheck()
        {
            IComparator comparator = new NumberOfWordComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(0.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }
    }
}
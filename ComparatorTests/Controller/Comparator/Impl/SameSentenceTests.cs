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
    public class SameSentenceTests
    {
        [TestMethod()]
        public void testTheSameTwoSentence()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest test.";
            input.file2 = "to jest test.";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheSameTwoSentenceWithManySpaces()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest    test.";
            input.file2 = "to     jest test.";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheSameTwoSentenceWithDiffrentCase()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "TO jest    test.";
            input.file2 = "to     JEST test.";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheSameTwoSentenceWithEmptySentence()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "TO jest    test.";
            input.file2 = "to     JEST test.    .";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheSameTwoSentenceDuplicate()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "TO jest    test. to     JEST test. to     JEST test.";
            input.file2 = "to     JEST test.    . to     JEST test.";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheManySentence()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "TO jest    test. to  nie    JEST test.";
            input.file2 = "to     JEST test.to     JEST test." +
                "to     JEST test.to     JEST test.to     JEST test."
                + "to     JEST test.to     JEST test.to     JEST test.";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0.67 , result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheManySentence2()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "TO jest    test. to  nie    JEST test. to  nie    JEST test. TO jest    test. ";
            input.file2 = "to     JEST test.to     JEST test." +
                "to     JEST test.to     JEST test.to     JEST test."
                + "to     JEST test.to     JEST test.to     JEST test. to  nie    JEST test";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testWithoutSimilarity()
        {
            IComparator comparator = new SameSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to      JEST test.";
            input.file2 = "to  nie    JEST test.";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0, result.score);
            Assert.AreEqual(3, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }
    }
}
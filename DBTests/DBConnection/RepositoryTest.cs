using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBD.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD.Model.DB;

namespace DBTests
{
    [TestClass()]
    public class RepositoryTest
    {
        [TestMethod()]
        public void testFindTextWithNameContainsAndSoreGreaterThen()
        {
            Repository repository = new Repository();
            const string filename = "nazwa1";
            const double score = 0.40;
            List<PairData> datas = repository.findTextWithNameContainsAndScoreGreaterThen(filename, score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.data1.filename.Contains(filename));
                    Assert.IsTrue(data.data2.filename.Contains(filename));
                    Assert.IsTrue(data.score > score);
                }

            }
        }

        [TestMethod()]
        public void testFindTextWithNameContainsAndScoreLessThen()
        {
            Repository repository = new Repository();
            const string filename = "nazwa1";
            const double score = 0.40;
            List<PairData> datas = repository.findTextWithNameContainsAndScoreLessThen(filename, score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.data1.filename.Contains(filename));
                    Assert.IsTrue(data.data2.filename.Contains(filename));
                    Assert.IsTrue(data.score < score);
                }

            }
        }

        [TestMethod()]
        public void testFindTextWithNameContainsAndScoreEquals()
        {
            Repository repository = new Repository();
            const string filename = "nazwa1";
            const double score = 0.49;
            List<PairData> datas = repository.findTextWithNameContainsAndScoreEquals(filename, score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.data1.filename.Contains(filename));
                    Assert.IsTrue(data.data2.filename.Contains(filename));
                    Assert.IsTrue(data.score == score);
                }

            }
        }

        [TestMethod()]
        public void testFindTextWithNameContainsAndScorGreatereEquals()
        {
            Repository repository = new Repository();
            const string filename = "nazwa2";
            const double score = 0.05;
            List<PairData> datas = repository.findTextWithNameContainsAndScoreGreaterEquals(filename, score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.data1.filename.Contains(filename));
                    Assert.IsTrue(data.data2.filename.Contains(filename));
                    Assert.IsTrue(data.score >= score);
                }

            }
        }

        [TestMethod()]
        public void testFindTextWithNameContainsAndScoreLessEquals()
        {
            Repository repository = new Repository();
            const string filename = "nazwa2";
            const double score = 0.29;
            List<PairData> datas = repository.findTextWithNameContainsAndScoreLessEquals(filename, score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.data1.filename.Contains(filename));
                    Assert.IsTrue(data.data2.filename.Contains(filename));
                    Assert.IsTrue(data.score <= score);
                }

            }
        }

        [TestMethod()]
        public void testFindTextWithWordContainsAndScoreLessEquals()
        {
            Repository repository = new Repository();
            const string word = "podobny";
            const double score = 0.30;
            List<PairData> datas = repository.findTextWithWordContainsAndScoreLessEquals(word, score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.data1.file.Contains(word));
                    Assert.IsTrue(data.data2.file.Contains(word));
                    Assert.IsTrue(data.score <= score);
                }

            }
        }

        [TestMethod()]
        public void testFindTextScoreGreaterThen()
        {
            Repository repository = new Repository();
            const double score = 0.70;
            List<PairData> datas = repository.findTextScoreGreaterThen(score);
            Assert.IsNotNull(datas);
            Assert.IsTrue(datas.Count > 0);
            if (datas != null)
            {
                foreach (var data in datas)
                {
                    Assert.IsTrue(data.score > score);
                }

            }
        }
    }
}
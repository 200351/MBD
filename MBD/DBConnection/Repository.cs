using MBD.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBD.DBConnection
{
    public class Repository
    {
        public List<PairData> findTextScoreGreaterThen(double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE {SCORE > @score}";
            command.parameters.Add("@score", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }

        public List<PairData> findTextWithNameContainsAndScoreGreaterThen(string name, double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE TITLE LIKE @title AND {SCORE > @score}";
            command.parameters.Add("@title", "%" + name + "%");
            command.parameters.Add("@score", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }

        public List<PairData> findTextWithNameContainsAndScoreLessThen(string name, double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE {SCORE < @score} AND TITLE LIKE @title";
            command.parameters.Add("@title", "%" + name + "%");
            command.parameters.Add("@score", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }

        public List<PairData> findTextWithNameContainsAndScoreEquals(string name, double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE {SCORE = @score} AND TITLE LIKE @title";
            command.parameters.Add("@title", "%" + name + "%");
            command.parameters.Add("@score", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }

        public List<PairData> findTextWithNameContainsAndScoreGreaterEquals(string name, double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE TITLE LIKE @title AND {SCORE >= @anotherscore}";
            command.parameters.Add("@title", "%" + name + "%");
            command.parameters.Add("@anotherscore", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }
        public List<PairData> findTextWithNameContainsAndScoreLessEquals(string name, double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE TITLE LIKE @title AND {SCORE <= @onemore}";
            command.parameters.Add("@title", "%" + name + "%");
            command.parameters.Add("@onemore", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }

        public List<PairData> findTextWithWordContainsAndScoreLessEquals(string name, double score)
        {
            SQLQueryExecutor executor = new SQLQueryExecutor();
            SQLCommand command = new SQLCommand();
            command.query = "SELECT * FROM TEXTS WHERE CONTENT LIKE @content AND {SCORE <= @score}";
            command.parameters.Add("@content", "%" + name + "%");
            command.parameters.Add("@score", score.ToString());
            List<PairData> datas = executor.executeQuery(command);
            return datas;
        }
    }
}

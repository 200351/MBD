using MBD.Controller.Comparator.Impl;
using MBD.Model;
using MBD.Model.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MBD.DBConnection
{
    public class SQLQueryExecutor
    {
        private const String EQUAL = "=";
        private const String GREATER = ">";
        private const String LESS = "<";
        private const String EQUAL_GREATER = ">=";
        private const String EQUAL_LESS = "<=";

        public List<PairData> executeQuery(SQLCommand query)
        {
            List<PairData> result = null;
            string queryString = query.query;
            string pattern = @"{\s{0,}SCORE\s{0,}(>|>=|<|<=|=)\s{0,}(@\w+)\s{0,}}";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(queryString);

            if (query != null && m.Success)
            {
                String fullScorePart = m.Value;
                Group signGroup = m.Groups[1];
                Group scoreParameterGroup = m.Groups[2];
                string signParameterPart = signGroup.Value;
                string scoreParameterPart = scoreParameterGroup.Value;
                Dictionary<String, String> parameters = query.parameters;

                if (parameters.ContainsKey(scoreParameterPart))
                {
                    double scoreParameter = double.Parse(parameters[scoreParameterPart]);
                    parameters.Remove(scoreParameterPart);
                    String queryToExecute = r.Replace(queryString, "1 == 1");
                    SQLiteConnection connection = DBConnection.openDB();
                    SQLiteCommand command = new SQLiteCommand(queryToExecute, connection);
                    command.CommandType = CommandType.Text;
                    addAllParameters(parameters, command);
                    SQLiteDataReader reader = command.ExecuteReader();
                    List<Data> datas = prepareDatas(reader);
                    DBConnection.closeDB();
                    result = analyze(scoreParameter, datas, signParameterPart);
                }
            }
            return result;
        }

        private List<PairData> analyze(double scoreParameter, List<Data> datas, string signParameterPart)
        {
            List<PairData> result = new List<PairData>();
            if (datas.Count > 1)
            {
                List<PairData> datasToFilter = runSystem(datas);
                result = datasToFilter.Where(compareFunction(scoreParameter, signParameterPart)).ToList();
            }

            return result;
        }

        private static Func<PairData, bool> compareFunction(double scoreParameter, string signParameterPart)
        {
            Func<PairData, bool> function = null;
            if (EQUAL.Equals(signParameterPart))
            {
                function = d => d.score == scoreParameter;
            }
            else if (GREATER.Equals(signParameterPart))
            {
                function = d => d.score > scoreParameter;
            }
            else if (LESS.Equals(signParameterPart))
            {
                function = d => d.score < scoreParameter;
            }
            else if (EQUAL_GREATER.Equals(signParameterPart))
            {
                function = d => d.score >= scoreParameter;
            }
            else if (EQUAL_LESS.Equals(signParameterPart))
            {
                function = d => d.score <= scoreParameter;
            }
            return function;
        }

        private List<PairData> runSystem(List<Data> datas)
        {
            List<PairData> pairs = new List<PairData>();
            foreach (var data1 in datas)
            {
                foreach (var data2 in datas)
                {
                    if (data1.id != data2.id)
                    {
                        AntiplagiarismSystem system = new AntiplagiarismSystem();
                        ComparationInput input = prepareInput(data1, data2);
                        double score = system.run(input);
                        PairData pair = new PairData();
                        pair.data1 = data1;
                        pair.data2 = data2;
                        pair.score = score;
                        pairs.Add(pair);
                    }
                }
            }
            return pairs;
        }

        private static void addAllParameters(Dictionary<string, string> parameters, SQLiteCommand command)
        {
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                command.Parameters.Add(new SQLiteParameter(entry.Key, entry.Value));
            }
        }

        private ComparationInput prepareInput(Data data1, Data data2)
        {
            ComparationInput cinput = new ComparationInput();
            cinput.file1 = data1.file;
            cinput.filename1 = data1.filename;
            cinput.file2 = data2.file;
            cinput.filename2 = data2.filename;
            return cinput;
        }

        private static List<Data> prepareDatas(SQLiteDataReader reader)
        {
            List<Data> datas = new List<Data>();
            while (reader.Read())
            {
                Data data = new Data();
                data.id = (long)reader["id"];
                data.file = (String)reader["content"];
                data.filename = (String)reader["title"];
                datas.Add(data);
            }

            return datas;
        }
    }
}

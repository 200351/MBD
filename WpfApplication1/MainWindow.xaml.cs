using MBD.DBConnection;
using MBD.Model.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String signParameter = "";
        String title = "";
        String score = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double retNum;
            var isNumeric = double.TryParse(score, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            if (isNumeric)
            {
                List<PairData> results = null;
                Repository repo = new Repository();
                resultTable.DataContext = null;
                if (SQLQueryExecutor.GREATER.Equals(signParameter))
                {
                    results = repo.findTextWithNameContainsAndScoreGreaterThen(title, retNum);
                }
                else if (SQLQueryExecutor.EQUAL_GREATER.Equals(signParameter))
                {
                    results = repo.findTextWithNameContainsAndScoreGreaterEquals(title, retNum);
                }
                else if (SQLQueryExecutor.EQUAL.Equals(signParameter))
                {
                    results = repo.findTextWithNameContainsAndScoreEquals(title, retNum);
                }
                else if (SQLQueryExecutor.EQUAL_LESS.Equals(signParameter))
                {
                    results = repo.findTextWithNameContainsAndScoreLessEquals(title, retNum);
                }
                else if (SQLQueryExecutor.LESS.Equals(signParameter))
                {
                    results = repo.findTextWithNameContainsAndScoreLessThen(title, retNum);
                }
                if (results != null)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Score", typeof(string));
                    table.Columns.Add("Filename1", typeof(string));
                    table.Columns.Add("Filename2", typeof(string));
                    table.Columns.Add("Text1", typeof(string));
                    table.Columns.Add("Text2", typeof(string));

                    int i = 0;
                    results = results.OrderBy(r => r.score).Reverse().ToList();
                    foreach (PairData data in results){
                        table.Rows.Add(data.score, data.data1.filename, data.data2.filename, data.data1.file,  data.data2.file);
                    }
                    resultTable.DataContext = table.DefaultView;
                }
            }

        }

        private void titleChanged(object sender, TextChangedEventArgs e)
        {
            title = titleBlock.Text;
        }

        private void scoreChanged(object sender, TextChangedEventArgs e)
        {
            score = scoreBlock.Text;
        }

        private void radioButtonChecked(object sender, RoutedEventArgs e)
        {

            if (sender.Equals(greater))
            {
                signParameter = SQLQueryExecutor.GREATER;
            }
            if (sender.Equals(greater_equals))
            {
                signParameter = SQLQueryExecutor.EQUAL_GREATER;
            }
            if (sender.Equals(equals))
            {
                signParameter = SQLQueryExecutor.EQUAL;
            }
            if (sender.Equals(less))
            {
                signParameter = SQLQueryExecutor.LESS;
            }
            if (sender.Equals(lessEquals))
            {
                signParameter = SQLQueryExecutor.EQUAL_LESS;
            }
        }
    }
}

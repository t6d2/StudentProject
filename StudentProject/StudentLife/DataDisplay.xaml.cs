using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace StudentLife
{
    public partial class DataDisplay : UserControl
    {
        public DataDisplay(DbConnection dbc)
        {
            InitializeComponent();

            LoadDataGrid(dbc, ToDoTasks_DataGrid, new OperatorsSQL(">=", "is", "or"));
            LoadDataGrid(dbc, DoneTasks_DataGrid, new OperatorsSQL("<", "is not", "and"));
        }

        public void LoadDataGrid(DbConnection dbc, DataGrid dg, OperatorsSQL op)
        {

            string referenceDate = "2018-04-01";    // data di riferimento fissa al 2018-04-01
            dbc.Comm.CommandText = "select h.Description, CONVERT(date, h.StartDate), CONVERT(date, h.EndDate), s.Description" +
                                    " from Homeworks as h " +
                                    "join Subjects as s on s.Id = h.SubjectId " +
                                    $"where (StartDate {op.MoreLessOperator} '{referenceDate}') " +
                                    $"{op.OrAndOperator} (h.EndDate {op.NullOperator} Null) " +
                                    "order by h.StartDate ";

            dbc.Comm.CommandType = CommandType.Text;
            var homeWorkList = new List<HomeWork>();
            using (SqlDataReader reader = dbc.Comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HomeWork hw = new HomeWork
                        {
                            Description = reader.GetString(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.IsDBNull(2)
                            ? (DateTime?)null
                            : reader.GetDateTime(2),
                            SubjectDescription = reader.GetString(3)
                        };
                        homeWorkList.Add(hw);
                    }
                    dg.ItemsSource = homeWorkList;
                }
            }
        }
    }

    class HomeWork
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SubjectDescription { get; set; }
    }
}

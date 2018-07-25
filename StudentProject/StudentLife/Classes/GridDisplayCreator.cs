using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;

namespace StudentLife.Classes
{
    public class GridDisplayCreator
    {
        private DbConnection Dbc { get; set; }

        public GridDisplayCreator(DbConnection dbc)
        {

            Dbc = dbc;
        }

        public void LoadDataGrid(DataGrid dg, OperatorsSQL op)
        {

            string referenceDate = "2018-04-01";    // data di riferimento fissa al 2018-04-01
            Dbc.Comm.CommandText = "select h.Description, CONVERT(date, h.StartDate), CONVERT(date, h.EndDate), s.Description" +
                                    " from Homeworks as h " +
                                    "join Subjects as s on s.Id = h.SubjectId " +
                                    $"where (StartDate {op.MoreLessOperator} '{referenceDate}') " +
                                    $"{op.OrAndOperator} (h.EndDate {op.NullOperator} Null) " +
                                    "order by h.StartDate ";

            Dbc.Comm.CommandType = CommandType.Text;
            var homeWorkList = new List<HomeWork>();
            using (SqlDataReader reader = Dbc.Comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HomeWork hw = new HomeWork
                        {
                            Description = reader.GetString(0),
                            Start_Date = reader.GetDateTime(1),
                            End_Date = reader.IsDBNull(2)
                            ? (DateTime?)null
                            : reader.GetDateTime(2),
                            SubjectDescription = reader.GetString(3)
                        };
                        hw.StartDate = hw.Start_Date.ToString("dd/MM/yyyy");
                        hw.EndDate = hw.End_Date == null ? "" : hw.End_Date.Value.ToString("dd/MM/yyyy");
                        homeWorkList.Add(hw);
                    }
                    dg.ItemsSource = homeWorkList;
                }
            }
        }
    }
}

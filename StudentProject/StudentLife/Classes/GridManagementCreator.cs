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
    public class GridManagementCreator
    {
        public DbConnection Dbc { get; set; }
        public DataManagement Dm { get; set; }
        public string TextHeader { get; set; }


        public GridManagementCreator(DbConnection dbc, DataManagement dm, string textHeader)
        {

            Dbc = dbc;
            Dm = dm;
            TextHeader = textHeader;
        }

        public void LoadDataManagementGrid(string stringSQL)
        {
            Dbc.Comm.CommandText = stringSQL;
            Dbc.Comm.CommandType = CommandType.Text;

            using (SqlDataReader reader = Dbc.Comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    Dm.Display_DataGrid.ItemsSource = dt.DefaultView;

                }
            }
        }

        public string CreateSQLString()
        {
            string stringSQL = "";
            switch (TextHeader)
            {
                case "Subjects":
                    {
                        IGenericTable subject = new Subject();
                        stringSQL = subject.PrepareSQLForDataManagementGrid();
                        break;
                    }

                case "ClassRoomTasks":
                    {
                        IGenericTable classRoomTask = new ClassRoomTask();
                        stringSQL = classRoomTask.PrepareSQLForDataManagementGrid();
                        break;
                    }

                case "HomeWorks":
                    {
                        IGenericTable homeWork = new HomeWork();
                        stringSQL = homeWork.PrepareSQLForDataManagementGrid();
                        break;
                    }
                case "ClassTask Types":
                    {
                        IGenericTable classRoomTaskType = new ClassRoomTaskType();
                        stringSQL = classRoomTaskType.PrepareSQLForDataManagementGrid();
                        break;
                    }

            }

            return stringSQL;

        }
    }
}

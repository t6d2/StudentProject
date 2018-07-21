using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace StudentLife.Classes
{
    public class ClassRoomTaskType : IGenericTable
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string PrepareQueryString { get; set; }

        public void PopulateUserCtrlFieldsfromDataGrid(StackPanel stackPanel, DataRowView drv)
        {
            Id = drv["Id"].ToString();
            Description = drv["Task Type Description"].ToString();

        }

        public void PrepareSQLStringForReadingDB()
        {
            PrepareQueryString = "select Id, Description as 'Task Type Description' from ClassRoomTaskTypes";
        }

        public void PrepareSQLStringForUpdateToDB()
        {
            PrepareQueryString = "update ClassRoomTaskTypes set Description = @Description " +
                               "where Id = @Id";
        }

        public void PrepareSQLStringForInsertToDB()
        {
            PrepareQueryString = "insert into ClassRoomTaskTypes (Description) " +
                          "values (@Description) ";
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select Id, Description as 'Task Type Description' from ClassRoomTaskTypes";
        }
    }
}

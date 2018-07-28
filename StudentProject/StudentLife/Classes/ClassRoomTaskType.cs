using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace StudentLife.Classes
{
    public class ClassRoomTaskType 
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string PrepareSQLStringForUpdateToDB()
        {
            return $"update ClassRoomTaskTypes set Description = '{Description}' " +
                        $"where Id = {Id}";
        }

        public string PrepareSQLStringForDeleteToDB()
        {
            return "delete from ClassRoomTaskTypes " +
                        $"where Id = {Id}";
        }

        public string PrepareSQLStringForInsertToDB()
        {
            return "insert into ClassRoomTaskTypes (Description) " +
                          $"values ('{Description}')";
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select Id, Description as 'Task Type Description' from ClassRoomTaskTypes";
        }
    }
}

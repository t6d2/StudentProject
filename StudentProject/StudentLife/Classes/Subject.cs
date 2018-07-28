using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace StudentLife.Classes
{
    public class Subject 
    {

        public string Id { get; set; }
        public string Description { get; set; }

        public string PrepareSQLStringForUpdateToDB()
        {
            return $"update Subjects set Description = '{Description}' " +
                $"where Id = {Id}";
        }

        public string PrepareSQLStringForDeleteToDB()
        {
            return "delete from Subjects " +
                 $"where Id = {Id}";
        }

        public string PrepareSQLStringForInsertToDB()
        {

            return "insert into Subjects (Description) " +
                $"values ('{Description}')";
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select Id, Description as 'Subject' from Subjects";
        }
    }
}

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
            var queryString = "";
            if (!String.IsNullOrEmpty(Description))
                queryString = $"update Subjects set Description = '{Description}' " +
                                 $"where Id = {Id}";
            return queryString;
        }

        public string PrepareSQLStringForDeleteToDB()
        {
            return "delete from Subjects " +
                                 $"where Id = {Id}";
        }

        public string PrepareSQLStringForInsertToDB()
        {
            var queryString = "";
            if (!String.IsNullOrEmpty(Description))
                queryString = "insert into Subjects (Description) " +
                          $"values ('{Description}')";
            return queryString;
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select Id, Description as 'Subject' from Subjects";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace StudentLife.Classes
{
    public class HomeWork 
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public string StartDate { get; set; }
        public DateTime? End_Date { get; set; }
        public string EndDate { get; set; }
        public string SubjectDescription { get; set; }

        public string PrepareSQLStringForUpdateToDB()
        {

            var queryString = "";
            if (!String.IsNullOrEmpty(Description) && !String.IsNullOrEmpty(StartDate))
            {
                string StartDateForSqlString =
                "'" + (StartDate.Substring(6, 4) + "-" + StartDate.Substring(3, 2) + "-" + StartDate.Substring(0, 2) + " 00:00:00'");
                string EndDateForSqlString =
                    !String.IsNullOrEmpty(EndDate)
                    ? "'" + (EndDate.Substring(6, 4) + "-" + EndDate.Substring(3, 2) + "-" + EndDate.Substring(0, 2) + " 00:00:00'")
                    : "null";
                queryString = $"update HomeWorks set Description = '{Description}', " +
                        $"StartDate = {StartDateForSqlString}, EndDate = {EndDateForSqlString}, " +
                        $"SubjectId = (select Id from Subjects where Description = '{SubjectDescription}') " +
                        $"where Id = {Id}";
            }
            return queryString;
        }

        public string PrepareSQLStringForDeleteToDB()
        {
            return "delete from HomeWorks " +
                                 $"where Id = {Id}";
        }

        public string PrepareSQLStringForInsertToDB()
        {
            var queryString = "";
            if (!String.IsNullOrEmpty(Description) && !String.IsNullOrEmpty(StartDate))
            {
                string StartDateForSqlString =
                "'" + (StartDate.Substring(6, 4) + "-" + StartDate.Substring(3, 2) + "-" + StartDate.Substring(0, 2) + " 00:00:00'");
                string EndDateForSqlString =
                    !String.IsNullOrEmpty(EndDate)
                    ? "'" + (EndDate.Substring(6, 4) + "-" + EndDate.Substring(3, 2) + "-" + EndDate.Substring(0, 2) + " 00:00:00'")
                    : "null";
                queryString = "insert into Homeworks (Description, StartDate, EndDate, SubjectId) " +
                          $"values ('{Description}', {StartDateForSqlString} , {EndDateForSqlString}, " +
                          $"(select Id from Subjects where Description = '{SubjectDescription}')) ";
            }
            return queryString;
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select h.Id, h.Description as 'Activity', " +
                        "convert(nvarchar(MAX), h.StartDate, 103) as 'Start Date', " +
                        "convert(nvarchar(MAX), h.EndDate, 103) as 'End Date', " +
                        "s.Description as 'Subject' " +
                        "from Homeworks as h " +
                        "left join Subjects as s on s.Id = h.SubjectId";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace StudentLife.Classes
{
    public class ClassRoomTask 
    {
        public string Id { get; set; }
        public string WhenDate { get; set; }
        public string Vote { get; set; }
        public string ClassRoomTaskType { get; set; }
        public string SubjectDescritpion { get; set; }

        public string PrepareSQLStringForUpdateToDB()
        {

            string WhenDateForSqlString =
            !String.IsNullOrEmpty(WhenDate)
                ? "'" + (WhenDate.Substring(6, 4) + "-" + WhenDate.Substring(3, 2) + "-" + WhenDate.Substring(0, 2) + " 00:00:00'")
                : "null";
            string VoteForSqlString =
                !String.IsNullOrEmpty(Vote)
                ? Vote
                : "null";
            return $"update ClassRoomTasks set WhenDate = {WhenDateForSqlString}, Vote = {VoteForSqlString}, " +
                    $"TaskId = (select Id from ClassroomTaskTypes where Description = '{ClassRoomTaskType}'), " +
                    $"SubjectId = (select Id from Subjects where Description = '{SubjectDescritpion}') " +
                    $"where Id = {Id} ";
        }

        public string PrepareSQLStringForDeleteToDB()
        {
            return "delete from ClassRoomTasks " +
                                 $"where Id = {Id}";
        }

        public string PrepareSQLStringForInsertToDB()
        {

            string WhenDateForSqlString =
                !String.IsNullOrEmpty(WhenDate)
                ? "'" + (WhenDate.Substring(6, 4) + "-" + WhenDate.Substring(3, 2) + "-" + WhenDate.Substring(0, 2) + " 00:00:00'")
                : "null";
            string VoteForSqlString =
                !String.IsNullOrEmpty(Vote)
                ? Vote
                : "null";
            return "insert into ClassRoomTasks (WhenDate, Vote, TaskId, SubjectId) " +
                        $"values ({WhenDateForSqlString}, {VoteForSqlString}, " +
                        $"(select Id from ClassroomTaskTypes where Description = '{ClassRoomTaskType}')," +
                        $"(select Id from Subjects where Description = '{SubjectDescritpion}')) ";
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select ct.Id, convert(nvarchar(MAX), ct.WhenDate, 103) as 'Date', " +
                        "ct.Vote, crt.Description as 'Task Type', s.Description as 'Subject' " +
                        "from ClassRoomTasks as ct " +
                        "left Join ClassroomTaskTypes as crt on crt.Id = ct.TaskId " +
                        "left join Subjects as s on s.Id = ct.SubjectId";
        }

    }
}

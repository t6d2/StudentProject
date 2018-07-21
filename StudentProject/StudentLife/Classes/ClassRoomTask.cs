using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace StudentLife.Classes
{
    public class ClassRoomTask : IGenericTable
    {
        public string Id { get; set; }
        public string WhenDate { get; set; }
        public string Vote { get; set; }
        public List<ClassRoomTaskType> ListClassRoomTaskType { get; set; }
        public string ClassRoomTaskType { get; set; }
        public List<Subject> ListSubjectsDescription { get; set; }
        public string SubjectDescritpion { get; set; }
        public string PrepareQueryString { get; set; }

        public void CreateToDB()
        {
            throw new NotImplementedException();
        }

        public void PopulateUserCtrlFieldsfromDataGrid(StackPanel stackPanel, DataRowView drv)
        {
            //Id = CRT_Id_Block.Text = drv["Id"].ToString();
            //WhenDate = CRT_When_DatePicker.Text = drv["Date"].ToString();
            //Vote = CRT_Vote_TextBox.Text = drv["Vote"].ToString();
            //ClassRoomTaskType = CRT_Type_ComboBox.Text = drv["Task Type"].ToString();
            //SubjectDescritpion = CRT_Subject_ComboBox.Text = drv["Subject"].ToString();
        }

        public void PrepareSQLStringForReadingDB()
        {
            PrepareQueryString = "select ct.Id, convert(nvarchar(MAX), ct.WhenDate, 103) as 'Date', " +
                        "ct.Vote, crt.Description as 'Task Type', s.Description as 'Subject' " +
                        "from ClassRoomTasks as ct " +
                        "left Join ClassroomTaskTypes as crt on crt.Id = ct.TaskId " +
                        "left join Subjects as s on s.Id = ct.SubjectId";
        }

        public void PrepareSQLStringForUpdateToDB()
        {
            PrepareQueryString = "update ClassRoomTasks " +
                                 "set WhenDate = @WhenDate, Vote = @Vote, TaskId = @TaskId, SubjectId = @SubjectId " +
                                 "where Id = @Id";
        }

        public void PrepareSQLStringForInsertToDB()
        {
            PrepareQueryString = "insert into ClassRoomTasks (WhenDate, Vote, TaskId) " +
                          "values (@WhenDate, @Vote, @TaskId, @SubjectId) ";
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

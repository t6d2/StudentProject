using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data;

namespace StudentLife.Classes
{
    public class HomeWork : IGenericTable
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public string StartDate { get; set; }
        public DateTime? End_Date { get; set; }
        public string EndDate { get; set; }
        public List<Subject> ListSubjectsDescription { get; set; }
        public string SubjectDescription { get; set; }
        public string PrepareQueryString { get; set; }

        public void CreateToDB()
        {
            throw new NotImplementedException();
        }

        public void PopulateUserCtrlFieldsfromDataGrid(StackPanel stackPanel, DataRowView drv)
        {
            //Id = HW_Id_Block.Text = drv["Id"].ToString();
            //Description = HW_Homework_TextBox.Text = drv["Activity"].ToString();
            //StartDate = HW_StartDate_DatePicker.Text = drv["Start Date"].ToString();
            //EndDate = drv["End Date"].ToString();
            //SubjectDescription = HW_EndDatePicker.Text = drv["Subject"].ToString();
        }

        public void PrepareSQLStringForReadingDB()
        {
            PrepareQueryString = "select h.Id, h.Description as 'Activity', " +
                        "convert(nvarchar(MAX), h.StartDate, 103) as 'Start Date', " +
                        "convert(nvarchar(MAX), h.EndDate, 103) as 'End Date', " +
                        "s.Description as 'Subject' " +
                        "from Homeworks as h " +
                        "left join Subjects as s on s.Id = h.SubjectId";
        }

        public void PrepareSQLStringForUpdateToDB()
        {
            PrepareQueryString = "update HomeWorks " +
                          "set Description = @Description, set StartDate = @StartDate, EndtDate = @EndtDate, SubjectId = @SubjectId " +
                          "where Id = @Id";
        }

        public void PrepareSQLStringForInsertToDB()
        {
            PrepareQueryString = "insert into ClassRoomTasks (WhenDate, Vote, TaskId) " +
                          "values (@WhenDate, @Vote, @TaskId, @SubjectId) ";
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

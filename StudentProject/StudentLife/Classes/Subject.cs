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
    public class Subject : IGenericTable, INotifyPropertyChanged
    {

        public string Id { get; set; }

        private string _Description { get; set; }
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;

            }
        }

        public string PrepareQueryString { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void CreateToDB()
        {
            throw new NotImplementedException();
        }

        public void PopulateUserCtrlFieldsfromDataGrid(StackPanel stackPanel, DataRowView drv)
        {

            //Id = SubjectId_TextBlock.Text = drv["Id"].ToString();
            //Description = SubjectDesciption_TextBox.Text = drv["Subject"].ToString();
        }

        public void PrepareSQLStringForReadingDB()
        {
            PrepareQueryString = "select Id, Description as 'Subject' from Subjects";
        }

        public void PrepareSQLStringForUpdateToDB()
        {
            PrepareQueryString = "update Subjects set Description = @Description " +
                               "where Id = @Id";
        }

        public void PrepareSQLStringForInsertToDB()
        {
            PrepareQueryString = "insert into Subjects (Subject) " +
                          "values (@Description) ";
        }

        public string PrepareSQLForDataManagementGrid()
        {
            return "select Id, Description as 'Subject' from Subjects";
        }
    }
}

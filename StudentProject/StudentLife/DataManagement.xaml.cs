using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentLife
{
    public partial class DataManagement : UserControl
    {
        public DbConnection dbc;

        public DataManagement(DbConnection dbcInput)
        {
            InitializeComponent();
            dbc = dbcInput;
            InitializeData();
        }

        private void InitializeData()
        {
            _stackPanels = new List<UIElement>
            {
                Subjects_StackPanel,
                ClassRoomTasks_StackPanel,
                Homeworks_StackPanel,
                ClassTaskTypes_StackPanel
            };
        }

        public List<UIElement> _stackPanels;

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string namePanel = CheckPanelVisible();
            switch (namePanel)
            {
                case "Subjects":
                    if (!String.IsNullOrEmpty(SubjectDesciption_TextBox.Text))
                    {

                        string stringSQL = "insert into Subjects (Description) " +
                                           "values (@Description)";
                        if (AUD(namePanel, stringSQL, 0))
                        {
                            AddButton.IsEnabled = false;
                            UpdateButton.IsEnabled = true;
                            DeleteButton.IsEnabled = true;
                        }
                    }
                    break;
                default:    //Developped only Subjects Management
                    break;
            }


        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string namePanel = CheckPanelVisible();
            switch (namePanel)
            {
                case "Subjects":
                    {
                        string stringSQL = "update Subjects set Description = @Description " +
                               "where Id = @Id";
                        if (AUD(namePanel, stringSQL, 1))
                            ResetAll();
                        else
                            SubjectDesciption_TextBox.Text = "";
                    }
                    break;
                default:    //Developped only Subjects Management
                    break;
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            string namePanel = CheckPanelVisible();
            switch (namePanel)
            {
                case "Subjects":
                    {
                        string stringSQL = "delete from Subjects " +
                               "where Id = @Id";
                        if (AUD(namePanel, stringSQL, 2))
                            ResetAll();
                    }
                    break;
                default:    //Developped only Subjects Management
                    break;
            }

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            string namePanel = CheckPanelVisible();
            switch (namePanel)
            {
                case "Subjects":
                    {
                        SubjectId_TextBlock.Text = "";
                        SubjectDesciption_TextBox.Text = "";
                    }
                    break;
                default:    //Developped only Subjects Management
                    {
                        AddButton.IsEnabled = true;
                        UpdateButton.IsEnabled = false;
                        DeleteButton.IsEnabled = false;
                    }
                    break;
            }


        }

        private string CheckPanelVisible()
        {
            if (Subjects_StackPanel.Visibility == Visibility.Visible)
                return "Subjects";
            else if (ClassRoomTasks_StackPanel.Visibility == Visibility.Visible)
                return "ClassRoomTasks";
            else if (Homeworks_StackPanel.Visibility == Visibility.Visible)
                return "Homeworks";
            else if (ClassTaskTypes_StackPanel.Visibility == Visibility.Visible)
                return "ClassTask Types";
            else
                throw new Exception();
        }



        private bool AUD(string textHeader, string stringSQL, int state)
        {
            string msg = "";
            bool IsSqlOK = false;
            dbc.Comm.CommandText = stringSQL;
            dbc.Comm.CommandType = CommandType.Text;
            dbc.Comm.Parameters.Clear();
            switch (state)
            {
                case 0:
                    msg = "Row Inserted!";
                    dbc.Comm.Parameters.AddWithValue("Description", SubjectDesciption_TextBox.Text);
                    break;
                case 1:
                    msg = "Row Updated!";
                    dbc.Comm.Parameters.AddWithValue("Id", SubjectId_TextBlock.Text);
                    dbc.Comm.Parameters.AddWithValue("Description", SubjectDesciption_TextBox.Text);
                    break;
                case 2:
                    msg = "Row Deleted!";
                    dbc.Comm.Parameters.AddWithValue("Id", SubjectId_TextBlock.Text);
                    break;
            }
            try
            {
                int n = dbc.Comm.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    UpdateDataGrid(CreateSQLString(textHeader));
                    IsSqlOK = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY constraint"))
                    MessageBox.Show("Subject already existent");
                else
                    MessageBox.Show(ex.Message);
            }

            return IsSqlOK;

        }

        public string CreateSQLString(string textHeader)
        {
            string stringSQL = "";
            switch (textHeader)
            {
                case "Subjects":
                    stringSQL = "select Id, Description as 'Subject' from Subjects";
                    break;
                case "ClassRoomTasks":
                    stringSQL = "select ct.Id, convert(nvarchar(MAX), ct.WhenDate, 103) as 'Date', " +
                        "ct.Vote, crt.Description as 'Task Type', s.Description as 'Subject' " +
                        "from ClassRoomTasks as ct " +
                        "left Join ClassroomTaskTypes as crt on crt.Id = ct.TaskId " +
                        "left join Subjects as s on s.Id = ct.SubjectId";
                    break;
                case "HomeWorks":
                    stringSQL = "select h.Id, h.Description as 'Activity', " +
                        "convert(nvarchar(MAX), h.StartDate, 103) as 'Start Date', " +
                        "convert(nvarchar(MAX), h.EndDate, 103) as 'End Date', " +
                        "s.Description as 'Subject' " +
                        "from Homeworks as h " +
                        "left join Subjects as s on s.Id = h.SubjectId";
                    break;
                case "ClassTask Types":
                    stringSQL = "select Id, Description as 'Task Type Description' from ClassRoomTaskTypes";
                    break;
            }

            return stringSQL;
        }

        private void UpdateDataGrid(string stringSQL)
        {
            dbc.Comm.CommandText = stringSQL;
            dbc.Comm.CommandType = CommandType.Text;

            using (SqlDataReader reader = dbc.Comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    Display_DataGrid.ItemsSource = dt.DefaultView;
                }
            }
        }

        private void Display_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string namePanel = CheckPanelVisible();
            DataGrid dg = sender as DataGrid;
            if (dg.SelectedItem is DataRowView drg)
            {
                switch (namePanel)
                {
                    case "Subjects":
                        {
                            SubjectId_TextBlock.Text = drg["Id"].ToString();
                            SubjectDesciption_TextBox.Text = drg["Subject"].ToString();
                        }
                        break;
                    default:        //Developped only Subjects Management
                        break;
                }
                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }


        }
    }

    public class UserControlSubjectData
    {
        public int SubjectId { get; }
        public string SubjectDescription { get; set; }
    }

    public class UserControlClassRoomTaskData
    {
        public int TaskId { get; }
        public List<string> TaskSubject { get; set; }
        public List<string> TaskType { get; set; }
        public DateTime TaskWhenDate { get; set; }
        public int TaskVote { get; set; }
    }

    public class UserControlHomeworkData
    {
        public int HomeworkId { get; }
        public string HomeworkDescription { get; set; }
        public List<string> HomeworkSubject { get; set; }
        public DateTime HomeworkStartDate { get; set; }
        public DateTime HomeworkEndDate { get; set; }

    }

    public class UserControlTaskTypeData
    {
        public int TaskTypeId { get; }
        public string TaskTypeDescription { get; set; }
    }
}

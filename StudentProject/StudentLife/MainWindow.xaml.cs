using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace StudentLife
{
    public partial class MainWindow : Window
    {

        public DbConnection dbc;

        public MainWindow()
        {
            InitializeComponent();

            PrepareStringConnection();

            this.contentMainWindow.Content = new DataDisplay(dbc);
            Display_MenuItem.Visibility = Visibility.Hidden;
        }

        private void PrepareStringConnection()
        {
            string connectionSQlString =
                   @"Data Source=.\sqlexpress;Database=StudentLife;Integrated Security=True";
            dbc = new DbConnection(connectionSQlString);
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            DataManagement dm = BaseOperationsForDataManagement(textHeader);

            SetVisibility(dm, dm.Subjects_StackPanel);

            LoadDataManagementGrid(dbc, dm.Display_DataGrid, CreateSQLString(textHeader));
        }

        private void ClassRoomTasks_Click(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            DataManagement dm = BaseOperationsForDataManagement(textHeader);

            SetVisibility(dm, dm.ClassRoomTasks_StackPanel);

            LoadDataManagementGrid(dbc, dm.Display_DataGrid, CreateSQLString(textHeader));
        }

        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            DataManagement dm = BaseOperationsForDataManagement(textHeader);

            SetVisibility(dm, dm.Homeworks_StackPanel);

            LoadDataManagementGrid(dbc, dm.Display_DataGrid, CreateSQLString(textHeader));
        }

        private void ClassTask_Click(object sender, RoutedEventArgs e)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            DataManagement dm = BaseOperationsForDataManagement(textHeader);

            SetVisibility(dm, dm.Homeworks_StackPanel);

            LoadDataManagementGrid(dbc, dm.Display_DataGrid, CreateSQLString(textHeader));
        }

        private void Display_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplay(dbc);
            Display_MenuItem.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private DataManagement BaseOperationsForDataManagement(string textHeader)
        {
            DataManagement dm = new DataManagement();
            this.contentMainWindow.Content = dm;
            Display_MenuItem.Visibility = Visibility.Visible;
            return dm;
        }

        private void SetVisibility(DataManagement dm, UIElement el)
        {
            foreach (UIElement panel in dm._stackPanels)
                panel.Visibility = panel == el
                    ? Visibility.Visible
                    : Visibility.Hidden;
        }

        private string CreateSQLString(string textHeader)
        {
            string stringSQL = "";
            switch (textHeader)
            {
                case "Subjects":
                    stringSQL = "select Id, Description as 'Subject' from Subjects";
                    break;
                case "ClassRoomTasks":
                    stringSQL = "select ct.Id, convert(nvarchar(MAX), ct.WhenDate, 101) as 'Date', " +
                        "ct.Vote, crt.Description as 'Task Type', s.Description as 'Subject' " +
                        "from ClassRoomTasks as ct " +
                        "left Join ClassroomTaskTypes as crt on crt.Id = ct.TaskId " +
                        "left join Subjects as s on s.Id = ct.SubjectId";
                    break;
                case "HomeWorks":
                    stringSQL = "select h.Id, h.Description as 'Activity', " +
                        "convert(nvarchar(MAX), h.StartDate, 101) as 'Start Date', " +
                        "convert(nvarchar(MAX), h.EndDate, 101) as 'End Date', " +
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

        public void LoadDataManagementGrid(DbConnection dbc, DataGrid dg, string stringSQL)
        {
            dbc.Comm.CommandText = stringSQL;
            dbc.Comm.CommandType = CommandType.Text;

            using (SqlDataReader reader = dbc.Comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dg.ItemsSource = dt.DefaultView;
                }
            }
        }
    }

    public class DbConnection
    {
        public SqlConnection SqlConn { get; }
        public SqlCommand Comm { get; }

        public DbConnection(string connectionSQlString)
        {
            SqlConn = new SqlConnection(connectionSQlString);
            Comm = SqlConn.CreateCommand();
            SqlConn.Open();
        }
    }

    public class OperatorsSQL
    {
        public string MoreLessOperator { get; }
        public string NullOperator { get; }
        public string OrAndOperator { get; }

        public OperatorsSQL(string moreLessOperator, string nullOperator, string orAndOperator)
        {
            MoreLessOperator = moreLessOperator;
            NullOperator = nullOperator;
            OrAndOperator = orAndOperator;
        }
    }
}

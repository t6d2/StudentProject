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
using StudentLife.Classes;

namespace StudentLife
{

    public partial class DataClassRoomTaskType : UserControl
    {
        private ClassRoomTaskType classRoomTaskType;
        private DbConnection dbConnection;

        public DataClassRoomTaskType(DbConnection dbc)
        {
            dbConnection = dbc;
            InitializeComponent();
            classRoomTaskType = new ClassRoomTaskType();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputOK())
            {
                var stringSQL = classRoomTaskType.PrepareSQLStringForInsertToDB();
                ExecuteDBOperations(stringSQL, "Row Inserted!");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputOK())
            {
                var stringSQL = classRoomTaskType.PrepareSQLStringForUpdateToDB();
                ExecuteDBOperations(stringSQL, "Row Updated!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = classRoomTaskType.PrepareSQLStringForDeleteToDB();
            if (!string.IsNullOrEmpty(stringSQL))
            {
                MessageBoxResult result = MessageBox.Show("PAY ATTENTION!! If you delete a Task Type, " +
                    "all linked Classroom Tasks will be deleted. Do you proceed?", "IMPORTANT", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ExecuteDBOperations(stringSQL, "Row Deleted!");
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
            Display_DataGrid.SelectedIndex = -1;
        }

        private void ResetAll()
        {
            Id_TextBlock.Text = "";
            Description_TextBox.Text = "";
            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void Display_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedItem is DataRowView drv)
            {

                this.DataContext = classRoomTaskType = new ClassRoomTaskType
                {
                    Id = drv["Id"].ToString(),
                    Description = drv["Task Type Description"].ToString(),
                };

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
        }

        private void ExecuteDBOperations(string stringSQL, string msg)
        {
            dbConnection.Comm.CommandText = stringSQL;
            dbConnection.Comm.CommandType = CommandType.Text;
            dbConnection.Comm.Parameters.Clear();
            try
            {
                int n = dbConnection.Comm.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    LoadDataManagementGrid(classRoomTaskType.PrepareSQLForDataManagementGrid());
                    ResetAll();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY constraint"))
                    MessageBox.Show("Row already existent");
                else
                    MessageBox.Show(ex.Message);
            }
        }

        private void ClassRoomTasksTypeData_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = classRoomTaskType;
            LoadDataManagementGrid(classRoomTaskType.PrepareSQLForDataManagementGrid());
        }

        private void LoadDataManagementGrid(string stringSQL)
        {
            dbConnection.Comm.CommandText = stringSQL;
            dbConnection.Comm.CommandType = CommandType.Text;

            using (SqlDataReader reader = dbConnection.Comm.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    Display_DataGrid.ItemsSource = dt.DefaultView;

                }
            }
        }

        private bool InputOK()
        {
            if (!String.IsNullOrEmpty(Description_TextBox.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Task type missing");
                return false;
            }
        }
    }
}

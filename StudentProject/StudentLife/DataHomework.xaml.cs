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
    /// <summary>
    /// Logica di interazione per DataHomework.xaml
    /// </summary>
    public partial class DataHomework : UserControl
    {
        private HomeWork homeWork;
        private DbConnection dbConnection;

        public DataHomework(DbConnection dbc)
        {
            InitializeComponent();
            homeWork = new HomeWork();
            dbConnection = dbc;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            var stringSQL = homeWork.PrepareSQLStringForInsertToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 0);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = homeWork.PrepareSQLStringForUpdateToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 1);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = homeWork.PrepareSQLStringForDeleteToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 2);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            Id_TextBlock.Text = "";
            Homework_TextBox.Text = "";
            Subject_ComboBox.Text = "";
            StartDate_DatePicker.Text = "";
            EndDate_DatePicker.Text = "";
            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void Display_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedItem is DataRowView drv)
            {
                Subject_ComboBox.ItemsSource = null;
                var comboItems = LoadComboBox(dg, 4);
                Subject_ComboBox.ItemsSource = comboItems;

                this.DataContext = homeWork = new HomeWork
                {
                    Id = drv["Id"].ToString(),
                    Description = drv["Activity"].ToString(),
                    SubjectDescription = drv["Subject"].ToString(),
                    StartDate = drv["Start Date"].ToString(),
                    EndDate = drv["End Date"].ToString(),
                };

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
        }

        private List<string> LoadComboBox(DataGrid dg, int columnNumber)
        {
            var listItems = new List<string>();
            foreach (DataRowView dataRowview in dg.ItemsSource)
            {
                string columnItem = dataRowview[columnNumber] == null ? "" : dataRowview[columnNumber].ToString();
                listItems.Add(columnItem);
            }
            var comboItems = listItems
                .Select(x => x)
                .Distinct()
                .ToList();
            return comboItems;
        }

        private void ExecuteDBOperations(string stringSQL, int state)
        {
            string msg = "";
            dbConnection.Comm.CommandText = stringSQL;
            dbConnection.Comm.CommandType = CommandType.Text;
            dbConnection.Comm.Parameters.Clear();
            switch (state)
            {
                case 0:
                    msg = "Row Inserted!";
                    break;
                case 1:
                    msg = "Row Updated!";
                    break;
                case 2:
                    msg = "Row Deleted!";
                    break;
            }
            try
            {
                int n = dbConnection.Comm.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show(msg);
                    LoadDataManagementGrid(homeWork.PrepareSQLForDataManagementGrid());
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

        private void HomeworkData_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = homeWork;
            LoadDataManagementGrid(homeWork.PrepareSQLForDataManagementGrid());
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

                    Subject_ComboBox.ItemsSource = null;
                    var comboItems = LoadComboBox(Display_DataGrid, 4);
                    Subject_ComboBox.ItemsSource = comboItems;

                }
            }
        }

        private void Display_DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Activity")
            {
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));

                col.ElementStyle = style;
            }
        }
    }
}

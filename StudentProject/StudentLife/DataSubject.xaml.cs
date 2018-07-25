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
    /// Logica di interazione per DataSubject.xaml
    /// </summary>
    public partial class DataSubject : UserControl
    {
        private Subject subject;
        private DbConnection dbConnection;

        public DataSubject(DbConnection dbc)
        {
            InitializeComponent();
            subject = new Subject();
            dbConnection = dbc;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = subject.PrepareSQLStringForInsertToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 0);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = subject.PrepareSQLStringForUpdateToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 1);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = subject.PrepareSQLStringForDeleteToDB();
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
                this.DataContext = subject = new Subject
                {
                    Id = drv["Id"].ToString(),
                    Description = drv["Subject"].ToString(),
                };

                AddButton.IsEnabled = false;
                UpdateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }

        }

        private List<string> PopulateComboBox(DataGrid dg, int columnNumber)
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

        private void SubjectData_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = subject;
            LoadDataManagementGrid(subject.PrepareSQLForDataManagementGrid());
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
                    LoadDataManagementGrid(subject.PrepareSQLForDataManagementGrid());
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
    }
}

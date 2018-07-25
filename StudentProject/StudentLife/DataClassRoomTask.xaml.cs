﻿using System;
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
    /// Logica di interazione per DataClassRoomTask.xaml
    /// </summary>
    public partial class DataClassRoomTask : UserControl
    {
        private ClassRoomTask classRoomTask;
        private DbConnection dbConnection;

        public DataClassRoomTask(DbConnection dbc)
        {
            InitializeComponent();
            classRoomTask = new ClassRoomTask();
            dbConnection = dbc;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            var stringSQL = classRoomTask.PrepareSQLStringForInsertToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 0);

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = classRoomTask.PrepareSQLStringForUpdateToDB();
            if (!string.IsNullOrEmpty(stringSQL))
                ExecuteDBOperations(stringSQL, 1);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var stringSQL = classRoomTask.PrepareSQLStringForDeleteToDB();
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
            Subject_ComboBox.Text = "";
            Type_ComboBox.Text = "";
            When_DatePicker.Text = "";
            Vote_TextBox.Text = "";
            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void Display_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedItem is DataRowView drv)
            {

                Type_ComboBox.ItemsSource = null;
                var comboItems = LoadComboBox(dg, 3);
                Type_ComboBox.ItemsSource = comboItems;

                Subject_ComboBox.ItemsSource = null;
                comboItems = LoadComboBox(dg, 4);
                Subject_ComboBox.ItemsSource = comboItems;

                this.DataContext = classRoomTask = new ClassRoomTask
                {
                    Id = drv["Id"].ToString(),
                    WhenDate = drv["Date"].ToString(),
                    Vote = drv["Vote"].ToString(),
                    ClassRoomTaskType = drv["Task Type"].ToString(),
                    SubjectDescritpion = drv["Subject"].ToString(),
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
                    LoadDataManagementGrid(classRoomTask.PrepareSQLForDataManagementGrid());
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


        private void ClassRoomTaskData_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = classRoomTask;
            LoadDataManagementGrid(classRoomTask.PrepareSQLForDataManagementGrid());
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

                    Type_ComboBox.ItemsSource = null;
                    var comboItems = LoadComboBox(Display_DataGrid, 3);
                    Type_ComboBox.ItemsSource = comboItems;

                    Subject_ComboBox.ItemsSource = null;
                    comboItems = LoadComboBox(Display_DataGrid, 4);
                    Subject_ComboBox.ItemsSource = comboItems;
                }
            }
        }
    }
}
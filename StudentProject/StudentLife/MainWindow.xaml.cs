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
    public partial class MainWindow : Window
    {
        public static DbConnection dbc;

        public MainWindow()
        {
            InitializeComponent();

            InitializeData();
            this.contentMainWindow.Content = new DataDisplay(dbc);
            Display_MenuItem.Visibility = Visibility.Hidden;
        }

        private void InitializeData()
        {

            dbc = new DbConnection();
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender);
        }
        private void ClassRoomTasks_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender);
        }

        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender);
        }

        private void ClassTask_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender);
        }

        private void MenuItemOperations(object sender)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            BaseOperationsForDataManagement(textHeader);
        }

        private void Display_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplay(dbc);
            Display_MenuItem.Visibility = Visibility.Hidden;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (dbc.SqlConn != null)
                dbc.SqlConn.Close();

            Application.Current.Shutdown();
        }

        private void BaseOperationsForDataManagement(string menuItemName)
        {
            switch (menuItemName)
            {
                case "Subjects":
                    this.contentMainWindow.Content = new DataSubject(dbc);
                    break;
                case "ClassRoomTasks":
                    this.contentMainWindow.Content = new DataClassRoomTask(dbc);
                    break;
                case "HomeWorks":
                    this.contentMainWindow.Content = new DataHomework(dbc);
                    break;
                case "ClassTask Types":
                    this.contentMainWindow.Content = new DataClassRoomTaskType(dbc);
                    break;

            }
            Display_MenuItem.Visibility = Visibility.Visible;

        }
    }
}

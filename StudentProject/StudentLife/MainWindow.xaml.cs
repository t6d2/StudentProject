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
        public static DbConnection dbConnection;

        public MainWindow()
        {
            InitializeComponent();

            InitializeData();
            this.contentMainWindow.Content = new DataDisplay(dbConnection);
            AllActivities_MenuItem.Visibility = Visibility.Collapsed;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void InitializeData()
        {
            dbConnection = new DbConnection();
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbConnection => new DataSubject(dbConnection));
        }
        private void ClassRoomTasks_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbConnection => new DataClassRoomTask(dbConnection));
        }

        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbConnection => new DataHomework(dbConnection));
        }

        private void ClassTask_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbConnection => new DataClassRoomTaskType(dbConnection));
        }

        private void MenuItemOperations(object sender, Func<DbConnection, UserControl> ucCreator)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            contentMainWindow.Content = ucCreator(dbConnection);
            AllActivities_MenuItem.Visibility = Visibility.Visible;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplay(dbConnection);
            AllActivities_MenuItem.Visibility = Visibility.Collapsed;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void MaterialActivities_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplayByMaterial(dbConnection);
            AllActivities_MenuItem.Visibility = Visibility.Visible;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Collapsed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dbConnection.SqlConn != null)
                dbConnection.SqlConn.Close();
        }
    }
}

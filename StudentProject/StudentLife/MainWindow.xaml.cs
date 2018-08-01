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
            AllActivities_MenuItem.Visibility = Visibility.Collapsed;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void InitializeData()
        {
            dbc = new DbConnection();
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbc => new DataSubject(dbc));
        }
        private void ClassRoomTasks_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbc => new DataClassRoomTask(dbc));
        }

        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbc => new DataHomework(dbc));
        }

        private void ClassTask_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dbc => new DataClassRoomTaskType(dbc));
        }

        private void MenuItemOperations(object sender, Func<DbConnection, UserControl> ucCreator)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            contentMainWindow.Content = ucCreator(dbc);
            AllActivities_MenuItem.Visibility = Visibility.Visible;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplay(dbc);
            AllActivities_MenuItem.Visibility = Visibility.Collapsed;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void MaterialActivities_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplayByMaterial(dbc);
            AllActivities_MenuItem.Visibility = Visibility.Visible;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Collapsed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (dbc.SqlConn != null)
                dbc.SqlConn.Close();
        }
    }
}

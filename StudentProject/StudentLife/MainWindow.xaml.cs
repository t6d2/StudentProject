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
        public MainWindow()
        {
            InitializeComponent();

            this.contentMainWindow.Content = new DataDisplay();
            AllActivities_MenuItem.Visibility = Visibility.Collapsed;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            contentMainWindow.Content = new DataSubject();
            MenuItemOperations();
        }
        private void ClassRoomTasks_Click(object sender, RoutedEventArgs e)
        {
            contentMainWindow.Content = new DataClassRoomTask();
            MenuItemOperations();
        }

        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            contentMainWindow.Content = new DataHomework();
            MenuItemOperations();
        }

        private void ClassTask_Click(object sender, RoutedEventArgs e)
        {
            contentMainWindow.Content = new DataClassRoomTaskType();
            MenuItemOperations();
        }

        private void MenuItemOperations()
        {
            AllActivities_MenuItem.Visibility = Visibility.Visible;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplay();
            AllActivities_MenuItem.Visibility = Visibility.Collapsed;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Visible;
        }

        private void MaterialActivities_Click(object sender, RoutedEventArgs e)
        {
            this.contentMainWindow.Content = new DataDisplayByMaterial();
            AllActivities_MenuItem.Visibility = Visibility.Visible;
            ActivitiesBySubject_MenuItem.Visibility = Visibility.Collapsed;
        }
    }
}

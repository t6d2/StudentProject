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
        public static DataManagement dm;
        public GridManagementCreator gmc;

        public MainWindow()
        {

            InitializeComponent();

            dm = new DataManagement();

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
            MenuItemOperations(sender, dm.Subjects_StackPanel);
        }
        private void ClassRoomTasks_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dm.ClassRoomTasks_StackPanel);
        }

        private void Homework_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dm.Homeworks_StackPanel);
        }

        private void ClassTask_Click(object sender, RoutedEventArgs e)
        {
            MenuItemOperations(sender, dm.ClassTaskTypes_StackPanel);
        }

        private void MenuItemOperations(object sender, StackPanel stackPanel)
        {
            var clickedMenuItem = sender as MenuItem;
            var textHeader = clickedMenuItem.Header.ToString();
            BaseOperationsForDataManagement(textHeader);

            SetVisibility(dm, stackPanel);
            gmc = new GridManagementCreator(dbc, dm, textHeader);
            gmc.LoadDataManagementGrid(gmc.CreateSQLString());
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

        private void BaseOperationsForDataManagement(string textHeader)
        {

            this.contentMainWindow.Content = dm;
            Display_MenuItem.Visibility = Visibility.Visible;

        }

        private void SetVisibility(DataManagement dm, UIElement el)
        {
            foreach (UIElement panel in dm._stackPanels)
                panel.Visibility = panel == el
                    ? Visibility.Visible
                    : Visibility.Hidden;
        }
    }
}

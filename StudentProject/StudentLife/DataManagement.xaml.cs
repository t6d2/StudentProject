using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using StudentLife.Classes;

namespace StudentLife
{
    public partial class DataManagement : UserControl
    {
        public DataManagement()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            _stackPanels = new List<UIElement>
            {
                Subjects_StackPanel,
                ClassRoomTasks_StackPanel,
                Homeworks_StackPanel,
                ClassTaskTypes_StackPanel
            };
        }

        public List<UIElement> _stackPanels;

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            //SubjectId_TextBlock.Text = "";
            //SubjectDesciption_TextBox.Text = "";
            AddButton.IsEnabled = true;
            UpdateButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;

        }

        private void Display_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;

            if (dg.SelectedItem is DataRowView drv)
            {
                UIElement stackPanelVisible = CheckVisibilityStackPanel();
                //var windowOperations = new WindowOperations((StackPanel)stackPanelVisible);
                //windowOperations.CallPopulationUserCtrlFieldsfromDataGrid(drv);

                string nameStackPanel = ((FrameworkElement)stackPanelVisible).Name;
                switch (nameStackPanel)
                {

                    case ("Subjects_StackPanel"):
                        {

                            //this.DataContext = "Subject";
                            var subject = new Subject
                            {
                                //Id = drg["Id"].ToString(),
                                //Description = drg["Subject"].ToString()

                                // this method works but in this way binding is unuseful
                                Id = SubjectId_TextBlock.Text = drv["Id"].ToString(),
                                Description = SubjectDesciption_TextBox.Text = drv["Subject"].ToString()
                            };
                        }
                        break;
                    case ("ClassRoomTasks_StackPanel"):
                        {

                            CRT_Type_ComboBox.Items.Clear();

                            var classRoomTask = new ClassRoomTask
                            {
                                Id = CRT_Id_Block.Text = drv["Id"].ToString(),
                                WhenDate = CRT_When_DatePicker.Text = drv["Date"].ToString(),
                                Vote = CRT_Vote_TextBox.Text = drv["Vote"].ToString(),
                                ClassRoomTaskType = CRT_Type_ComboBox.Text = drv["Task Type"].ToString(),
                                SubjectDescritpion = CRT_Subject_ComboBox.Text = drv["Subject"].ToString()
                            };
                        }
                        break;
                    case ("Homeworks_StackPanel"):
                        {
                            var homeWork = new HomeWork()
                            {
                                Id = HW_Id_Block.Text = drv["Id"].ToString(),
                                Description = HW_Homework_TextBox.Text = drv["Activity"].ToString(),
                                SubjectDescription = HW_Subject_ComboBox.Text = drv["Subject"].ToString(),
                                StartDate = HW_StartDate_DatePicker.Text = drv["Start Date"].ToString(),
                                EndDate = HW_EndDate_DatePicker.Text = drv["End Date"].ToString()
                            };
                        }
                        break;
                    case ("ClassTaskTypes_StackPanel"):
                        {
                            var classRoomTaskType = new ClassRoomTaskType
                            {
                                Id = CTT_Id_Block.Text = drv["Id"].ToString(),
                                Description = CTT_Description_Text.Text = drv["Task Type Description"].ToString()
                            };
                        }
                        break;
                    default:
                        break;
                }
            }

            AddButton.IsEnabled = false;
            UpdateButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private UIElement CheckVisibilityStackPanel()
        {
            foreach (UIElement panel in this._stackPanels)
                if (panel.Visibility == Visibility.Visible)
                    return panel;
            throw new Exception("No Stack panels visible");
        }
    }
}

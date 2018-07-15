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
            // to do
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // to do
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // to do
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // to do
        }
    }
}

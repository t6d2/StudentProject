using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DataDisplay : UserControl
    {
        private GridDisplayCreator _GridDisplayCreator;

        public DataDisplay()
        {
            InitializeComponent();
            _GridDisplayCreator = new GridDisplayCreator();
            _GridDisplayCreator.LoadDataGrid(ToDoTasks_DataGrid, new OperatorsSQL(">=", "is", "or"));
            _GridDisplayCreator.LoadDataGrid(DoneTasks_DataGrid, new OperatorsSQL("<", "is not", "and"));
        }

        private void ToDoTasks_DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseScrollEnable(sender, e);
        }

        private void DoneTasks_DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseScrollEnable(sender, e);
        }

        private static void MouseScrollEnable(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(
                    e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                };
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}

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

    public partial class DataDisplayByMaterial : UserControl
    {
        private DbConnection _DbConnection { get; set; }
        private SqlConnection _SqlConn { get; set; }
        private SqlCommand _Comm { get; set; }

        public DataDisplayByMaterial()
        {
            InitializeComponent();
        }

        private void Homeworks_DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseScrollEnable(sender, e);
        }

        private void ClassTasks_DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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

        private void Subject_ComboBox_Initialized(object sender, EventArgs e)
        {
            var dbConnection = new DbConnection();
            var homeWorkList = new List<HomeWork>();
            using (_SqlConn = new SqlConnection(dbConnection.PrepareStringConnection()))
            {
                _Comm = _SqlConn.CreateCommand();
                _SqlConn.Open();
                _Comm.CommandText = "select Description from Subjects order by Description";
                _Comm.CommandType = CommandType.Text;
                var descriptionList = new List<string>();
                using (SqlDataReader reader = _Comm.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            descriptionList.Add(reader.GetString(0));
                        }
                    }
                }
                Subject_ComboBox.ItemsSource = null;
                Subject_ComboBox.ItemsSource = descriptionList;
            }
        }

        private void Subject_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDataGrid(Subject_ComboBox.SelectedItem.ToString());
        }

        public void LoadDataGrid(string subject)
        {
            var dbConnection = new DbConnection();
            Homeworks_DataGrid.ItemsSource = null;
            using (_SqlConn = new SqlConnection(dbConnection.PrepareStringConnection()))
            {
                _Comm = _SqlConn.CreateCommand();
                _SqlConn.Open();

                _Comm.CommandText = "select h.Description, CONVERT(date, h.StartDate), CONVERT(date, h.EndDate), s.Description" +
                                    " from Homeworks as h " +
                                    "join Subjects as s on s.Id = h.SubjectId " +
                                    $"where s.Description = '{subject}' " +
                                     "order by h.StartDate ";
                _Comm.CommandType = CommandType.Text;
                var homeWorkList = new List<HomeWork>();
                using (SqlDataReader reader = _Comm.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HomeWork hw = new HomeWork
                            {
                                Description = reader.GetString(0),
                                Start_Date = reader.GetDateTime(1),
                                End_Date = reader.IsDBNull(2)
                                ? (DateTime?)null
                                : reader.GetDateTime(2),
                                SubjectDescription = reader.GetString(3)
                            };
                            hw.StartDate = hw.Start_Date.ToString("dd/MM/yyyy");
                            hw.EndDate = hw.End_Date == null ? "" : hw.End_Date.Value.ToString("dd/MM/yyyy");
                            homeWorkList.Add(hw);
                        }
                        Homeworks_DataGrid.ItemsSource = homeWorkList;
                    }
                }

                ClassTasks_DataGrid.ItemsSource = null;
                _Comm.CommandText = "select ctt.Description, CONVERT(date, ct.WhenDate), ct.Vote " +
                                        "from ClassroomTasks as ct " +
                                        "join Subjects as s on s.Id = SubjectId " +
                                        "join ClassroomTaskTypes as ctt on ctt.Id = TaskId " +
                                        $"where s.Description = '{subject}' " +
                                        "order by ct.WhenDate ";
                _Comm.CommandType = CommandType.Text;
                var classRoomTask = new List<ClassRoomTask>();
                using (SqlDataReader reader = _Comm.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ClassRoomTask crt = new ClassRoomTask
                            {
                                ClassRoomTaskType = reader.GetString(0),
                                When_Date = reader.GetDateTime(1),
                                Vote = reader.IsDBNull(2)
                                ? ""
                                : reader.GetInt32(2).ToString(),
                            };
                            crt.WhenDate = crt.When_Date.ToString("dd/MM/yyyy");
                            classRoomTask.Add(crt);
                        }
                        ClassTasks_DataGrid.ItemsSource = classRoomTask;
                    }
                }
            }
        }
    }
}

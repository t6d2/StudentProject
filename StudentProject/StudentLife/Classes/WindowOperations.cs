using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StudentLife.Classes
{
    public class WindowOperations
    {
        private StackPanel _StackPanel { get; }
        private string _StackPanelName { get; set; }
        private IGenericTable _GenericTable { get; }

        public WindowOperations(StackPanel stackPanel)
        {
            _StackPanel = stackPanel;
            _StackPanelName = stackPanel.Name.ToString();
            IGenericTable _GenericTable = DefineTypeOfClass();


        }
        public StackPanel StackPanel { get; set; }
        public string StackPanelName { get; set; }
        public IGenericTable GenericTable { get; set; }

        public IGenericTable DefineTypeOfClass()
        {


            switch (_StackPanelName)
            {
                case "Subjects_StackPanel":
                    return new Subject();
                case "ClassRoomTasks_StackPanel":
                    return new ClassRoomTask();
                case "Homeworks_StackPanel":
                    return new HomeWork();
                case "ClassTaskTypes_StackPanel":
                    return new ClassRoomTaskType();
                default:
                    throw new Exception("StackPanel not existing");
            }
        }

        public void PrepareQueryReadDB() { }

        public void PrepareQueryWriteDB(string typeOfOperation)
        {
            var queryString = "";
            switch (typeOfOperation)
            {


                case "Add":
                    _GenericTable.PrepareSQLStringForInsertToDB();
                    break;

                case "Udpate":
                    _GenericTable.PrepareSQLStringForUpdateToDB();
                    break;

                case "Delete":
                    queryString = $"delete from {_StackPanelName} " +
                               "where Id = @Id";
                    break;
            }
        }

        public void CallPopulationUserCtrlFieldsfromDataGrid(DataRowView dataRowView)
        {

            _GenericTable.PopulateUserCtrlFieldsfromDataGrid(_StackPanel, dataRowView);

        }


    }
}

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
    public interface IGenericTable
    {
        string PrepareQueryString { get; set; }
        void PopulateUserCtrlFieldsfromDataGrid(StackPanel stackPanel, DataRowView drv);
        string PrepareSQLForDataManagementGrid();
        void PrepareSQLStringForReadingDB();
        void PrepareSQLStringForInsertToDB();
        void PrepareSQLStringForUpdateToDB();

    }
}

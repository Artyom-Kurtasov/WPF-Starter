using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using System.Windows.Controls;

namespace WPF_Starter.ViewModels
{
    public class DataGridManager
    {
        public void SetDataGrid(DataGrid _dataGrid, AppDbContext dataBase)
        {

            var lines = dataBase.Person.ToList();
            _dataGrid.ItemsSource = lines;
        }
    }
}

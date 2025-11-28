using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using System.Windows.Controls;
using WPF_Starter.ViewModels.SearchServices;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels
{
    public class DataGridManager
    {
        public void SetDataGrid(DataGrid _dataGrid, AppDbContext dataBase, PagingSettings pagingSettings, Search search)
        {
            var query = search.SearchPeople(dataBase);

            var page = query
                .Skip(pagingSettings.Page * pagingSettings.PageSize)
                .Take(pagingSettings.PageSize)
                .ToList();

            _dataGrid.ItemsSource = page;
        }
    }

}

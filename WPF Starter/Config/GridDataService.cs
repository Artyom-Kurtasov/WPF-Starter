using System.Collections.ObjectModel;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.SearchServices;

namespace WPF_Starter.Config
{
    public class GridDataService
    {
        private readonly AppDbContext _appDbContext;
        private readonly PagingSettings _pagingSettings;
        private readonly Search _search;
        private readonly DataGridManager _dataGridManager;

        public GridDataService(AppDbContext appDbContext, PagingSettings pagingSettings, Search search, DataGridManager dataGridManager)
        {
            _appDbContext = appDbContext;
            _pagingSettings = pagingSettings;
            _search = search;
            _dataGridManager = dataGridManager;
        }

        public ObservableCollection<People> GetPage() => _dataGridManager.GetPage(_appDbContext, _pagingSettings, _search);
    }
}

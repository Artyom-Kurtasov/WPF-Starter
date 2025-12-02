using System.IO;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.Notifiers;

namespace WPF_Starter.Config
{
    public class StartupDataLoader
    {
        private PagingSettings _pagingSettings;
        private readonly DataBaseWriter _dataBaseWriter;
        private readonly GridDataService _gridDataService;
        private readonly LoadingState _loadingState;

        public StartupDataLoader(PagingSettings pagingSettings, GridDataService gridDataService,
            DataBaseWriter dataBaseWriter, LoadingState loadingState)
        {
            _pagingSettings = pagingSettings;
            _gridDataService = gridDataService;
            _dataBaseWriter = dataBaseWriter;
            _loadingState = loadingState;
        }

        public async Task InitializeAsync(string filePath, AppDbContext appDbContext)
        {
            _loadingState.IsLoading = true;
             await _dataBaseWriter.SaveAsync(appDbContext);
            _pagingSettings.GridPeoples = _gridDataService.GetPage();
            _loadingState.IsLoading = false;
        }
    }
}

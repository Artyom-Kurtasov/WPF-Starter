using System.IO;
using WPF_Starter.Models;
using WPF_Starter.Services;

namespace WPF_Starter.Config
{
    public class StartupDataLoader
    {
        public event Action? LoadCompleted;
        private PagingSettings _pagingSettings;
        private readonly DataLoaderService _dataLoaderService;
        private readonly GridDataService _gridDataService;
        private readonly ErrorNotifier _errorNotifier;

        public StartupDataLoader(PagingSettings pagingSettings, DataLoaderService dataLoaderService, GridDataService gridDataService,
            ErrorNotifier errorNotifier)
        {
            _pagingSettings = pagingSettings;
            _dataLoaderService = dataLoaderService;
            _gridDataService = gridDataService;
            _errorNotifier = errorNotifier;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _dataLoaderService.LoadAsync();
                _pagingSettings.GridPeoples = _gridDataService.GetPage();
                LoadCompleted?.Invoke();
            }
            catch (DirectoryNotFoundException ex)
            {
                _errorNotifier.Notify($"{ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                _errorNotifier.Notify($"{ex.Message}");
            }
            catch (Exception ex)
            {
                _errorNotifier.Notify($"{ex.Message}");
            }
        }
    }
}

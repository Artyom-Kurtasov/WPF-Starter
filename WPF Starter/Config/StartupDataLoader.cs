using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.Exceptions;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Config
{
    public class StartupDataLoader : IStartupDataLoader
    {
        private PagingSettings _pagingSettings;
        private readonly DataBaseWriter _dataBaseWriter;
        private readonly GridDataService _gridDataService;

        public StartupDataLoader(PagingSettings pagingSettings, GridDataService gridDataService,
            DataBaseWriter dataBaseWriter)
        {
            _pagingSettings = pagingSettings;
            _gridDataService = gridDataService;
            _dataBaseWriter = dataBaseWriter;
        }
        /// <summary>
        /// Initialize the process of importing a csv file
        /// saves data to the database, refreshes the DataGrid
        /// and manages loading state
        /// </summary>
        public async Task InitializeAsync(string filePath, AppDbContext appDbContext, Action<long> progressAction)
        {
            try
            {
                _pagingSettings.Page = 0;
                await _dataBaseWriter.SaveAsync(appDbContext, progressAction);
                _pagingSettings.GridPeoples = _gridDataService.GetPage();
            }
            catch (Microsoft.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number == 53) throw new SqlServerConnectionException("Failed to connect to SQL Server: the server was not found or is not accessible." +
                       "Verify the server name, instance name, network settings, and port availability.", sqlEx.Number);
                else if (sqlEx.Number == 4060) throw new DataBaseNotFoundException("Failed to access the specified database." +
                    "Check connection string, verify that the database exists, confirm user permissions", sqlEx.Number);
                else if (sqlEx.Number == 4701) throw new TableNotFoundException("Cannot find the specified table." +
                    "Verify that it exists in the database and that you have permissions", sqlEx.Number);
            }
        }
    }
}

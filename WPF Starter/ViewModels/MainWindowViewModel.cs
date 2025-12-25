using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.IO;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.Exceptions;
using WPF_Starter.Services.Logging;
using WPF_Starter.Services.MessageServices.Interfaces;
using WPF_Starter.Services.Notifiers;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseNotifier _databaseNotifier;
        private readonly ErrorNotifier _errorNotifier;

        private readonly IMessageBoxService _messageBoxService;
        private readonly IStartupDataLoader _dataLoader;
        private readonly ExportSettings _exportSettings;
        private readonly AppDbContext _appDbContext;
        private readonly FileLogger _fileLogger;

        public event EventHandler? UnexpectedError;
        public event EventHandler? TableNotFound;
        public event EventHandler? DatabaseNotFound;
        public event EventHandler? SqlConnecctionFailed;
        public event EventHandler? DatabaseLoaded;
        public event EventHandler? FileNotFound;
        public event EventHandler? InvalidConnectionString;

        public NavigationCommands NavigationCommands { get; }
        public PagingSettings PagingSettings { get; }
        public ImportCommands ImportCommands { get; }

        public MainWindowViewModel(IStartupDataLoader dataLoader, ExportSettings exportSettings, AppDbContext appDbContext,
            FileLogger fileLogger, NavigationCommands navigationCommands, PagingSettings pagingSettings, ImportCommands importCommands,
            ImportNotifier importNotifier, ErrorNotifier errorNotifier, DatabaseNotifier databaseNotifier, IMessageBoxService messageBoxService)
        {
            _dataLoader = dataLoader;
            _exportSettings = exportSettings;
            _appDbContext = appDbContext;
            _fileLogger = fileLogger;
            _errorNotifier = errorNotifier;

            NavigationCommands = navigationCommands;
            PagingSettings = pagingSettings;
            ImportCommands = importCommands;
            _databaseNotifier = databaseNotifier;
            _messageBoxService = messageBoxService;
        }

        public async Task LoadStartupDataAsync()
        {
            await _messageBoxService.ShowProgressAsync("Importing data from file to database", "Reading file and loading data into database, please wait...", async controller =>
            {
            try
            {
                    Subscribe();
                    await _dataLoader.InitializeAsync(_exportSettings.CsvFilePath, _appDbContext, count => controller.SetMessage($"Processed {count:N0} rows"));
                    DatabaseLoaded?.Invoke(this, EventArgs.Empty);
            }
            catch (FileNotFoundException ex)
            {
                _fileLogger.LogError($"{ex}\n");
                FileNotFound?.Invoke(this, EventArgs.Empty);
            }
            catch (SqlServerConnectionException ex)
            {
                _fileLogger.LogError($"{ex}\n");
                SqlConnecctionFailed?.Invoke(this, EventArgs.Empty);
            }
            catch (DataBaseNotFoundException ex)
            {
                _fileLogger.LogError($"{ex}\n");
                DatabaseNotFound?.Invoke(this, EventArgs.Empty);
            }
            catch (TableNotFoundException ex)
            {
                _fileLogger.LogError($"{ex}\n");
                TableNotFound?.Invoke(this, EventArgs.Empty);
            }
            catch (InvalidOperationException ex)
            {
                _fileLogger.LogError($"{ex}\n");
                InvalidConnectionString?.Invoke(this, EventArgs.Empty);
            }
            catch (ArgumentException ex)
            {
                _fileLogger.LogError($"{ex}\n");
                InvalidConnectionString?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                _fileLogger.LogError($"{ex}\n");
                UnexpectedError?.Invoke(this, EventArgs.Empty);
            }
            }, false);
        }

        private void Subscribe()
        {
            InvalidConnectionString += _errorNotifier.OnInvalidConnectionStringAsync;
            UnexpectedError += _errorNotifier.OnUnexpectedErrorOccurred;
            TableNotFound += _databaseNotifier.OnTableNotFoundAsync;
            DatabaseNotFound += _databaseNotifier.OnDatabaseNotFoundAsync;
            SqlConnecctionFailed += _databaseNotifier.OnSqlServerConnectionFailedAsync;
            DatabaseLoaded += _databaseNotifier.OnDatabaseLoadedAsync;
            FileNotFound += _errorNotifier.OnFileNotFoundAsync;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
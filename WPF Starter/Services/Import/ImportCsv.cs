using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.Import.Interfaces;
using WPF_Starter.Services.Logging;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Import
{
    public class ImportCsv : IImportCsv
    {
        public event EventHandler? InvalidPath;
        public event EventHandler? ImportCsvFailed;
        public event EventHandler? ImportCompleted;
        public event EventHandler? InvalidConnectionString;

        private readonly FileLogger _fileLogger;
        private readonly AppDbContext _appDbContext;
        private readonly ExportSettings _exportSettings;
        private readonly IFileDialogService _fileDialogService;
        private readonly IStartupDataLoader _startupDataLoader;
        private readonly IMessageBoxService _messageBoxService;

        public ImportCsv(AppDbContext appDbContext, ExportSettings exportSettings, IFileDialogService fileDialogService, IStartupDataLoader startupDataLoader,
            IMessageBoxService messageBoxService, FileLogger fileLogger)
        {
            _appDbContext = appDbContext;
            _exportSettings = exportSettings;
            _fileDialogService = fileDialogService;
            _startupDataLoader = startupDataLoader;
            _messageBoxService = messageBoxService;
            _fileLogger = fileLogger;
        }

        /// <summary>
        /// Opens a file dialog to select a CSV file
        /// validates the path, initializes data loading into the database
        /// </summary>
        public async Task Import()
        {
            await _messageBoxService.ShowProgressAsync("Importing data from file to database", "Reading file and loading data into database, please wait...", async controller =>
            {
                try
                {
                    _exportSettings.CsvFilePath = _fileDialogService.ChooseFile("CSV files (*.csv)|*.csv|All files (*.*)|*.*", "Select a CSV file");
                    if (string.IsNullOrEmpty(_exportSettings.CsvFilePath))
                    {
                        InvalidPath?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                    await _startupDataLoader.InitializeAsync(_exportSettings.CsvFilePath, _appDbContext, count => controller.SetMessage($"Processed {count:N0} rows"));
                    ImportCompleted?.Invoke(this, EventArgs.Empty);
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
                catch (DbUpdateException ex)
                {
                    _fileLogger.LogError($"{ex}\n");
                    ImportCsvFailed?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    _fileLogger.LogError($"{ex}\n");
                    ImportCsvFailed?.Invoke(this, EventArgs.Empty);
                }
            }, false);
        }
    }
}

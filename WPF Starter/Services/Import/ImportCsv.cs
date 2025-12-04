using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.Import.Interfaces;

namespace WPF_Starter.Services.Import
{
    public class ImportCsv : IImportCsv
    {
        public event EventHandler? InvalidPath;
        public event EventHandler? ImportCsvFailed;
        public event EventHandler? ImportCompleted;

        private readonly AppDbContext _appDbContext;
        private readonly ExportSettings _exportSettings;
        private readonly IFileDialogService _fileDialogService;
        private readonly IStartupDataLoader _startupDataLoader;

        public ImportCsv(AppDbContext appDbContext, ExportSettings exportSettings, IFileDialogService fileDialogService, IStartupDataLoader startupDataLoader)
        {
            _appDbContext = appDbContext;
            _exportSettings = exportSettings;
            _fileDialogService = fileDialogService;
            _startupDataLoader = startupDataLoader;
        }

        /// <summary>
        /// Opens a file dialog to select a CSV file
        /// validates the path, initializes data loading into the database
        /// </summary>
        public async Task Import()
        {
            try
            {
                _exportSettings.CsvFilePath = _fileDialogService.ChooseFile("CSV files (*.csv)|*.csv|All files (*.*)|*.*", "Select a CSV file");
                if (string.IsNullOrEmpty(_exportSettings.CsvFilePath))
                {
                    InvalidPath?.Invoke(this, EventArgs.Empty);
                    return;
                }
                await _startupDataLoader.InitializeAsync(_exportSettings.CsvFilePath, _appDbContext);
                ImportCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception) 
            {
                ImportCsvFailed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.Notifiers;

namespace WPF_Starter.Services.Import
{
    public class ImportCsv
    {
        public event Action? InvalidPath;
        public event Action? ImportCsvFailed;
        public event Action? ImportCompleted;

        private readonly AppDbContext _appDbContext;
        private readonly ExportSettings _exportSettings;
        private readonly IFileDialogService _fileDialogService;
        private readonly StartupDataLoader _startupDataLoader;

        public ImportCsv(AppDbContext appDbContext, ExportSettings exportSettings, IFileDialogService fileDialogService, StartupDataLoader startupDataLoader)
        {
            _appDbContext = appDbContext;
            _exportSettings = exportSettings;
            _fileDialogService = fileDialogService;
            _startupDataLoader = startupDataLoader;
        }
        public async Task Import()
        {
            try
            {
                _exportSettings.CsvFilePath = _fileDialogService.ChooseFile("CSV files (*.csv)|*.csv|All files (*.*)|*.*", "Select a CSV file");
                if (string.IsNullOrEmpty(_exportSettings.CsvFilePath))
                {
                    InvalidPath?.Invoke();
                    return;
                }
                await _startupDataLoader.InitializeAsync(_exportSettings.CsvFilePath, _appDbContext);
                ImportCompleted?.Invoke();
            }
            catch (Exception) 
            {
                ImportCsvFailed?.Invoke();
            }
        }
    }
}

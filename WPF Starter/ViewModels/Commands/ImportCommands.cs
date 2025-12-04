using System.Windows.Input;
using WPF_Starter.Models;
using WPF_Starter.Services.Import.Interfaces;

namespace WPF_Starter.ViewModels.Commands
{
    public class ImportCommands
    {
        private readonly IImportCsv _importCsv;
        private readonly LoadingState _loadingState;
        private readonly ExportSettings _exportSettings;
        public ICommand ImportCommand { get; }

        public ImportCommands(IImportCsv importCsv, LoadingState loadingState, ExportSettings exportSettings)
        {
            _importCsv = importCsv;
            _loadingState = loadingState;
            _exportSettings = exportSettings;

            ImportCommand = new AsyncRelayCommand(ImportFile, CanImportFile);

            _loadingState.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(LoadingState.IsLoading))
                {
                    (ImportCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                }
            };
            _exportSettings.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ExportSettings.IsExporting))
                {
                    (ImportCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                }
            };
        }

        public async Task ImportFile() => await _importCsv.Import();
        public bool CanImportFile() => !_exportSettings.IsExporting && !_loadingState.IsLoading;
    }
}

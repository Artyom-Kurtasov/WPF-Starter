using System.Windows.Input;
using WPF_Starter.Models;
using WPF_Starter.Services.Import.Interfaces;

namespace WPF_Starter.ViewModels.Commands
{
    public class ImportCommands
    {
        private readonly ExportSettings _exportSettings;
        private readonly IImportCsv _importCsv;
        public ICommand ImportCommand { get; }

        public ImportCommands(IImportCsv importCsv, ExportSettings exportSettings)
        {
            _importCsv = importCsv;

            ImportCommand = new AsyncRelayCommand(ImportFileAsync, CanImportFile);
            _exportSettings = exportSettings;
        }

        public async Task ImportFileAsync() => await _importCsv.Import();
        public bool CanImportFile() => !_exportSettings.IsExporting;
    }
}

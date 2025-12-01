using System.Windows.Input;
using WPF_Starter.Models;
using WPF_Starter.Services.Export;

namespace WPF_Starter.ViewModels.Commands
{
    public class ExportCommands
    {
        private readonly RefreshStates _refreshStates;
        private readonly ExportSettings _exportSettings;
        private readonly ExportToXml _exportToXml;
        private readonly ExportToExcel _exportToExcel;
        private readonly LoadingState _loadingState;
        public ICommand ExportToXmlFile { get; }
        public ICommand ExportToExcelFile { get; }

        public ExportCommands(ExportToXml exportToXml, ExportToExcel exportToExcel, ExportSettings exportSettings, LoadingState loadingState,
            RefreshStates refreshStates)
        {
            ExportToExcelFile = new AsyncRelayCommand(ExportExcelExecute, CanExportExcelExecute);
            ExportToXmlFile = new AsyncRelayCommand(ExportXmlExecute, CanExportXmlExecute);

            _exportToXml = exportToXml;
            _exportToExcel = exportToExcel;
            _exportSettings = exportSettings;
            _loadingState = loadingState;
            _refreshStates = refreshStates;

            _loadingState.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(LoadingState.IsLoading))
                {
                    (ExportToExcelFile as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                    (ExportToXmlFile as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                }
            };
            _exportSettings.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == nameof(ExportSettings.IsExporting))
                {
                    (ExportToExcelFile as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                    (ExportToXmlFile as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                }
            };
        }
        private async Task ExportExcelExecute() => await _exportToExcel.Export();

        private async Task ExportXmlExecute() => await _exportToXml.Export();

        private bool CanExportExcelExecute() => !_exportSettings.IsExporting;
        private bool CanExportXmlExecute() => !_exportSettings.IsExporting;
    }
}

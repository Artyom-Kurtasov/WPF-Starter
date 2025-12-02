using System.Windows;
using WPF_Starter.Services.Import;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
{
    public class ImportNotifier
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly ImportCsv _importCsv;

        public ImportNotifier(IMessageBoxService messageBoxService, ImportCsv importCsv)
        {
            _importCsv = importCsv;
            _messageBoxService = messageBoxService;

            _importCsv.ImportCompleted += OnImportCompleted;
        }

        private void OnImportCompleted()
        {
            _messageBoxService.ShowMessage("Import has been completed.",
               "Success",
               MessageBoxImage.Information,
               MessageBoxButton.OK);
        }
    }
}

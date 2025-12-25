using MahApps.Metro.Controls.Dialogs;
using WPF_Starter.Services.Import.Interfaces;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
{
    public class ImportNotifier
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IImportCsv _importCsv;

        public ImportNotifier(IMessageBoxService messageBoxService, IImportCsv importCsv)
        {
            _importCsv = importCsv;
            _messageBoxService = messageBoxService;

            _importCsv.ImportCompleted += OnImportCompleted;
        }

        private void OnImportCompleted(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessageAsync("Success", 
               "Import has been completed.",
               MessageDialogStyle.Affirmative);
        }
    }
}

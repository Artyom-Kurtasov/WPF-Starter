using System.Windows;
using WPF_Starter.Config;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
{
    public class DataBaseNotifier
    {
        private readonly IMessageBoxService _messageBoxService;

        public DataBaseNotifier(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;

        }

        public void OnLoadCompleted()
        {
            _messageBoxService.ShowMessage("Database has been successfully loaded. You can now use the application.",
                "Success",
                 MessageBoxImage.Information,
                 MessageBoxButton.OK);
        }
    }
}

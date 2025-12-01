using WPF_Starter.Services.MessageServices.Interfaces;
using System.Windows;
using WPF_Starter.Services.Export;

namespace WPF_Starter.Services
{
    public class SubscriptionManager
    {
        private ErrorNotifier _errorNotifyer;
        private readonly IMessageBoxService _messageBoxService;
        public SubscriptionManager(ErrorNotifier errorNotifyer, IMessageBoxService messageBoxService)
        {
            _errorNotifyer = errorNotifyer;
            _messageBoxService = messageBoxService;

            _errorNotifyer.OnError += message =>
            {
                _messageBoxService.ShowMessage(message, "Error", MessageBoxImage.Error, MessageBoxButton.OK);
            };
        }
    }
}

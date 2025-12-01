using System.Windows;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Config
{
    public class StartUpNotifier
    {
        private readonly StartupDataLoader _startupDataLoader;
        private readonly IMessageBoxService _messageBoxService;

        public StartUpNotifier(StartupDataLoader startupDataLoader, IMessageBoxService messageBoxService)
        {
            _startupDataLoader = startupDataLoader;
            _messageBoxService = messageBoxService;

            _startupDataLoader.LoadCompleted += OnStartUp;
        }

        private void OnStartUp()
        {
            _messageBoxService.ShowMessage("Data base is loaded. You can use the app",
                "Succsess",
                MessageBoxImage.Information,
                MessageBoxButton.OK);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.ViewModels;

namespace WPF_Starter.Config.Initialization
{
    public class MainWindowInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MainWindow InitMainWindow()
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

            mainWindow.DataContext = viewModel;

            return mainWindow;
        }
    }
}
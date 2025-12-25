using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.View;
using WPF_Starter.ViewModels;

namespace WPF_Starter.Config.Initialization
{
    public class SettingsWindowInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public SettingsWindowInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public SettingsWindow Init()
        {
            SettingsWindow? settingsWindow = _serviceProvider.GetRequiredService<SettingsWindow>();
            SettingsWindowViewModel? viewModel = _serviceProvider.GetRequiredService<SettingsWindowViewModel>();

            settingsWindow.DataContext = viewModel;

            return settingsWindow;
        }
    }
}

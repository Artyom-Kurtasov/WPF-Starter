using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.View;
using WPF_Starter.ViewModels;

namespace WPF_Starter.Config.Initialization
{
    public class ExportWindowInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public ExportWindowInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Export Init()
        {
            Export? exportWindow = _serviceProvider.GetRequiredService<Export>();
            ExportWindowViewModel? viewModel = _serviceProvider.GetRequiredService<ExportWindowViewModel>();

            exportWindow.DataContext = viewModel;

            return exportWindow;
        }
    }

}

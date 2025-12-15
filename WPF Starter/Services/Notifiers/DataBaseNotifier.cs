using ControlzEx.Standard;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
{
    public class DataBaseNotifier
    {
        private IServiceProvider _serviceProvider;
        private readonly IMessageBoxService _messageBoxService;
        private readonly LoadingState _loadingState;
        private readonly AppDbContext _appDbContext;

        public DataBaseNotifier(IMessageBoxService messageBoxService, LoadingState loadingState, IServiceProvider serviceProvider,
            AppDbContext appDbContext)
        {
            _messageBoxService = messageBoxService;
            _loadingState = loadingState;
            _serviceProvider = serviceProvider;
            _appDbContext = appDbContext;
        }

        public void OnLoadCompleted(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessage("Success", 
                "Database has been successfully loaded. You can now use the application.",
                MessageDialogStyle.Affirmative);
        }

        public async void OnDataBaseLoadFailed(object sender, EventArgs e)
        {
            _loadingState.MessageResult = await _messageBoxService.ShowMessage("Error",
                "DataBase not found. Do you want to create it?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (_loadingState.MessageResult == MessageDialogResult.Affirmative)
            {
                _appDbContext.Database.EnsureCreated();
            }
        }
    }
}

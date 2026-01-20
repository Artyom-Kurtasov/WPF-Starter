using MahApps.Metro.Controls.Dialogs;
using System.Configuration;
using WPF_Starter.Config.Settings;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.MessageServices.Interfaces;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.Services.Notifiers
{
    public class DatabaseNotifier : IDisposable
    {
        private readonly DatabaseCreator _databaseCreator;
        private ConnectionStringValidator _con;
        private readonly IMessageBoxService _messageBoxService;
        private readonly SettingCommands _settingCommands;

        private bool _isDisposed = false;
        private readonly string? _defaultConnectionString;
        public DatabaseNotifier(IMessageBoxService messageBoxService, SettingCommands settingCommands, ConnectionStringValidator connectionStringValidator,
            DatabaseCreator databaseCreator)
        {
            _defaultConnectionString = ConfigurationManager.ConnectionStrings?["DefaultConnection"]?.ConnectionString;

            _databaseCreator = databaseCreator;
            _messageBoxService = messageBoxService;
            _settingCommands = settingCommands;
            _con = connectionStringValidator;

            _settingCommands.ConnectionEstablishedAsync += OnTestConnectionAsync;
            _settingCommands.ConnectionFailedAsync += OnTestConnectionFailedAsync;
        }
        private async Task OnTestConnectionAsync(object? sender, EventArgs e)
        {
            var result = await _messageBoxService.ShowMessageAsync(
                "Connection test result",
                "Connection established successfully.\n\nDo you want to save the connection string?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                UserSettings.Default.ConnectionString = _con.ConnectionString;
                UserSettings.Default.Save();
                await _messageBoxService.ShowMessageAsync(
                    "Success",
                    "Connection string saved.\nRestart the application to apply changes.",
                    MessageDialogStyle.Affirmative);
            }
        }
        private async Task OnTestConnectionFailedAsync(object? sender, EventArgs e)
        {
            await _messageBoxService.ShowMessageAsync(
                "Connection test failed",
                "The connection string is invalid.\nPlease try another one.",
                MessageDialogStyle.Affirmative);
        }

        public async void OnSqlServerConnectionFailedAsync(object? sender, EventArgs e)
        {
            await _messageBoxService.ShowMessageAsync(
                "Database Connection Failed",
                "Cannot establish connection to SQL server.\n" +
                "Please check the following:\n" +
                "1. SQL Server is installed and running\n" +
                "2. Connection string is correct\n" +
                "Tried connection string:\n" +
                $"1. {_defaultConnectionString}\n",
                MessageDialogStyle.Affirmative);
        }

        public async void OnDatabaseLoadedAsync(object? sender, EventArgs e)
        {
            await _messageBoxService.ShowMessageAsync(
                "Success",
                "Database has been successfully loaded. You can now use the application.",
                MessageDialogStyle.Affirmative);
        }

        public async void OnDatabaseNotFoundAsync(object? sender, EventArgs e)
        {
            var result = await _messageBoxService.ShowMessageAsync(
                   "Database access error",
                   "The selected database could not be opened.\n" +
                   "It may not exist or you may not have permissions.\n\n" +
                   "Do you want to try to create a new database?",
                   MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                if (_databaseCreator.EnsureDatabaseSchema())
                {
                    await _messageBoxService.ShowMessageAsync(
                        "Success",
                        "The database has been successfully created.",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    await _messageBoxService.ShowMessageAsync(
                        "Creation Failed",
                        "Unable to create the database.\n\n" +
                        "Most likely, you don't have sufficient permissions.\n" +
                        "Try running the application as Administrator.",
                        MessageDialogStyle.Affirmative);
                }
            }
        }
        public async void OnTableNotFoundAsync(object? sender, EventArgs e)
        {
            var result = await _messageBoxService.ShowMessageAsync(
                    "Table access error",
                    "The specified table could not be opened.\n" +
                    "It may not exist in the database or you may not have permissions.\n\n" +
                    "Do you want to try to create a new table?",
                    MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                if (_databaseCreator.EnsureDatabaseSchema())
                {
                    await _messageBoxService.ShowMessageAsync(
                        "Success",
                        "The table has been successfully created.",
                        MessageDialogStyle.Affirmative);
                }
                else
                {
                    await _messageBoxService.ShowMessageAsync(
                        "Creation Failed",
                        "Unable to create the database.\n\n" +
                        "Most likely, you don't have sufficient permissions.\n" +
                        "Try running the application as Administrator.",
                        MessageDialogStyle.Affirmative);
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;

            _settingCommands.ConnectionEstablishedAsync -= OnTestConnectionAsync;
            _settingCommands.ConnectionFailedAsync -= OnTestConnectionFailedAsync;
        }
    }
}

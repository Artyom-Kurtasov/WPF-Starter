using System.Windows.Input;
using WPF_Starter.Services.DataBase;

namespace WPF_Starter.ViewModels.Commands
{
    public class SettingCommands
    {
        public event Func<object, EventArgs, Task>? ConnectionEstablishedAsync;
        public event Func<object, EventArgs, Task>? ConnectionFailedAsync;

        private readonly ConnectionStringValidator _connectionStringValidator;
        public ICommand TestConnection { get; }

        public SettingCommands(ConnectionStringValidator connectionStringValidator)
        {
            TestConnection = new AsyncRelayCommand(TestConnectionStringAsync, CanTestConnectionString);

            _connectionStringValidator = connectionStringValidator;
        }


        private async Task TestConnectionStringAsync()
        {
            if (await _connectionStringValidator.ValidateConnectionString())
            {
                    await ConnectionEstablishedAsync(this, EventArgs.Empty);
            }
            else
            {
                    await ConnectionFailedAsync(this, EventArgs.Empty);
            }
        }
        private bool CanTestConnectionString() => true;
    }
}

using Microsoft.EntityFrameworkCore;

namespace WPF_Starter.Services.DataBase
{
    public class ConnectionStringValidator : PropertyChangedEvent
    {
        private string? _connectionString;
        public string? ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                OnPropertyChanged(nameof(ConnectionString));
            }
        }
        public async Task<bool> ValidateConnectionString()
        {
            try
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(ConnectionString)
                    .Options;

                await using var context = new AppDbContext(options);

                return await context.Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}

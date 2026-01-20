using WPF_Starter.Services.DataBase;

namespace WPF_Starter.Config
{
    public interface IStartupDataLoader
    {
        Task InitializeAsync(string filePath, AppDbContext appDbContext, Action<double>? progressAction);
    }
}

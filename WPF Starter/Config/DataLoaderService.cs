using WPF_Starter.Services.DataBase;

namespace WPF_Starter.Config
{
    public class DataLoaderService
    {
        private readonly DataBaseLoader _dataBaseLoader;
        private readonly AppDbContext _appDbContext;

        public DataLoaderService(DataBaseLoader dataBaseLoader, AppDbContext appDbContext)
        {
            _dataBaseLoader = dataBaseLoader;
            _appDbContext = appDbContext;
        }

        public Task LoadAsync()
        {
            return Task.Run(() =>
            {
                _dataBaseLoader.LoadDataBase(_appDbContext);
            });
        }



    }
}

namespace WPF_Starter.Services.DataBase
{
    public class DatabaseCreator
    {

        private readonly AppDbContext _appDbContext;
        public DatabaseCreator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public bool EnsureDatabaseSchema()
        {
            try
            {
                _appDbContext.Database.EnsureCreated();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

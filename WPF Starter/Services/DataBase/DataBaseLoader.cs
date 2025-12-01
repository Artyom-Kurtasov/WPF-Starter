namespace WPF_Starter.Services.DataBase
{
    public class DataBaseLoader
    {
        private readonly DataBaseWriter _writer;
        public DataBaseLoader(DataBaseWriter dataBaseWriter) 
        {
            _writer = dataBaseWriter;
        }
        public void LoadDataBase(AppDbContext dataBase)
        {
                _writer.Save(dataBase);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WPF_Starter.Models;

namespace WPF_Starter.Services.DataBase
{
    public class DataBaseWriter
    {
        private readonly CsvParser _csvParser;
        private readonly ExportSettings _exportSettings;

        public DataBaseWriter(CsvParser csvParser, ExportSettings exportSettings)
        {
            _csvParser = csvParser;
            _exportSettings = exportSettings;
        }
        private void ClearDataBase(AppDbContext dataBase)
        {
            dataBase.Database.ExecuteSqlRaw("TRUNCATE TABLE [dbo].[Table]");
        }

        public void Save(AppDbContext dataBase)
        {
            ClearDataBase(dataBase);
            foreach (var batch in _csvParser.Parse(_exportSettings.CsvFilePath).Chunk(1000))
            {
                {
                    dataBase.AddRange(batch);
                    dataBase.SaveChanges();
                    dataBase.ChangeTracker.Clear();
                }
            }
        }
    }
}

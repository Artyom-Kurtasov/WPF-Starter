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

        /// <summary>
        /// Clear all database
        /// parses a csv file
        /// add and save changes to the database
        /// </summary>
        public async Task SaveAsync(AppDbContext dataBase)
        {
            ClearDataBase(dataBase);

            foreach (People[] batch in _csvParser.Parse(_exportSettings.CsvFilePath).Chunk(1000))
            {
                await dataBase.AddRangeAsync(batch);
                await dataBase.SaveChangesAsync();
                dataBase.ChangeTracker.Clear();
            }
        }
    }
}

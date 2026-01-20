using Microsoft.EntityFrameworkCore;
using WPF_Starter.Models;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.DataBase
{
    public class DataBaseWriter
    {
        private readonly CsvParser _csvParser;
        private readonly ExportSettings _exportSettings;
        private readonly PagingSettings _pagingSettings;

        public DataBaseWriter(CsvParser csvParser, ExportSettings exportSettings, PagingSettings pagingSettings)
        {
            _csvParser = csvParser;
            _exportSettings = exportSettings;
            _pagingSettings = pagingSettings;
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
        public async Task SaveAsync(AppDbContext dataBase, Action<double> progressAction)
        {
           ClearDataBase(dataBase);

           await foreach (People[] batch in _csvParser.Parse(_exportSettings.CsvFilePath, _pagingSettings.BlockSize, int.Parse(_exportSettings.BufferSize), progressAction).Chunk(1000))
            {
                await dataBase.AddRangeAsync(batch);
                await dataBase.SaveChangesAsync();
                dataBase.ChangeTracker.Clear();
            }

            _exportSettings.PercentOfRows = dataBase.Person.Count();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels.DataBaseServices
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
            var deletable = dataBase.Person.Where(u => u.Id != null);
            dataBase.Person.RemoveRange(deletable);
        }

        public void Save(AppDbContext dataBase)
        {
            ClearDataBase(dataBase);
            foreach (var batch in _csvParser.Parse(_exportSettings.CsvFileName).Chunk(1000))
            {
                dataBase.AddRange(batch);
                dataBase.SaveChanges();
                dataBase.ChangeTracker.Clear();
            }
        }
    }
}

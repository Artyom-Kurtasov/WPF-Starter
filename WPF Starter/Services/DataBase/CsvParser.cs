using WPF_Starter.Models;
using WPF_Starter.Services.FileServices;

namespace WPF_Starter.Services.DataBase
{
    public class CsvParser
    {
        private readonly CsvFileReader _csvFileReader;
        private readonly CsvRowParser _csvRowParser;
        private readonly PeopleMapper _peopleMapper;

        public CsvParser(CsvFileReader csvFileReader, CsvRowParser csvRowParser, PeopleMapper peopleMapper)
        {
            _csvFileReader = csvFileReader;
            _csvRowParser = csvRowParser;
            _peopleMapper = peopleMapper;
        }
        public IEnumerable<People> Parse(string fileName)
        {
            foreach (var line in _csvFileReader.ReadLines(fileName))
            {
                string[] cells = _csvRowParser.ParseRow(line);
                if (cells == null) continue;

                People? person = _peopleMapper.Map(cells);
                if (person != null) yield return person;
            }
        }

    }
}

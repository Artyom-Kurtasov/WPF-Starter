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

        /// <summary>
        /// Parses a csv file
        /// splits row into cells
        /// ,aps the cells into People records
        /// </summary>
        public async IAsyncEnumerable<People> Parse(string filePath, int blockSize, int bufferSize, Action<long>? progressAction = null)
        {
            int processed = 0;
            await foreach (List<string?> block in _csvFileReader.ReadLines(filePath, blockSize, bufferSize))
            {
                foreach (string? line in block)
                {
                    string[]? cells = _csvRowParser.ParseRow(line);
                    if (cells == null || cells.Length == 0) continue;

                    processed++;
                    if (processed % 100 == 0)
                    {
                        progressAction?.Invoke(processed);
                    }

                    People? person = _peopleMapper.Map(cells);
                    if (person != null) yield return person;
                }
            }
        }
    }
}

using System.IO;
using WPF_Starter.Models;
using WPF_Starter.Services.FileServices;

namespace WPF_Starter.Services.DataBase
{
    public class CsvParser
    {
        private readonly CsvFileReader _csvFileReader;
        private readonly CsvRowParser _csvRowParser;
        private readonly PeopleMapper _peopleMapper;
        private readonly ExportSettings _exportSettings;

        public CsvParser(CsvFileReader csvFileReader, CsvRowParser csvRowParser, PeopleMapper peopleMapper, ExportSettings exportSettings)
        {
            _csvFileReader = csvFileReader;
            _csvRowParser = csvRowParser;
            _peopleMapper = peopleMapper;
            _exportSettings = exportSettings;
        }

        /// <summary>
        /// Parses a csv file
        /// splits row into cells
        /// ,aps the cells into People records
        /// </summary>
        public async IAsyncEnumerable<People> Parse(string filePath, int blockSize, int bufferSize, Action<double>? progressAction = null)
        {
            FileInfo file = new FileInfo(_exportSettings.CsvFilePath);
            int fullPercent = 100;
            long onePercnetInBytes = file.Length / fullPercent;
            long nextPercnetInBytes = onePercnetInBytes;
            double progress = 0.0;

            await foreach ((List<string?> block, long bytes) in _csvFileReader.ReadLines(filePath, blockSize, bufferSize))
            {
                if (block == null)
                {
                    if (bytes >= nextPercnetInBytes)
                    {
                        if (progress >= 0.99) continue;

                        progress += 0.01;

                        progressAction?.Invoke(progress);

                        nextPercnetInBytes += onePercnetInBytes;
                    }
                    continue;
                }

                foreach (string? line in block)
                {
                    string[]? cells = _csvRowParser.ParseRow(line);
                    if (cells == null || cells.Length == 0) continue;

                    People? person = _peopleMapper.Map(cells);
                    if (person != null) yield return person;
                }
            }
        }
    }
}

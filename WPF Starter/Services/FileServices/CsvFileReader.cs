using System.IO;
using System.Text;

namespace WPF_Starter.Services.FileServices
{
    public class CsvFileReader
    {
        public IEnumerable<string> ReadLines(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 65536);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    yield return line;
            }
        }
    }
}

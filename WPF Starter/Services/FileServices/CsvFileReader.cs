using System.IO;
using System.Text;

namespace WPF_Starter.Services.FileServices
{
    public class CsvFileReader
    {
        public async IAsyncEnumerable<List<string?>> ReadLines(string filePath, int blockSize, int sizeOfBuffer)
        {
            List<string?> currentBlock = new List<string?>(blockSize);

            using StreamReader reader = new StreamReader(filePath, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: sizeOfBuffer);
            while (true)
            {
                string? line = await reader.ReadLineAsync();
                if (line == null) break;

                if (!string.IsNullOrEmpty(line))
                {
                    currentBlock.Add(line);
                }
                if (currentBlock.Count == blockSize)
                {
                    yield return currentBlock;
                    currentBlock = new List<string?>(blockSize);
                }    
            }

            if (currentBlock.Count > 0)
            {
                yield return currentBlock;
            }
        }
    }
}

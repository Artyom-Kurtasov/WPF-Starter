using System.IO;
using System.Text;

namespace WPF_Starter.Services.FileServices
{
    public class CsvFileReader
    {
        public async IAsyncEnumerable<(List<string?>?, long bytes)> ReadLines(string filePath, int blockSize, int sizeOfBuffer)
        {
            List<string?> currentBlock = new List<string?>(blockSize);

            using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read); 
            using StreamReader reader = new StreamReader(fileStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: sizeOfBuffer);

            while (true)
            {
                string? line = await reader.ReadLineAsync();

                if (line == null) break;

                long currentPosition = fileStream.Position;
                yield return (null, currentPosition);

                if (!string.IsNullOrEmpty(line))
                {
                    currentBlock.Add(line);
                }
                if (currentBlock.Count == blockSize)
                {
                    yield return (currentBlock, 0);
                    currentBlock = new List<string?>(blockSize);
                }    
            }

            if (currentBlock.Count > 0)
            {
                yield return (currentBlock, 0);
            }
        }
    }
}

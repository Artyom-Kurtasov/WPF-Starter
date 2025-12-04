namespace WPF_Starter.Services
{
    public class CsvRowParser
    {
        public string[]? ParseRow(string line)
        {
            string[] cells = line.Split(';');
            return cells.Length < 6 ? null : cells;
        }
    }
}

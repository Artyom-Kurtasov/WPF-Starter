using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels.DataBaseServices
{
    public class CsvParser
    {
        public IEnumerable<People> Parse(string fileName)
        {
            var formats = new[] { "dd.MM.yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };
            var culture = new CultureInfo("ru-RU");

            using var reader = new StreamReader(fileName, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 65536);
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var cells = line.Split(';');
                if (cells.Length < 6) continue;

                if (!DateTime.TryParseExact(cells[0], formats, culture, DateTimeStyles.None, out var date)) continue;

                yield return new People
                {
                    Date = date,
                    Name = cells[1],
                    Surname = cells[2],
                    Patronymic = cells[3],
                    City = cells[4],
                    Country = cells[5]
                };
            }
        }

    }
}

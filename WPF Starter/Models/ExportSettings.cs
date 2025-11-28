using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Starter.Models
{
    public class ExportSettings
    {
        public string? ExcelFileName { get; set; } = null;
        public string? CsvFileName { get; set; } = "people.csv";
        public string? XmlFileName { get; set; } = null;
    }
}

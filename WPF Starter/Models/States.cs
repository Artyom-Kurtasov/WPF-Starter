using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF_Starter.View;

namespace WPF_Starter.Models
{
    public class States
    {
        public LinkedList<People> peoples = new();
        public List<People> AllSov = new();
        public string? CsvFileName { get; set; }
        public string[]? TextFromSeacrhBox { get; set; }
        public string? DateBoxText { get; set; }
        public string? NameBoxText { get; set; }
        public string? SurnameBoxText { get; set; }
        public string? PatronymicBoxText { get; set; }
        public string? CityBoxText { get; set; }
        public string? CountryBoxText { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; } = 1000;
        public string? ExcelFileName { get; set; } = null;

    }
}

using System.Globalization;
using WPF_Starter.Models;

namespace WPF_Starter.Services
{
    public class PeopleMapper
    {
        private readonly string[] _formats = new[] { "dd.MM.yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };
        private readonly CultureInfo _culture = new CultureInfo("ru-RU");

        /// <summary>
        /// Maps an array of CSV row cells into a People object
        /// supports multiple date formats
        /// </summary>
        public People? Map(string[] cells)
        {
            if (!DateTime.TryParseExact(cells[0], _formats, _culture, DateTimeStyles.None, out DateTime date))
                return null;

            return new People
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

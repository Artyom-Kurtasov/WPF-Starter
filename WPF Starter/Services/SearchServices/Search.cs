using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;

namespace WPF_Starter.Services.SearchServices
{

    public class Search
    {
        private PeopleFormState FormState;

        public Search(PeopleFormState peopleFormState)
        {
            FormState = peopleFormState;
        }

        /// <summary>
        /// Do search People records in the database
        /// applying filters from the form state
        /// </summary>
        public IQueryable<People> SearchPeople(AppDbContext dataBase)
        {
            String? nameFilter = FormState.NameBoxText?.Trim().ToLower();
            String? surnameFilter = FormState.SurnameBoxText?.Trim().ToLower();
            String? patronymicFilter = FormState.PatronymicBoxText?.Trim().ToLower();
            String? cityFilter = FormState.CityBoxText?.Trim().ToLower();
            String? countryFilter = FormState.CountryBoxText?.Trim().ToLower();

            DateTime? parsedDate = FormState.DateOfDatepicker;

            return dataBase.Person.Where(u =>
                (FormState.DateOfDatepicker == null || u.Date == parsedDate) &&
                (string.IsNullOrEmpty(nameFilter) || u.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(surnameFilter) || u.Surname.Contains(surnameFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(patronymicFilter) || u.Patronymic.Contains(patronymicFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(cityFilter) || u.City.Contains(cityFilter, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(countryFilter) || u.Country.Contains(countryFilter, StringComparison.OrdinalIgnoreCase))
            );

        }

    }
}

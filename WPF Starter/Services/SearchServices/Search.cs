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
        public IQueryable<People> SearchPeople(AppDbContext dataBase)
        {
            var nameFilter = FormState.NameBoxText?.Trim().ToLower();
            var surnameFilter = FormState.SurnameBoxText?.Trim().ToLower();
            var patronymicFilter = FormState.PatronymicBoxText?.Trim().ToLower();
            var cityFilter = FormState.CityBoxText?.Trim().ToLower();
            var countryFilter = FormState.CountryBoxText?.Trim().ToLower();

            DateTime? parsedDate = FormState.DateOfDatepicker;

            return dataBase.Person.Where(u =>
                (FormState.DateOfDatepicker == null ||u.Date == parsedDate) &&
                (string.IsNullOrEmpty(nameFilter) || u.Name.ToLower().Contains(nameFilter)) &&
                (string.IsNullOrEmpty(surnameFilter) || u.Surname.ToLower().Contains(surnameFilter)) &&
                (string.IsNullOrEmpty(patronymicFilter) || u.Patronymic.ToLower().Contains(patronymicFilter)) &&
                (string.IsNullOrEmpty(cityFilter) || u.City.ToLower().Contains(cityFilter)) &&
                (string.IsNullOrEmpty(countryFilter) || u.Country.ToLower().Contains(countryFilter))
            );
        }

    }
}

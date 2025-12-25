using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.SearchServices;

namespace WPF_Starter.Services.DataGridServices
{
    public class DataGridManager
    {
        /// <summary>
        /// Return a single page of List<People> from the database
        /// applying search filters and paging settings
        /// </summary>
        public ObservableCollection<People> GetPage(AppDbContext dataBase, PagingSettings pagingSettings, Search search)
        {
            IQueryable<People> query = search.SearchPeople(dataBase);

            return new ObservableCollection<People>(query
                .AsNoTracking()
                .Skip(pagingSettings.Page * pagingSettings.PageSize)
                .Take(pagingSettings.PageSize)
                .ToList());
        }
    }

}

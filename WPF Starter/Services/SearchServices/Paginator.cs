using Microsoft.EntityFrameworkCore;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;

namespace WPF_Starter.Services.SearchServices
{
    public class Paginator
    {

        /// <summary>
        /// Splits People records in pages
        /// </summary>
        public IEnumerable<List<People>> Pagenation(AppDbContext dataBase, PagingSettings pagingSettings, IQueryable<People> query)
        {
            pagingSettings.Page = 0;

            while (true)
            {
                List<People> batch = query
                    .AsNoTracking()
                    .Skip(pagingSettings.Page * pagingSettings.PageSize)
                    .Take(pagingSettings.PageSize)
                    .ToList();

                if (!batch.Any()) yield break;

                pagingSettings.Page++;

                yield return batch;
            }
        }
    }
}

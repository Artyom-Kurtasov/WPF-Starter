using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;

namespace WPF_Starter.Services.SearchServices
{
    public class Paginator
    {
        public IEnumerable<List<People>> Pagenation(AppDbContext dataBase, PagingSettings pagingSettings, IQueryable<People> query)
        {
            pagingSettings.Page = 0;

            while (true)
            {
                var batch = query
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

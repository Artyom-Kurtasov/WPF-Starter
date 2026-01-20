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
        public IEnumerable<List<People>> Pagenation(AppDbContext dataBase, PagingSettings pagingSettings, IQueryable<People> query,
            IProgress<double>? progress = null)
        {
            pagingSettings.Page = 0;

            int totalCount = query.Count();
            int processed = 0;

            while (true)
            {
                List<People> batch = query
                    .AsNoTracking()
                    .Skip(pagingSettings.Page * pagingSettings.PageSize)
                    .Take(pagingSettings.PageSize)
                    .ToList();

                if (!batch.Any()) yield break;

                pagingSettings.Page++;
                processed += batch.Count;

                if (progress != null && totalCount > 0)
                {
                    double percentage = (double)processed / totalCount;
                    progress.Report(percentage);
                }

                yield return batch;
            }
        }
    }
}

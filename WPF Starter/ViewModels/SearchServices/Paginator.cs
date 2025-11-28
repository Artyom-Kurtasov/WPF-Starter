using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels.SearchServices
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

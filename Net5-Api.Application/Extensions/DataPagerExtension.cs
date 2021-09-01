using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Controllers.Model;

namespace Net5_Api.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
        this IQueryable<TModel> query,
        int page,
        int limit,
        CancellationToken cancellationToken)
        where TModel : class
        {
            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            var startRow = (page - 1) * limit;
            paged.Items = await query
                        .Skip(startRow)
                        .Take(limit)
                        .ToListAsync(cancellationToken);

            paged.TotalItems = query.Count();
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}
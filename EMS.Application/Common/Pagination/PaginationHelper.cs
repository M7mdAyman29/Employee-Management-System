
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Common.Pagination
{
    public static class PaginationHelper
    {
        public static async Task<PagedResult<T>> CreateAsync<T>(IQueryable<T> query, int page, int pageSize)
        {

            var total = await query.CountAsync();

            var data = await query.Skip((page - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return new PagedResult<T>
            {
                TotalCount = total,
                Page = page,
                PageSize = pageSize,
                Data = data
            };


        }
    }
}
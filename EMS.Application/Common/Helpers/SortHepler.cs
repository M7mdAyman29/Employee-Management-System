using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.Common.Helpers
{
    public class SortHepler
    {
        public static IQueryable<T> ApplySorting<T>(
    IQueryable<T> query,
    string sortBy,
    bool isAsc = true)
        {

            if (string.IsNullOrEmpty(sortBy))
                return query;

            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, sortBy);
            var lambda = Expression.Lambda(property, param);


            string methodName = isAsc ? "OrderBy" : "OrderByDescending";

            var result = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.Type },
               query.Expression,
                Expression.Quote(lambda));


            return query.Provider.CreateQuery<T>(result);

        }
    }
}

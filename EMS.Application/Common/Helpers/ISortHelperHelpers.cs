namespace EMS.Application.Common.Helpers
{
    internal static interface ISortHelperHelpers
    {
        IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortBy, bool isAsc = true);
    }
}
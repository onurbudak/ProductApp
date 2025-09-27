namespace ProductApp.Application.Extensions;

public static class PaginatedExtension
{
    public static IEnumerable<T>? Paginated<T>(this IEnumerable<T> data, int pageNumber, int pageSize, out int totalItems, out List<T>? paginatedDatas)
    {
        totalItems = data.Count();
        if (pageNumber == 0)
        {
            pageNumber = 1;
        }
        if (pageSize == 0)
        {
            pageSize = int.MaxValue;
        }
        paginatedDatas = data?.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return paginatedDatas;
    }
}

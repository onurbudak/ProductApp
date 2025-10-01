using ProductApp.Application.Filters;

namespace ProductApp.Application.Interfaces.Filters;

public interface IFilterService<T>
{
    IQueryable<T> ApplyFilters(IQueryable<T> query, List<FilterCriteria> filters);
}

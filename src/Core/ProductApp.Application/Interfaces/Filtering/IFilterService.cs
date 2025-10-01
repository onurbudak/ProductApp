using ProductApp.Application.Filtering;

namespace ProductApp.Application.Interfaces.Filtering;

public interface IFilterService<T>
{
    IQueryable<T> ApplyFilters(IQueryable<T> query, List<FilterCriteria> filters);
}

using System.Linq.Dynamic.Core;
using ProductApp.Application.Interfaces.Filtering;

namespace ProductApp.Application.Filtering;

public class DynamicLinqFilterService<T> : IFilterService<T>
{
    public IQueryable<T> ApplyFilters(IQueryable<T> query, List<FilterCriteria> filters)
    {
        if (filters == null || !filters.Any())
            return query;

        var whereClauses = new List<string>();
        var values = new List<object>();

        for (int i = 0; i < filters.Count; i++)
        {
            var f = filters[i];
            string paramName = $"@{i}";

            // Contains desteği
            if (f.Operator.Equals("Contains", StringComparison.OrdinalIgnoreCase))
            {
                whereClauses.Add($"{f.Field}.Contains({paramName})");
            }
            else
            {
                whereClauses.Add($"{f.Field} {f.Operator} {paramName}");
            }

            values.Add(f.Value);
        }

        var whereClause = string.Join(" AND ", whereClauses);
        return query.Where(whereClause, values.ToArray());
    }
}

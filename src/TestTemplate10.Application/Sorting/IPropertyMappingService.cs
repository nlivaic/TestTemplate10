using System.Collections.Generic;
using TestTemplate10.Application.Sorting.Models;

namespace TestTemplate10.Application.Sorting
{
    public interface IPropertyMappingService
    {
        IEnumerable<SortCriteria> Resolve(BaseSortable sortableSource, BaseSortable sortableTarget);
    }
}

using Revision_Part_1.Domain.Entities;

namespace Revision_Part_1.Domain.Interfaces;

public interface IDishService
{
    Task<int> Create(Dish entity);
    Task Delete(IEnumerable<Dish> entities);
}

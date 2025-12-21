using Microsoft.EntityFrameworkCore;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Interfaces;
using Revision_Part_1.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revision_Part_1.Infrastructure.Repositories
{
    internal class DishServiceRepository(RestaurentsDbContext dbContext) : IDishService
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            dbContext.Dishes.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}

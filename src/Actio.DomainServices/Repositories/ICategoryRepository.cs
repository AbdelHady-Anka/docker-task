
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Domain.Entities;

namespace Actio.DomainServices.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(Guid id);
        Task<Category> GetByNameAsync(string name);
        Task AddAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
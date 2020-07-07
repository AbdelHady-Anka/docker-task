using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.DomainServices.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _databse;

        public CategoryRepository(IMongoDatabase databse)
        {
            _databse = databse;
        }

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);


        public async Task<IEnumerable<Category>> GetAllAsync()
            => await Collection.AsQueryable().ToListAsync();

        public async Task<Category> GetByIdAsync(Guid id)
            => await Collection.AsQueryable().FirstOrDefaultAsync(c => c.Id == id);

        public Task<Category> GetByNameAsync(string name)
            => Collection.AsQueryable().FirstOrDefaultAsync(c => c.Name == name.ToLowerInvariant());

        private IMongoCollection<Category> Collection
            => _databse.GetCollection<Category>("Categories");
    }
}
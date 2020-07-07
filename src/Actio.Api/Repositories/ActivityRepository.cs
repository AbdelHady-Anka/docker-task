using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;
        public ActivityRepository(IMongoDatabase database)
        {
            this._database = database;
        }

        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");
        public async Task AddAsync(Activity model)
            => await Collection
                .InsertOneAsync(model);

        public async Task<IEnumerable<Activity>> GetAllAsync(Guid userId)
            => await Collection
            .AsQueryable()
            .Where(a => a.UserId == userId)
            .ToListAsync();

        public async Task<Activity> GetByIdAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(a => a.Id == id);
    }
}
using Actio.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;

namespace Actio.DomainServices.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase _database;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetByIdAsync(Guid id)
            => await Collection
            .AsQueryable()
            .FirstOrDefaultAsync(a => a.Id == id);

        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");
    }
}

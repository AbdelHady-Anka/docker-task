using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase Database;

        protected MongoSeeder(IMongoDatabase database)
        {
            Database = database;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await Database.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await CustomSeeder();
        }

        protected virtual Task CustomSeeder()
        {
            return Task.CompletedTask;
        }
    }
}
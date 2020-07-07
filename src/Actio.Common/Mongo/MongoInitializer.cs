using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitilaizer
    {
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _databaseSeeder;
        private readonly bool _seed;
        private bool _initialized;

        public MongoInitializer(IMongoDatabase database, 
        IDatabaseSeeder databaseSeeder,
        IOptions<MongoOptions> options)
        {
            _database = database;
            _databaseSeeder = databaseSeeder;
            _seed = options.Value.Seed;
            _initialized = false;
        }

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }
            RegisterConventions();
            if (!_seed)
            {
                return;
            }
            await _databaseSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConvention", new MongoConvention(), _ => true);
        }
    }

    internal class MongoConvention : IConventionPack
    {
        public IEnumerable<IConvention> Conventions => new List<IConvention>
        {
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
            new CamelCaseElementNameConvention()
        };

    }
}
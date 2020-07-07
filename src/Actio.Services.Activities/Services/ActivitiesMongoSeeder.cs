using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Mongo;
using Actio.Domain.Entities;
using Actio.DomainServices.Repositories;
using MongoDB.Driver;

namespace Actio.Services.Activities.Services
{
    public class ActivitiesMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;

        public ActivitiesMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository) : base(database)
        {
            _categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeeder()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hoppy"
            };

            await Task.WhenAll(categories.Select(c
            => _categoryRepository.AddAsync(new Category(c))
            ));
        }
    }
}
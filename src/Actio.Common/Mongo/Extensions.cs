using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<MongoOptions>>();
                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<IOptions<MongoOptions>>();

                return mongoClient.GetDatabase(options.Value.Database);
            });

            services.AddScoped<IDatabaseInitilaizer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }
    }
}
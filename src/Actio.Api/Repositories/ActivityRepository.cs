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
        private IMongoCollection<Activity> Collection
            => _database.GetCollection<Activity>("Activities");

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Activity activity)
        {
            await Collection.InsertOneAsync(activity);
        }

        public async Task<IEnumerable<Activity>> BrowseAsync(Guid userId)
        {
            return await Collection
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
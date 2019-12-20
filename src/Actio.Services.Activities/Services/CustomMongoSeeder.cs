using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IActivityRepository _activityRepository;

        public CustomMongoSeeder(IMongoDatabase database,
            ICategoryRepository categoryRepository,
            IActivityRepository activityRepository) : base(database)
        {
            _categoryRepository = categoryRepository;
            _activityRepository = activityRepository;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>
            {
                "work",
                "sports",
                "hobbies"
            };

            await Task.WhenAll(categories.Select(x =>
                _categoryRepository.AddAsync(new Category(x))));
        }
    }
}
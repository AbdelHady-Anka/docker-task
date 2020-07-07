using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Domain.Entities;
using Actio.DomainServices.Repositories;

namespace Actio.DomainServices.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            this._activityRepository = activityRepository;
            this._categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string category,
            string name, string description, DateTime createdAt)
        {
            var activityCategory = _categoryRepository.GetByNameAsync(category);
            if (activityCategory == null)
            {
                throw new ActioException("category_not_found", $"Category {category} was not found.");
            }
            await _activityRepository.AddAsync(new Activity(id, userId, category, name, description, createdAt));
        }
    }
}
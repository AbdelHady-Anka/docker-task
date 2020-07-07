using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.Models;

namespace Actio.Api.Repositories
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity model);
        Task<Activity> GetByIdAsync(Guid id);
        Task<IEnumerable<Activity>> GetAllAsync(Guid userId);
    }
}
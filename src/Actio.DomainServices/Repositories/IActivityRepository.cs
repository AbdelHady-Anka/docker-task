using System;
using System.Threading.Tasks;
using Actio.Domain.Entities;

namespace Actio.DomainServices.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetByIdAsync(Guid id);
        Task AddAsync(Activity activity);
    }
}
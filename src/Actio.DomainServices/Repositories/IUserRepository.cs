using System;
using System.Threading.Tasks;
using Actio.Domain.Entities;

namespace Actio.DomainServices.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ValidateUser(string email, string password);
        Task AddAsync(User user);
    }
}
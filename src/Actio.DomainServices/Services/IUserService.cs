using System.Threading.Tasks;
using Actio.Common.Auth;

namespace Actio.DomainServices.Services
{
    public interface IUserService
    {
        Task RegisterAsync(string email, string password, string name);
        Task<JsonWebToken> LoginAsync(string email, string password);
    }
}
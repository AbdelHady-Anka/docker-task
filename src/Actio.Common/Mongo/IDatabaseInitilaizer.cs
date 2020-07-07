using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public interface IDatabaseInitilaizer
    {
        Task InitializeAsync();
    }
}
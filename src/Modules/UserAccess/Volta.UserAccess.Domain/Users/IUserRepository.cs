using System.Threading;
using System.Threading.Tasks;

namespace Volta.UserAccess.Domain.Users
{
    public interface IUserRepository
    {
        Task Add(User user, CancellationToken cancellationToken = default);
    }
}
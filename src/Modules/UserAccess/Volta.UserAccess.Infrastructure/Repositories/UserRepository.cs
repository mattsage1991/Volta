using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.UserAccess.Domain.Users;

namespace Volta.UserAccess.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext dbContext;

        public UserRepository(UsersContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(User user, CancellationToken cancellationToken = default)
        {
            await dbContext.Users.AddAsync(user, cancellationToken);
        }
    }
}
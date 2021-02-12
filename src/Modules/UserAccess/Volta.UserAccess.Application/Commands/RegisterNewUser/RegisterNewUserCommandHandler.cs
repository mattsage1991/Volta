using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application;
using Volta.UserAccess.Domain.Users;
using Volta.UserAccess.Domain.Users.Services;

namespace Volta.UserAccess.Application.Commands.RegisterNewUser
{
    public class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public RegisterNewUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Register(Email.Of(request.Email), Password.Of(request.Password, passwordHasher));

            await userRepository.Add(user, cancellationToken);

            return user.Id.Value;
        }
    }
}
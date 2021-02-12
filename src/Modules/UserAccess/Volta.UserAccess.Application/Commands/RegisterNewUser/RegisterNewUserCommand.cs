using System;
using System.Windows.Input;
using Volta.BuildingBlocks.Application;

namespace Volta.UserAccess.Application.Commands.RegisterNewUser
{
    public class RegisterNewUserCommand : ICommand<Guid>
    {
        public string Email { get; }
        public string Password { get; set; }

        public RegisterNewUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
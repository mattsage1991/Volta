namespace Volta.UserAccess.Domain.Users.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
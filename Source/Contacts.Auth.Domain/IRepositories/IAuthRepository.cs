using Contacts.Auth.Domain.Entities;

namespace Contacts.Auth.Domain.IRepositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByUUId(Guid uuId);
        Task<User> CreateUser(RegisterUser registerUser);
        Task<bool> UpdateUser(ResetPassword resetPassword); 

    }
}

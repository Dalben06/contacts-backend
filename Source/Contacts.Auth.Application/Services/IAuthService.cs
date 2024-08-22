using Contacts.Auth.Application.ViewModel;
using Contacts.Auth.Domain.Entities;
using Contacts.Core.Domain;

namespace Contacts.Auth.Application.Services
{
    public interface IAuthService
    {
        Task<RequestResponse<UserAuthViewModel>> Login(AuthViewModel model);
        Task<RequestResponse<UserViewModel>> Register(AuthViewModel model);
        Task<bool> IsUserAutheticated(Guid id);
        UserSession UserSession { get; }
    }
}

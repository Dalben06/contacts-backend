using AutoMapper;
using Contacts.Auth.Application.ViewModel;
using Contacts.Auth.Domain.Entities;
using Contacts.Auth.Domain.IRepositories;
using Contacts.Core.Configuration;
using Contacts.Core.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace Contacts.Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly string _secret;
        private UserSession _userSession;

        public UserSession UserSession => _userSession;

        public AuthService(IAuthRepository authRepository, IMapper mapper, Settings settings)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _secret = settings.Configuration.Secret;
        }

        public async Task<RequestResponse<UserAuthViewModel>> Login(AuthViewModel model)
        {
            var notification = new Notifiable();
            try
            {
                var user = await _authRepository.GetUserByUsernameAsync(model?.Username);

                if (user is null) throw new Exception("User not found!");

                if (!user.IsCorrectPassword(model.Password)) throw new Exception("Password invalid!");

                var expiredDate = DateTimeOffset.Now.AddDays(1);
                return new RequestResponse<UserAuthViewModel>(new UserAuthViewModel
                {
                    Username = user.Username,
                    UUId = user.UUId,
                    JWTToken = GenerateJWTToken(user,expiredDate),
                    ExpiredDate = expiredDate
                }, notification);

            }
            catch (Exception ex)
            {
                notification.AddNotification(ex.Message);
            }

            return new RequestResponse<UserAuthViewModel>(notification);
        }

        public async Task<RequestResponse<UserViewModel>> Register(AuthViewModel model)
        {
            var noticable = new Notifiable();
            try
            {
                var existUser = await _authRepository.GetUserByUsernameAsync(model.Username);
                if (existUser != null)
                    throw new Exception("Username already used");

                var user = new RegisterUser(model.Username, model.Password);
                if (noticable.Valid)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var newUser = await _authRepository.CreateUser(user);
                        scope.Complete();
                        return new RequestResponse<UserViewModel>(_mapper.Map<UserViewModel>(newUser), noticable);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                noticable.AddNotification(ex.Message);
            }
            return new RequestResponse<UserViewModel>(noticable);
        }

        public async Task<bool> IsUserAutheticated(Guid id)
        {
            if (id == Guid.Empty) return false;

            var user = await _authRepository.GetUserByUUId(id);
            if (user == null) return false;

            _userSession = new UserSession(user.Id, user.UUId, user.Username);
            return true;

        }

        private string GenerateJWTToken(User Usuario, DateTimeOffset expiredDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                                new Claim(ClaimTypes.Name, Usuario.Username),
                                new Claim(ClaimTypes.NameIdentifier, Usuario.UUId.ToString()),
                }),
                Expires = expiredDate.DateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));


        }

       
    }
}

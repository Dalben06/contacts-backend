using AutoMapper;
using Contacts.Auth.Application.ViewModel;
using Contacts.Auth.Domain.Entities;

namespace Contacts.Auth.Application.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}

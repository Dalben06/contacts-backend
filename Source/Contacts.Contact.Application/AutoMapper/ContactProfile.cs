using AutoMapper;
using Contacts.Contact.Application.ViewModel;
using Contacts.Contact.Domain.Entities;
using Contacts.Contact.Domain.Enums;
using Contacts.Core.Extensions;

namespace Contacts.Contact.Application.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<PersonViewModel, Person>()
                .ConstructUsing(p => new Person(p.Name, p.Number, (CountryCode)p.Code, p.Email, p.UserId))
                .ForMember(dest => dest.Number, src => src.MapFrom(src => src.Number.OnlyNumbers()))
                .ForMember(dest => dest.UUId, src => src.MapFrom(src => src.UUId == Guid.Empty ? Guid.NewGuid() : src.UUId))
                .ReverseMap();
        }
    }
}

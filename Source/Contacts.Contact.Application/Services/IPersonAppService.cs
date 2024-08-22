using Contacts.Contact.Application.ViewModel;
using Contacts.Core.Domain;

namespace Contacts.Contact.Application.Services
{
    public interface IPersonAppService
    {

        Task<RequestResponse<IEnumerable<PersonViewModel>>> GetContacts();
        Task<RequestResponse<IEnumerable<PersonViewModel>>> GetContactsFromFilter(string filter);
        Task<RequestResponse<PersonViewModel>> GetContact(Guid Id);
        Task<RequestResponse<PersonViewModel>> CreateContact(PersonViewModel model);
        Task<RequestResponse<PersonViewModel>> UpdateContact(PersonViewModel model);
        Task<NoContentResponse> RemoveContact(Guid Id, int UserId);
    }
}

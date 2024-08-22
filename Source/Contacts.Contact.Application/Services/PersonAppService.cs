using AutoMapper;
using Contacts.Contact.Application.ViewModel;
using Contacts.Contact.Domain.Entities;
using Contacts.Contact.Domain.IRepositories;
using Contacts.Core.Domain;
using System.Transactions;

namespace Contacts.Contact.Application.Services
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonAppService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

     

        public async Task<RequestResponse<PersonViewModel>> GetContact(Guid Id)
        {
            try
            {
                return new RequestResponse<PersonViewModel>(
                    this._mapper.Map<PersonViewModel>(await _personRepository.GetById(Id))
                    , new Notifiable());
            }
            catch (Exception ex)
            {
                return new RequestResponse<PersonViewModel>(ex.Message);
            }
        }

        public async Task<RequestResponse<IEnumerable<PersonViewModel>>> GetContacts()
        {
            try
            {
                return new RequestResponse<IEnumerable<PersonViewModel>>(
                    this._mapper.Map<IEnumerable<PersonViewModel>>(await _personRepository.GetAllAsync())
                    , new Notifiable());
            }
            catch (Exception ex)
            {
                return new RequestResponse<IEnumerable<PersonViewModel>>(ex.Message);
            }
        }

        public async Task<RequestResponse<IEnumerable<PersonViewModel>>> GetContactsFromFilter(string filter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filter))
                    throw new Exception("filter can't be empty");


                return new RequestResponse<IEnumerable<PersonViewModel>>(
                    this._mapper.Map<IEnumerable<PersonViewModel>>(await _personRepository.GetByFilterAsync(filter))
                    , new Notifiable());
            }
            catch (Exception ex)
            {
                return new RequestResponse<IEnumerable<PersonViewModel>>(ex.Message);
            }
        }

        public async Task<RequestResponse<PersonViewModel>> CreateContact(PersonViewModel model)
        {
            var noticable = new Notifiable();
            try
            {
                var person = _mapper.Map<Person>(model);
                person.Validate();  
                noticable.AddNotifications(person.Notifications);

                if (noticable.Valid)
                {
                    person.UUId = Guid.NewGuid();
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        person = await _personRepository.InsertAsync(person);

                        scope.Complete();
                    }
                    return new RequestResponse<PersonViewModel>(_mapper.Map<PersonViewModel>(person), noticable);
                }
            }
            catch (Exception ex)
            {
                noticable.AddNotification(ex.Message);
            }
            return new RequestResponse<PersonViewModel>(noticable);
        }
        public async Task<RequestResponse<PersonViewModel>> UpdateContact(PersonViewModel model)
        {
            var noticable = new Notifiable();
            try
            {
                var person = _mapper.Map<Person>(model);
                person.Validate();
                var orig = await _personRepository.GetById(person.UUId);

                if (orig is null)
                    noticable.AddNotification("Person not found!");


                noticable.AddNotifications(person.Notifications);
                if (noticable.Valid)
                {
                    person.Id = orig.Id;
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _personRepository.UpdateAsync(person);
                        scope.Complete();
                    }
                    return new RequestResponse<PersonViewModel>(_mapper.Map<PersonViewModel>(person), noticable);
                }
            }
            catch (Exception ex)
            {
                noticable.AddNotification(ex.Message);
            }
            return new RequestResponse<PersonViewModel>(noticable);
        }
        public async Task<NoContentResponse> RemoveContact(Guid Id, int UserId)
        {
            var noticable = new Notifiable();
            try
            {
                var orig = await _personRepository.GetById(Id);

                if (orig is null)
                    throw new Exception("Person not found!");

                orig.Disable(UserId);
                if (noticable.Valid)
                {
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _personRepository.DeleteAsync(orig);
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                noticable.AddNotification(ex.Message);
            }
            return new NoContentResponse(noticable);
        }

    }
}

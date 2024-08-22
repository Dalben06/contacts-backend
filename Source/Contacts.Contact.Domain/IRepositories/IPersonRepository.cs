using Contacts.Contact.Domain.Entities;

namespace Contacts.Contact.Domain.IRepositories
{
    public interface IPersonRepository
    {
        Task<Person> GetById(Guid id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<IEnumerable<Person>> GetByFilterAsync(string searchWord);
        Task<Person> InsertAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> DeleteAsync(Person person);

    }
}

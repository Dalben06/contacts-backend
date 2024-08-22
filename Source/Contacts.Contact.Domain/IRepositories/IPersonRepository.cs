using Contacts.Contact.Domain.Entities;

namespace Contacts.Contact.Domain.IRepositories
{
    public interface IPersonRepository
    {
        Task<Person> GetById(Guid id, int IdUser);
        Task<IEnumerable<Person>> GetAllAsync(int IdUser);
        Task<IEnumerable<Person>> GetByFilterAsync(string searchWord, int IdUser);
        Task<Person> InsertAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> DeleteAsync(Person person);

    }
}

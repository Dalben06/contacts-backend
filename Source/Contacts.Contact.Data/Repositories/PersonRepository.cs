using Contacts.Contact.Domain.Entities;
using Contacts.Contact.Domain.IRepositories;
using Contacts.Core.Data;
using Dapper;
using static Dapper.SqlMapper;
using Contacts.Core.Extensions;

namespace Contacts.Contact.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        private readonly DbContext _dbContext;

        public PersonRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private string GetDefaultSql()
        {
            return @"SELECT Persons.* FROM Persons WHERE 1=1 and Persons.IsDeleted = 0 ";
        }

        public async Task<Person> GetById(Guid id)
        {
            return await _dbContext.DbConnection.QueryFirstOrDefaultAsync<Person>(GetDefaultSql() + " AND Persons.UUId = @id", new { id});
        }
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _dbContext.DbConnection.QueryAsync<Person>(GetDefaultSql());
        }

        public async Task<IEnumerable<Person>> GetByFilterAsync(string searchWord)
        {
            var sql = GetDefaultSql() + @" AND Persons.Name like @searchWord 
                                           AND Persons.Number like @searchWord
                                           AND Persons.Email like @searchWord";

            return await _dbContext.DbConnection.QueryAsync<Person>(sql, searchWord );
        }
        
        public async Task<Person> InsertAsync(Person person)
        {
            return await this._dbContext.DbConnection.CoreInsertAsync<Person>(person);
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            return await this._dbContext.DbConnection.CoreUpdateAsync<Person>(person);
        }

        public async Task<bool> DeleteAsync(Person person)
        {
            return await this._dbContext.DbConnection.CoreUpdateAsync<Person>(person);
        }

        
    }
}

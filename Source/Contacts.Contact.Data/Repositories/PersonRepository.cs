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

        public async Task<Person> GetById(Guid id, int IdUser)
        {
            return await _dbContext.DbConnection.QueryFirstOrDefaultAsync<Person>(GetDefaultSql() + " AND Persons.CreateUserId = @IdUser  AND Persons.UUId = @id", new { id, IdUser });
        }
        public async Task<IEnumerable<Person>> GetAllAsync(int IdUser)
        {
            return await _dbContext.DbConnection.QueryAsync<Person>(GetDefaultSql() + " AND Persons.CreateUserId = @IdUser", new {IdUser});
        }

        public async Task<IEnumerable<Person>> GetByFilterAsync(string searchWord, int IdUser)
        {
            var sql = GetDefaultSql() + @" AND (Persons.Name like @searchWord 
                                           OR Persons.Number like @searchWord
                                           OR Persons.Email like @searchWord)
                                            AND Persons.CreateUserId = @IdUser";

            return await _dbContext.DbConnection.QueryAsync<Person>(sql, new { searchWord, IdUser} );
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

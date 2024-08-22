using Contacts.Auth.Domain.Entities;
using Contacts.Auth.Domain.IRepositories;
using Contacts.Core.Data;
using Contacts.Core.Extensions;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Contacts.Auth.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DbContext _dbContext;

        public AuthRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private string GetDefaultSql()
        {
            return @"SELECT Users.* FROM Users WHERE 1=1 ";
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.DbConnection.QueryFirstOrDefaultAsync<User>(GetDefaultSql() + " AND Users.Username = @username", new { username });    
        }

        public async Task<User> GetUserByUUId(Guid uuId)
        {
            return await _dbContext.DbConnection.QueryFirstOrDefaultAsync<User>(GetDefaultSql() + " AND Users.UUId = @uuId", new { uuId });
        }
        public async Task<User> CreateUser(RegisterUser registerUser)
        {
            registerUser.Id = await _dbContext.DbConnection.InsertAsync<RegisterUser>(registerUser);
            return new User(registerUser.Id,
                registerUser.UUId,
                registerUser.Username,
                registerUser.Password,
                registerUser.PasswordSalt,
                registerUser.CreateDate,
                registerUser.UpdateDate
                );
        }

        public async Task<bool> UpdateUser(ResetPassword resetPassword)
        {
            var command = @"UPDATE Users SET Password = @Password, PasswordSalt = @PasswordSalt WHERE UUId = @UUId = @UUId ";
            return (await _dbContext.DbConnection.ExecuteAsync(command, resetPassword)) > 0;
        }

       
    }
}

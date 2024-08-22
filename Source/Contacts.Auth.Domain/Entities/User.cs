using Contacts.Core.DataAnnotations;
using Contacts.Core.Domain;
using Dapper.Contrib.Extensions;
using System.Text;

namespace Contacts.Auth.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        [OnlyInsert]
        public DateTimeOffset CreateDate { get; private set; }
        public DateTimeOffset UpdateDate { get; private set; }
        public User()
        {
                
        }
        public User(int id, Guid uuid, string userName, string pass, string passSalt, DateTimeOffset createDate, DateTimeOffset updateDate )
        {
            Id = id;
            UUId=  uuid;
            Username = userName;
            Password = pass;
            PasswordSalt = passSalt;
            CreateDate = createDate;
            UpdateDate = updateDate;
        }

        public User(string username, string password, string passwordSalt)
        {
            Username = username;
            Password = password;
            PasswordSalt = passwordSalt;
        }

        public bool IsCorrectPassword(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            var storedHash = Convert.FromBase64String(Password);
            var storedSalt = Convert.FromBase64String(PasswordSalt);
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != storedHash[i]) return false;
            }

            return true;
        }
        public override void Disable(int idUser)
        {
            return;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Username)) AddNotification("Username can't be empty");
        }
    }
}

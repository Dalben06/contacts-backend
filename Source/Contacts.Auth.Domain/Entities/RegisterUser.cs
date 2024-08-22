using Contacts.Core.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace Contacts.Auth.Domain.Entities
{
    [Table("Users")]
    public class RegisterUser 
    {
        public RegisterUser(string username, string password)
        {
            UUId = Guid.NewGuid();
            Username = username;
            GeneratePassword(password);
            UpdateDate = CreateDate = DateTimeOffset.Now;
        }
        [Key]
        public int Id { get; set; }
        [ExplicitKey]
        [OnlyInsert]
        public Guid UUId { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        [OnlyInsert]
        public DateTimeOffset CreateDate { get; private set; }
        public DateTimeOffset UpdateDate { get; private set; }

        private void GeneratePassword(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = Convert.ToBase64String(hmac.Key);
                Password = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }


        
    }
}

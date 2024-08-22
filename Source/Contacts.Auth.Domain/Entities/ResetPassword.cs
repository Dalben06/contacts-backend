using Contacts.Core.Domain;
using Dapper.Contrib.Extensions;

namespace Contacts.Auth.Domain.Entities
{
    public class ResetPassword 
    {
        [Key]
        public int Id { get; set; }
        public Guid UUId { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
     
    }
}

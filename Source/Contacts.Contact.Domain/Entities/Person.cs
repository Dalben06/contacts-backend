using Contacts.Contact.Domain.Enums;
using Contacts.Core.Domain;
using Contacts.Core.Extensions;
using Dapper.Contrib.Extensions;

namespace Contacts.Contact.Domain.Entities
{
    [Table("Persons")]
    public class Person : AuditEntity
    {
        public string Name { get; private set; }
        public string Number { get; private set; }
        public CountryCode Code { get; private set; }
        public string Email { get; private set; }

        [Computed]
        public string FormatNumber { get { return $"+{(int)Code}{Number}"; } }

        public Person(): base(0)
        {
            
        }
        public Person(string name, string number, CountryCode code, string email, int idUser) : base(idUser)
        {
            Name = name;
            Number = number;
            Code = code;
            Email = email;
        }

        public override void Disable(int idUser)
        {
            this.IsDeleted = true;
            this.DeleteDate = this.UpdateDate = DateTime.UtcNow;
            this.DeleteUserId = this.UpdateUserId = (int)idUser;
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Name)) AddNotification("Name is required!");

            if (Name?.Length > 50) AddNotification("Email max caracteres is 50");

            if (string.IsNullOrWhiteSpace(Number.OnlyNumbers()) || string.IsNullOrEmpty(Number.OnlyNumbers())) AddNotification("Number is required!");

            if (Number?.Length > 20) AddNotification("Number max caracteres is 20");

            if (Email?.Length > 250) AddNotification("Email max caracteres is 250");
        }
    }
}

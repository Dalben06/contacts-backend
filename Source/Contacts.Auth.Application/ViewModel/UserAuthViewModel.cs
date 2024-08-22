namespace Contacts.Auth.Application.ViewModel
{
    public class UserAuthViewModel
    {
        public Guid UUId { get; set; }
        public string JWTToken { get; set; }
        public string Username { get; set; }
        public DateTimeOffset ExpiredDate { get; set; }
    }
}

namespace Contacts.Auth.Domain.Entities
{
    public class UserSession
    {
        public UserSession(int userId, Guid userUUId, string username)
        {
            UserId = userId;
            UserUUId = userUUId;
            Username = username;
        }

        public int UserId { get; set; }
        public Guid UserUUId { get; set; }
        public string Username { get; set; }
    }
}

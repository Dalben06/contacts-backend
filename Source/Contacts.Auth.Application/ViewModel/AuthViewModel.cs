using System.ComponentModel.DataAnnotations;

namespace Contacts.Auth.Application.ViewModel
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Username is Required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}

using Contacts.Core.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Contact.Application.ViewModel
{
    public class PersonViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(50, ErrorMessage = "Name should be max {1} caracteres!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Number is required!")]
        [StringLength(20, ErrorMessage = "Number should be max {1} caracteres")]
        public string Number { get; set; }
        [StringLength(250, ErrorMessage = "Email should be max {1} caracteres")]
        [EmailAddress]
        public string Email { get; set; }
        public int Code { get; set; }
        public string FormatNumber { get; set; }
    }
}

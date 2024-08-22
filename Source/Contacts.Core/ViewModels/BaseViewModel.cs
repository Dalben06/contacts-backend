using System.ComponentModel.DataAnnotations;

namespace Contacts.Core.ViewModels
{
    public abstract class BaseViewModel
    {
        [Key]
        public Guid UUId { get; set; }
        public int UserId { get; set; }
    }
}

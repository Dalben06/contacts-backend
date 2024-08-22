using Contacts.Core.DataAnnotations;

namespace Contacts.Core.Domain
{
    public abstract class AuditEntity : BaseEntity
    {
        [OnlyInsert]
        public DateTimeOffset CreateDate { get; set; }
        [OnlyInsert]
        public int CreateUserId { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        [OnlyUpdate]
        public bool IsDeleted { get; set; }
        [OnlyUpdate]
        public DateTimeOffset? DeleteDate { get; set; }
        [OnlyUpdate]
        public int? DeleteUserId { get; set; }


        public AuditEntity(int idUser) : base()
        {
            CreateDate = UpdateDate = DateTime.UtcNow;
            CreateUserId = UpdateUserId = idUser;
        }
    }
}

namespace Contacts.Core.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OnlyInsert : Attribute
    {
    }
}

namespace Domain.Entity;

public class DDD
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Region { get; set; }
    public string UF { get; set; }
    public ICollection<Contact> Contacts { get; } = new List<Contact>();
}
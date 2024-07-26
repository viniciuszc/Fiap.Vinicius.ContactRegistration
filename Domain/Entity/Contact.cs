using System.Text.Json.Serialization;

namespace Domain.Entity;

public class Contact
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public int DDDId { get; set; }

    [JsonIgnore]
    public virtual DDD DDD { get; set; } = null;

    public Contact() { }

    public Contact(int userId, string name, string phone, string email)
    {
        Name = name;
        Phone = phone;
        Email = email;
        UserId = userId;
    }
    public Contact(int userId, int id, string name, string phone, string email)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        UserId = userId;
    }
}
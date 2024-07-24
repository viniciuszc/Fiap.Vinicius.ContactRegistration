namespace Domain.Entity;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }

    public Contact(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }
}
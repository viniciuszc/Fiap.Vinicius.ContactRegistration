namespace Domain.Entity;

public class User
{
    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }
}

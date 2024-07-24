using Domain.Enums;

namespace Domain.Entity;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public TypePermission PermissaoSistema { get; set; }
}

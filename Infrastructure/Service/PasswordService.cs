using BCrypt.Net;
using Domain.Service;

namespace Infrastructure.Service;

public class PasswordService : IPasswordService
{
    public string Hash(string password) 
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);

    public bool Verify(string password, string hash)
        => BCrypt.Net.BCrypt.EnhancedVerify(password, hash, HashType.SHA256);
}
using Microsoft.EntityFrameworkCore;
using ETicaretweb.Controllers;
using ETicaretweb.Models;
using System.Text;
using System.Security.Cryptography;
public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
         using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha.ComputeHash(bytes);
        return Convert.ToHexString(hashBytes);
    }
    
}




using ETicaretweb.Models;
using Microsoft.EntityFrameworkCore;
namespace Models;
public class Admin
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "Admin";
}


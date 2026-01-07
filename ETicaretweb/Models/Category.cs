using ETicaretweb.Models;
using Microsoft.EntityFrameworkCore;
namespace Models;
public class Category
{
    public int id { get; set; }
    public string? CategoryName { get; set; }
    public string? URL { get; set; }
    public string? ImageUrl { get; set; }


    public ICollection<Product>? products { get; set; }

}
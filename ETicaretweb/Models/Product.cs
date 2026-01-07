using ETicaretweb.Models;
using Microsoft.EntityFrameworkCore;
namespace Models;
public class Product
{
    public int id { get; set; }
    public string ProductName { get; set; } = null!;
    public int Price { get; set; }
    public string? Description { get; set; }
    public Boolean isActive { get; set; } = true;
    public string? URL { get; set; }
     public int quantity { get; set; } = 1;
    public int CategoryId { get; set; }

    public Category? category { get; set; }
    public ProductImage? ProductImage  { get; set; }=null!;

}
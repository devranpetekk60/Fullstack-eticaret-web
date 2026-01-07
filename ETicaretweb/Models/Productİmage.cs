using ETicaretweb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Models;
public class ProductImage
{
    public int id { get; set; }
    public string? Url { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
}
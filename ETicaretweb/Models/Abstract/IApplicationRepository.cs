using Microsoft.EntityFrameworkCore;
using ETicaretweb.Controllers;
using ETicaretweb.Models;
namespace Models;
public interface IApplicationRepository
{
    IQueryable<Product> products { get; set; }
        IQueryable<ProductImage> productImages { get; set; }
        IQueryable<Admin> admins { get; set; }
    IQueryable<Category> categories { get; set; }

    // Create methods
    void CreateProduct(Product entity);
    void CreateCategory(Category category);
    void CreateProductImage(ProductImage productImage);
    void CreateAdmin(Admin admin);
    
    // Update methods
    void UpdateProduct(Product entity);
    void UpdateCategory(Category category);
    void UpdateAdmin(Admin admin);
    
    // Delete methods
    void DeleteProduct(int id);
    void DeleteCategory(int id);
    void DeleteProductImage(int id);
    void DeleteAdmin(int id);
}
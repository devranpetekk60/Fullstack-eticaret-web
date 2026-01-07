using Microsoft.EntityFrameworkCore;
using ETicaretweb.Models;
namespace Models;
public class EfApplicationRepository:IApplicationRepository
{
    private readonly ApplicationDbContext dbContext;
    public EfApplicationRepository(ApplicationDbContext _dbContext)
    {
        dbContext = _dbContext;
    }
    public IQueryable<Admin> admins => dbContext.admins;
    public IQueryable<Product> products => dbContext.products;
    public IQueryable<ProductImage> productImages => dbContext.productImages;
    public IQueryable<Category> categories => dbContext.categories;

    IQueryable<Product> IApplicationRepository.products { get => products; set => throw new NotImplementedException(); }
    IQueryable<ProductImage> IApplicationRepository.productImages { get => productImages; set => throw new NotImplementedException(); }
    IQueryable<Admin> IApplicationRepository.admins { get => admins; set => throw new NotImplementedException(); }
    IQueryable<Category> IApplicationRepository.categories { get => categories; set => throw new NotImplementedException(); }

    public void CreateProduct(Product entity)
    {
        if (string.IsNullOrWhiteSpace(entity.ProductName))
            throw new Exception("Ürün adı boş olamaz");
        dbContext.products.Add(entity);
        dbContext.SaveChanges();


    }
    public void CreateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.CategoryName))
        {
            throw new Exception("Kategori ismi boş olamaz");
        }
        dbContext.categories.Add(category);
        dbContext.SaveChanges();
    }
    public void CreateProductImage(ProductImage productImage)
    {
        dbContext.productImages.Add(productImage);
        dbContext.SaveChanges();
    }
    
    public void CreateAdmin(Admin admin)
    {
        if (string.IsNullOrWhiteSpace(admin.Username))
            throw new Exception("Kullanıcı adı boş olamaz");
        if (string.IsNullOrWhiteSpace(admin.PasswordHash))
            throw new Exception("Şifre boş olamaz");
        dbContext.admins.Add(admin);
        dbContext.SaveChanges();
    }
    
    public void UpdateProduct(Product entity)
    {
        if (string.IsNullOrWhiteSpace(entity.ProductName))
            throw new Exception("Ürün adı boş olamaz");
        dbContext.products.Update(entity);
        dbContext.SaveChanges();
    }
    
    public void UpdateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.CategoryName))
            throw new Exception("Kategori ismi boş olamaz");
        dbContext.categories.Update(category);
        dbContext.SaveChanges();
    }
    
    public void UpdateAdmin(Admin admin)
    {
        if (string.IsNullOrWhiteSpace(admin.Username))
            throw new Exception("Kullanıcı adı boş olamaz");
        dbContext.admins.Update(admin);
        dbContext.SaveChanges();
    }
    
    public void DeleteProduct(int id)
    {
        var product = dbContext.products.FirstOrDefault(p => p.id == id);
        if (product != null)
        {
            dbContext.products.Remove(product);
            dbContext.SaveChanges();
        }
    }
    
    public void DeleteCategory(int id)
    {
        var category = dbContext.categories.FirstOrDefault(c => c.id == id);
        if (category != null)
        {
            dbContext.categories.Remove(category);
            dbContext.SaveChanges();
        }
    }
    
    public void DeleteProductImage(int id)
    {
        var productImage = dbContext.productImages.FirstOrDefault(pi => pi.id == id);
        if (productImage != null)
        {
            dbContext.productImages.Remove(productImage);
            dbContext.SaveChanges();
        }
    }
    
    public void DeleteAdmin(int id)
    {
        var admin = dbContext.admins.FirstOrDefault(a => a.Id == id);
        if (admin != null)
        {
            dbContext.admins.Remove(admin);
            dbContext.SaveChanges();
        }
    }
}
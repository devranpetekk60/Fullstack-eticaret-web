using ETicaretweb.Models;
using Microsoft.EntityFrameworkCore;

namespace Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Admin> admins { get; set; }
    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<ProductImage> productImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Category>().HasData(
            new Category { id = 1, CategoryName = "Telefon", URL = "akilli-telefon", ImageUrl = "telefon.webp" },
            new Category { id = 2, CategoryName = "Kıyafetler", URL = "kiyafet", ImageUrl = "kiyafet.webp" },
            new Category { id = 3, CategoryName = "Aksesuar", URL = "aksesuar", ImageUrl = "aksesuar.jpg" }
        );

       modelBuilder.Entity<ProductImage>().HasData(
    new ProductImage { id = 1, Url = "/images/iphone15.jpg", ProductId = 1 },
    new ProductImage { id = 2, Url = "/images/iphone16.jpg", ProductId = 2 },
    new ProductImage { id = 3, Url = "/images/samsung-s24.jpg", ProductId = 3 }
);


        modelBuilder.Entity<Product>().HasData(
            new Product { id = 1, ProductName = "İphone15", URL = "iphone15", Price = 45000, CategoryId = 1},
            new Product { id = 2, ProductName = "İphone16", URL = "iphone16", Price = 55000, CategoryId = 1 },
            new Product { id = 3, ProductName = "Samsung Galaxy S24", URL = "samsung-s24", Price = 40000, CategoryId = 1 }
            // Diğer ürünler için ProductImageId ver
        );

        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1,
                Username = "Devran",
                PasswordHash = "67CD3F79C3A2B26A5F2BFDF1610E31452DB4FD4F60381CB317FF5C6F5E996DEF",
                Role = "Admin"
            }
        );

        modelBuilder.Entity<Product>()
    .HasOne(p => p.ProductImage)
    .WithOne(pi => pi.Product)
    .HasForeignKey<ProductImage>(pi => pi.ProductId);

    }
}

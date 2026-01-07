using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Models;
using ETicaretweb.Models;

namespace ETicaretweb.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IApplicationRepository _repository;
    private readonly IWebHostEnvironment _environment;

    public AdminController(IApplicationRepository repository, IWebHostEnvironment environment)
    {
        _repository = repository;
        _environment = environment;
    }



private async Task<string?> SaveImageAsync(IFormFile file, string folder)
{
    if (file == null || file.Length == 0)
        return null;

    var uploadsFolder = Path.Combine(
        Directory.GetCurrentDirectory(),
        "wwwroot",
        "images",
        folder
    );

    if (!Directory.Exists(uploadsFolder))
        Directory.CreateDirectory(uploadsFolder);

    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    var filePath = Path.Combine(uploadsFolder, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    return fileName; // SADECE dosya adı
}






    // Dashboard
    public async Task<IActionResult> Dashboard()
    {
        var productCount = await _repository.products.CountAsync();
        var categoryCount = await _repository.categories.CountAsync();
        var adminCount = await _repository.admins.CountAsync();
        var imageCount = await _repository.productImages.CountAsync();

        ViewBag.ProductCount = productCount;
        ViewBag.CategoryCount = categoryCount;
        ViewBag.AdminCount = adminCount;
        ViewBag.ImageCount = imageCount;

        return View();
    }

    public async Task<IActionResult> Products()
    {
        var products = await _repository.products
            .Include(p => p.category)
            .ToListAsync();
        return View("Products/Index", products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.Categories = await _repository.categories.ToListAsync();
        return View("Products/Create");
    }

    [HttpPost]
public async Task<IActionResult> CreateProduct(Product product, IFormFile? productImage)
{
    if (!ModelState.IsValid)
    {
        ViewBag.Categories = _repository.categories.ToList();
        return View("Products/Create", product);
    }

    if (productImage != null && productImage.Length > 0)
    {
        var fileName = await SaveImageAsync(productImage, "products");

        product.ProductImage = new ProductImage
        {
            Url = "/images/products/" + fileName
        };
    }

    _repository.CreateProduct(product); // TEK SAVE

    return RedirectToAction("Products");
}

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _repository.products.FirstOrDefaultAsync(p => p.id == id);
        if (product == null)
            return NotFound();
        
        ViewBag.Categories = await _repository.categories.ToListAsync();
        return View("Products/Edit", product);
    }

    [HttpPost]
public async Task<IActionResult> EditProduct(Product product, IFormFile? productImage)
{
    if (!ModelState.IsValid)
    {
        ViewBag.Categories = _repository.categories.ToList();
        return View("Products/Edit", product);
    }

    try
    {
        //  DB'den mevcut ürünü çek
        var existingProduct = _repository.products
            .FirstOrDefault(p => p.id == product.id);

        if (existingProduct == null)
            return NotFound();

        // Sadece güncellenecek alanları ata
        existingProduct.ProductName = product.ProductName;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.CategoryId = product.CategoryId;
        existingProduct.isActive = product.isActive;

        // Ürünü güncelle
        _repository.UpdateProduct(existingProduct);

        // Yeni ürün resmi yükle
        if (productImage != null && productImage.Length > 0)
        {
            var imageFileName = await SaveImageAsync(productImage, "");

            if (!string.IsNullOrEmpty(imageFileName))
            {
                var productImageEntity = new ProductImage
                {
                    ProductId = existingProduct.id,
                    Url = imageFileName
                };

                _repository.CreateProductImage(productImageEntity);
            }
        }

        return RedirectToAction("Products");
    }
    catch (Exception ex)
    {
        //  Asıl hatayı yakala
        var errorMessage = ex.InnerException?.Message ?? ex.Message;
        ModelState.AddModelError("", errorMessage);

        ViewBag.Categories = _repository.categories.ToList();
        return View("Products/Edit", product);
    }
}


    [HttpPost]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            _repository.DeleteProduct(id);
            return RedirectToAction("Products");
        }
        catch
        {
            return RedirectToAction("Products");
        }
    }

    // Categories Management
    public async Task<IActionResult> Categories()
    {
        var categories = await _repository.categories.ToListAsync();
        return View("Categories/Index", categories);
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View("Categories/Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category, IFormFile? categoryImage)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Kategori resmi yükle
                if (categoryImage != null && categoryImage.Length > 0)
                {
                    var imageFileName = await SaveImageAsync(categoryImage, "categories");
                    if (!string.IsNullOrEmpty(imageFileName))
                    {
                        category.ImageUrl = imageFileName;
                    }
                }
                
                _repository.CreateCategory(category);
                return RedirectToAction("Categories");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View("Categories/Create", category);
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _repository.categories.FirstOrDefaultAsync(c => c.id == id);
        if (category == null)
            return NotFound();
        
        return View("Categories/Edit", category);
    }

   [HttpPost]
public async Task<IActionResult> EditCategory(Category model, IFormFile? categoryImage)
{
    var existingCategory = await _repository.categories
        .FirstOrDefaultAsync(c => c.id == model.id);

    if (existingCategory == null)
        return NotFound();

    // Alanları güncelle
    existingCategory.CategoryName = model.CategoryName;
    existingCategory.URL = model.URL;

    // Resim varsa güncelle
    if (categoryImage != null && categoryImage.Length > 0)
    {
        var imageFileName = await SaveImageAsync(categoryImage, "categories");
        existingCategory.ImageUrl = imageFileName;
    }

    // ❗ UpdateCategory çağırma
    // ❗ SaveChanges repository içinde zaten var
    _repository.UpdateCategory(existingCategory);

    return RedirectToAction("Categories");
}

    [HttpPost]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            _repository.DeleteCategory(id);
            return RedirectToAction("Categories");
        }
        catch
        {
            return RedirectToAction("Categories");
        }
    }

    // Images Management
    public async Task<IActionResult> ProductImages()
    {
        var images = await _repository.productImages
            .Include(pi => pi.Product)
            .ToListAsync();
        return View("ProductImages/Index", images);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProductImage()
    {
        ViewBag.Products = await _repository.products.ToListAsync();
        return View("ProductImages/Create");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductImage(int ProductId, IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            ModelState.AddModelError("", "Lütfen bir resim seçin.");
            ViewBag.Products = _repository.products.ToList();
            return View("ProductImages/Create");
        }

        try
        {
            var imageFileName = await SaveImageAsync(imageFile, "");
            if (!string.IsNullOrEmpty(imageFileName))
            {
                var productImage = new ProductImage
                {
                    ProductId = ProductId,
                    Url = imageFileName
                };
                _repository.CreateProductImage(productImage);
                return RedirectToAction("ProductImages");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        
        ViewBag.Products = _repository.products.ToList();
        return View("ProductImages/Create");
    }

    [HttpPost]
    public IActionResult DeleteProductImage(int id)
    {
        try
        {
            _repository.DeleteProductImage(id);
            return RedirectToAction("ProductImages");
        }
        catch
        {
            return RedirectToAction("ProductImages");
        }
    }

    // Admins Management
    public async Task<IActionResult> Admins()
    {
        var admins = await _repository.admins.ToListAsync();
        return View("Admins/Index", admins);
    }

    [HttpGet]
    public IActionResult CreateAdmin()
    {
        return View("Admins/Create");
    }

    [HttpPost]
    public IActionResult CreateAdmin(Admin admin, string Password)
    {
        if (ModelState.IsValid)
        {
            try
            {
                admin.PasswordHash = PasswordHasher.HashPassword(Password);
                if (string.IsNullOrWhiteSpace(admin.Role))
                    admin.Role = "Admin";
                _repository.CreateAdmin(admin);
                return RedirectToAction("Admins");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View("Admins/Create", admin);
    }

    [HttpGet]
    public async Task<IActionResult> EditAdmin(int id)
    {
        var admin = await _repository.admins.FirstOrDefaultAsync(a => a.Id == id);
        if (admin == null)
            return NotFound();
        
        return View("Admins/Edit", admin);
    }

    [HttpPost]
    public async Task<IActionResult> EditAdmin(Admin admin, string? NewPassword)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var existingAdmin = await _repository.admins.FirstOrDefaultAsync(a => a.Id == admin.Id);
                if (existingAdmin == null)
                    return NotFound();
                
                if (!string.IsNullOrWhiteSpace(NewPassword))
                {
                    admin.PasswordHash = PasswordHasher.HashPassword(NewPassword);
                }
                else
                {
                    admin.PasswordHash = existingAdmin.PasswordHash;
                }
                _repository.UpdateAdmin(admin);
                return RedirectToAction("Admins");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View("Admins/Edit", admin);
    }

    [HttpPost]
    public IActionResult DeleteAdmin(int id)
    {
        try
        {
            _repository.DeleteAdmin(id);
            return RedirectToAction("Admins");
        }
        catch
        {
            return RedirectToAction("Admins");
        }
    }
}
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ETicaretweb.Models;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ETicaretweb.Controllers;

public class HomeController : Controller
{
    private readonly IApplicationRepository _repository;
    public HomeController(IApplicationRepository repository)
    {
        _repository = repository;
    }


    public IActionResult Index()
    {
        var category = _repository.categories.ToList();
        return View(category);
    }
    public async Task<IActionResult> Category(int id)
{
    var products = await _repository.products
        .Include(p => p.ProductImage)   // RESİM YÜKLENİYO
        .Where(p => p.CategoryId == id)
        .ToListAsync();

    ViewBag.CategoryName = await _repository.categories
        .Where(c => c.id == id)
        .Select(c => c.CategoryName)
        .FirstOrDefaultAsync();

    return View(products);
}

    
  public async Task<IActionResult> Search(string products)
{
    if (string.IsNullOrWhiteSpace(products))
        return View(await _repository.products.ToListAsync());

    string query = products.Trim();

    var allProducts = await _repository.products.Include(p=>p.ProductImage).ToListAsync();

    var result = allProducts
        .Where(p => p.ProductName.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0)
        .ToList();

    return View(result);
}

[HttpGet]
public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
public async Task<IActionResult> Login(string Username,string PasswordHash)
    {
         var hashedInput = PasswordHasher.HashPassword(PasswordHash);
        var admin = await _repository.admins.FirstOrDefaultAsync(a=>a.Username==Username && a.PasswordHash==hashedInput);
        if (admin==null)
        {
            return View("Login");
        }
        var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, admin.Username),
    new Claim(ClaimTypes.Role, admin.Role ?? "Admin")
};
var identity = new ClaimsIdentity(claims, "Cookies");
var principal = new ClaimsPrincipal(identity);

await HttpContext.SignInAsync("Cookies", principal);

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Index");
    }
public IActionResult Contact()
{
    return View();
}
public async Task<IActionResult> Details(int id)
{
    var result = await _repository.products
        .Include(p => p.ProductImage) 
        .FirstOrDefaultAsync(p => p.id == id);

    if (result == null)
        return NotFound();

    return View(result);
}

    [HttpGet]
public IActionResult Sepet()
{
   
    return View();
}
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}

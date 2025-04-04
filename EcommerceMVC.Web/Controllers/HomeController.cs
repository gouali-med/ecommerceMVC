using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Web.Models;
using EcommerceMVC.Web.Services;

namespace EcommerceMVC.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CartService _cartService;
    private static List<Product> _products = new List<Product>
    {
        new Product
        {
            Id = 1,
            Name = "iPhone 13 Pro",
            Description = "Le dernier iPhone avec une caméra professionnelle",
            Price = 999.99m,
            ImageUrl = "/images/iphone13.jpg",
            Category = "Smartphones",
            StockQuantity = 10,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-5)
        },
        new Product
        {
            Id = 2,
            Name = "MacBook Pro M1",
            Description = "Ordinateur portable puissant avec puce M1",
            Price = 1299.99m,
            ImageUrl = "/images/macbook.jpg",
            Category = "Ordinateurs",
            StockQuantity = 5,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-3)
        },
        new Product
        {
            Id = 3,
            Name = "iPad Pro 12.9",
            Description = "Tablette professionnelle avec écran Retina",
            Price = 799.99m,
            ImageUrl = "/images/ipad.jpg",
            Category = "Tablettes",
            StockQuantity = 8,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-1)
        },
        new Product
        {
            Id = 4,
            Name = "Samsung Galaxy S21",
            Description = "Smartphone Android haut de gamme",
            Price = 899.99m,
            ImageUrl = "/images/samsung.jpg",
            Category = "Smartphones",
            StockQuantity = 15,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-7)
        },
        new Product
        {
            Id = 5,
            Name = "Dell XPS 13",
            Description = "Ordinateur portable ultra-léger",
            Price = 1099.99m,
            ImageUrl = "/images/dell.jpg",
            Category = "Ordinateurs",
            StockQuantity = 7,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-2)
        },
        new Product
        {
            Id = 6,
            Name = "AirPods Pro",
            Description = "Écouteurs sans fil avec réduction de bruit",
            Price = 249.99m,
            ImageUrl = "/images/airpods.jpg",
            Category = "Accessoires",
            StockQuantity = 20,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-4)
        },
        new Product
        {
            Id = 7,
            Name = "Apple Watch Series 7",
            Description = "Montre connectée dernière génération",
            Price = 399.99m,
            ImageUrl = "/images/watch.jpg",
            Category = "Accessoires",
            StockQuantity = 12,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-6)
        },
        new Product
        {
            Id = 8,
            Name = "Surface Pro 8",
            Description = "Tablette hybride Windows",
            Price = 999.99m,
            ImageUrl = "/images/surface.jpg",
            Category = "Tablettes",
            StockQuantity = 6,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-8)
        },
        new Product
        {
            Id = 9,
            Name = "Sony WH-1000XM4",
            Description = "Casque audio premium avec réduction de bruit",
            Price = 349.99m,
            ImageUrl = "/images/sony.jpg",
            Category = "Accessoires",
            StockQuantity = 9,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-9)
        },
        new Product
        {
            Id = 10,
            Name = "LG OLED C1",
            Description = "Téléviseur 4K avec technologie OLED",
            Price = 1999.99m,
            ImageUrl = "/images/lg.jpg",
            Category = "Téléviseurs",
            StockQuantity = 4,
            IsAvailable = true,
            CreatedAt = DateTime.Now.AddDays(-10)
        }
    };

    public HomeController(ILogger<HomeController> logger, CartService cartService)
    {
        _logger = logger;
        _cartService = cartService;
    }

    public IActionResult Index(string searchString, string category, string sortBy)
    {
        var products = _products.AsQueryable();

        // Filtre par recherche
        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            products = products.Where(p => 
                p.Name.ToLower().Contains(searchString) || 
                p.Description.ToLower().Contains(searchString));
        }

        // Filtre par catégorie
        if (!string.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.Category == category);
        }

        // Tri
        switch (sortBy)
        {
            case "price_asc":
                products = products.OrderBy(p => p.Price);
                break;
            case "price_desc":
                products = products.OrderByDescending(p => p.Price);
                break;
            case "newest":
                products = products.OrderByDescending(p => p.CreatedAt);
                break;
            default:
                products = products.OrderByDescending(p => p.CreatedAt);
                break;
        }

        // Récupérer les catégories uniques pour le filtre
        var categories = _products.Select(p => p.Category).Distinct().ToList();

        // Récupérer les derniers produits
        var latestProducts = _products.OrderByDescending(p => p.CreatedAt).Take(4).ToList();

        var viewModel = new HomeViewModel
        {
            Products = products.ToList(),
            Categories = categories,
            LatestProducts = latestProducts,
            SearchString = searchString,
            SelectedCategory = category,
            SortBy = sortBy
        };

        return View(viewModel);
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

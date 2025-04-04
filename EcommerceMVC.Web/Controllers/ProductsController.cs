using Microsoft.AspNetCore.Mvc;
using EcommerceMVC.Web.Models;
using EcommerceMVC.Web.Services;
using System.Collections.Generic;

namespace EcommerceMVC.Web.Controllers
{
    public class ProductsController : Controller
    {
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
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
                IsAvailable = true
            }
        };

        public ProductsController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(_products);
        }

        public IActionResult Details(int id)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult Category(string category)
        {
            var products = _products.Where(p => p.Category == category).ToList();
            return View("Index", products);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _cartService.AddItem(product, quantity);
            return Json(new { success = true, message = "Produit ajouté au panier" });
        }

        public IActionResult Cart()
        {
            var cartItems = _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveItem(id);
            return Json(new { success = true, message = "Produit retiré du panier" });
        }

        [HttpPost]
        public IActionResult UpdateCartQuantity(int id, int quantity)
        {
            _cartService.UpdateQuantity(id, quantity);
            return Json(new { success = true, message = "Quantité mise à jour" });
        }
    }
} 
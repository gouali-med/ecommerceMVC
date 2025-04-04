using System.Collections.Generic;
using System.Text.Json;
using EcommerceMVC.Web.Models;
using Microsoft.AspNetCore.Http;

namespace EcommerceMVC.Web.Services
{
    public class CartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartItems";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private List<CartItem> GetCartItemsFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        private void SaveCartItemsToSession(List<CartItem> items)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonSerializer.Serialize(items);
            session.SetString(CartSessionKey, cartJson);
        }

        public void AddItem(Product product, int quantity = 1)
        {
            var cartItems = GetCartItemsFromSession();
            var existingItem = cartItems.Find(item => item.Id == product.Id);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = quantity
                });
            }
            
            SaveCartItemsToSession(cartItems);
        }

        public void RemoveItem(int productId)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.Find(item => item.Id == productId);
            if (item != null)
            {
                cartItems.Remove(item);
                SaveCartItemsToSession(cartItems);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cartItems = GetCartItemsFromSession();
            var item = cartItems.Find(item => item.Id == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                SaveCartItemsToSession(cartItems);
            }
        }

        public List<CartItem> GetCartItems()
        {
            return GetCartItemsFromSession();
        }

        public decimal GetTotal()
        {
            return GetCartItemsFromSession().Sum(item => item.Total);
        }

        public void ClearCart()
        {
            SaveCartItemsToSession(new List<CartItem>());
        }
    }
} 
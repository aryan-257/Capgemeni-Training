using Ecommerce_Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecommerce_Inventory.Controllers
{
    public class ProductsController : Controller
    {
        private static List<Product> _products = new()
        {
            // Men's Clothing
            new Product { Id = 1, Name = "Men's Classic T-Shirt", Description = "Comfortable cotton t-shirt", Price = 29.99m, Category = "Men", ImageUrl = "https://via.placeholder.com/300x400?text=Men+T-Shirt", Stock = 50 },
            new Product { Id = 2, Name = "Men's Denim Jeans", Description = "Stylish blue denim jeans", Price = 59.99m, Category = "Men", ImageUrl = "https://via.placeholder.com/300x400?text=Men+Jeans", Stock = 30 },
            new Product { Id = 3, Name = "Men's Formal Shirt", Description = "Professional formal shirt", Price = 49.99m, Category = "Men", ImageUrl = "https://via.placeholder.com/300x400?text=Men+Shirt", Stock = 40 },
            
            // Women's Clothing
            new Product { Id = 4, Name = "Women's Summer Dress", Description = "Light and breezy summer dress", Price = 69.99m, Category = "Women", ImageUrl = "https://via.placeholder.com/300x400?text=Women+Dress", Stock = 25 },
            new Product { Id = 5, Name = "Women's Casual Top", Description = "Trendy casual top", Price = 34.99m, Category = "Women", ImageUrl = "https://via.placeholder.com/300x400?text=Women+Top", Stock = 45 },
            new Product { Id = 6, Name = "Women's Skinny Jeans", Description = "Comfortable skinny fit jeans", Price = 54.99m, Category = "Women", ImageUrl = "https://via.placeholder.com/300x400?text=Women+Jeans", Stock = 35 },
            
            // Kids' Clothing
            new Product { Id = 7, Name = "Kids' Graphic T-Shirt", Description = "Fun graphic print t-shirt", Price = 19.99m, Category = "Kids", ImageUrl = "https://via.placeholder.com/300x400?text=Kids+T-Shirt", Stock = 60 },
            new Product { Id = 8, Name = "Kids' Shorts", Description = "Comfortable play shorts", Price = 24.99m, Category = "Kids", ImageUrl = "https://via.placeholder.com/300x400?text=Kids+Shorts", Stock = 55 },
            new Product { Id = 9, Name = "Kids' Hoodie", Description = "Warm and cozy hoodie", Price = 39.99m, Category = "Kids", ImageUrl = "https://via.placeholder.com/300x400?text=Kids+Hoodie", Stock = 40 }
        };

        public IActionResult Index(string category = "All")
        {
            ViewBag.Category = category;
            var products = category == "All" 
                ? _products 
                : _products.Where(p => p.Category == category).ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return NotFound();

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                if (quantity > 0)
                    item.Quantity = quantity;
                else
                    cart.Remove(item);
            }

            SaveCart(cart);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
                cart.Remove(item);

            SaveCart(cart);
            return RedirectToAction("Cart");
        }

        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Cart");

            return View(cart);
        }

        [HttpPost]
        public IActionResult ProcessOrder()
        {
            var cart = GetCart();
            // Here you would process the payment and create order
            // For now, we'll just clear the cart
            HttpContext.Session.Remove("Cart");
            TempData["OrderSuccess"] = "Your order has been placed successfully!";
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson) 
                ? new List<CartItem>() 
                : JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}

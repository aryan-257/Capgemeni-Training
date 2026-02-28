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
            new Product { Id = 1, Name = "Men's Classic T-Shirt", Description = "Comfortable cotton t-shirt", Price = 2499.00m, Category = "Men", ImageUrl = "/Images/men-classic-tshirt.jpg", Stock = 50 },
            new Product { Id = 2, Name = "Men's Denim Jeans", Description = "Stylish blue denim jeans", Price = 4999.00m, Category = "Men", ImageUrl = "/Images/men-dennim-jeans.jpg", Stock = 30 },
            new Product { Id = 3, Name = "Men's Formal Shirt", Description = "Professional formal shirt", Price = 4149.00m, Category = "Men", ImageUrl = "/Images/men-shirt.jpg", Stock = 40 },
            
            // Women's Clothing
            new Product { Id = 4, Name = "Women's Summer Dress", Description = "Light and breezy summer dress", Price = 5799.00m, Category = "Women", ImageUrl = "/Images/women-dress.jpg", Stock = 25 },
            new Product { Id = 5, Name = "Women's Casual Top", Description = "Trendy casual top", Price = 2899.00m, Category = "Women", ImageUrl = "/Images/women-top.jpg", Stock = 45 },
            new Product { Id = 6, Name = "Women's Skinny Jeans", Description = "Comfortable skinny fit jeans", Price = 4599.00m, Category = "Women", ImageUrl = "/Images/women-jeans.jpg", Stock = 35 },
            
            // Kids' Clothing
            new Product { Id = 7, Name = "Kids' Graphic T-Shirt", Description = "Fun graphic print t-shirt", Price = 1659.00m, Category = "Kids", ImageUrl = "/Images/kids-tshirt.jpg", Stock = 60 },
            new Product { Id = 8, Name = "Kids' Shorts", Description = "Comfortable play shorts", Price = 2099.00m, Category = "Kids", ImageUrl = "/Images/kids-shorts.jpg", Stock = 55 },
            new Product { Id = 9, Name = "Kids' Hoodie", Description = "Warm and cozy hoodie", Price = 3319.00m, Category = "Kids", ImageUrl = "/Images/kids-hoodie.jpg", Stock = 40 }
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

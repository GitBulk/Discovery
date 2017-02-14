using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureCoreOne.Models.ProBook;
using AzureCoreOne.Models.ProBook.Infrastructure;
using AzureCoreOne.Models.ProBook.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repo;
        public CartController(IProductRepository repo)
        {
            this.repo = repo;
        }

        // GET: /<controller>/
        public IActionResult Index(string returnUrl)
        {
            string verifiedUrl = "/";
            if (Url.IsLocalUrl(returnUrl))
            {
                verifiedUrl = returnUrl;
            }
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = verifiedUrl
            });
        }

        public RedirectToActionResult RemoveFromCart(int productId, 
            string returnUrl)
        {
            Product product = this.repo.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        
        public IActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = this.repo.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
    }
}

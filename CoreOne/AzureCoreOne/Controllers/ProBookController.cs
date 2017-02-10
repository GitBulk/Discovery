using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureCoreOne.Models.ProBook;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    public class ProBookController : Controller
    {
        private bool FilterByPrice(Product p)
        {
            return (p?.Price ?? 0) >= 20;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //var results = new List<string>();
            //foreach (var item in Product.GetProducts())
            //{
            //    string name = item?.Name;
            //    decimal? price = item?.Price;
            //    string relatedName = item?.Related?.Name;
            //    results.Add($"Name: { name }, Price: { price }, Related: { relatedName }");
            //}
            //return View(results);
            ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };
            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            //Func<Product, bool> nameFilter = delegate (Product prod)
            //{
            //    return prod?.Name?[0] == 'S';
            //};

            //decimal priceFilterTotal = productArray.FilterByPrice(20).TotalPrices();
            //decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();
            //return View("Index", new string[] {
            //    $"Price Total: {priceFilterTotal:C2}",
            //    $"Name Total: {nameFilterTotal:C2}" });


            Func<Product, bool> nameFilter = delegate (Product prod)
            {
                return prod?.Name?[0] == 'S';
            };

            decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();
            return View("Index", new string[] {
                $"Price Total: {priceFilterTotal:C2}",
                $"Name Total: {nameFilterTotal:C2}" });

        }

        public async Task<ViewResult> DoTask()
        {
            long? length = await MyAsyncMethods.GetPageLength();
            return View(new string[] { $"Length: {length}" });
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureCoreOne.Models.ProBook;
using AzureCoreOne.Models.ProBook.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    public class ProBookController : Controller
    {
        private readonly IProductRepository repository;
        //SimpleRepository repo = SimpleRepository.SharedRepository;
        public int PageSize = 4;
        public ProBookController(IProductRepository productRepo)
        {
            this.repository = productRepo;
        }

        private bool FilterByPrice(Product p)
        {
            return (p?.Price ?? 0) >= 20;
        }

        public ViewResult List(string category, int page = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });

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
            //ShoppingCart cart = new ShoppingCart { Products = Product.GetProducts() };
            //Product[] productArray = {
            //    new Product {Name = "Kayak", Price = 275M},
            //    new Product {Name = "Lifejacket", Price = 48.95M},
            //    new Product {Name = "Soccer ball", Price = 19.50M},
            //    new Product {Name = "Corner flag", Price = 34.95M}
            //};

            //Func<Product, bool> nameFilter = delegate (Product prod)
            //{
            //    return prod?.Name?[0] == 'S';
            //};

            //decimal priceFilterTotal = productArray.FilterByPrice(20).TotalPrices();
            //decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();
            //return View("Index", new string[] {
            //    $"Price Total: {priceFilterTotal:C2}",
            //    $"Name Total: {nameFilterTotal:C2}" });


            //Func<Product, bool> nameFilter = delegate (Product prod)
            //{
            //    return prod?.Name?[0] == 'S';
            //};

            //decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            //decimal nameFilterTotal = productArray.Filter(nameFilter).TotalPrices();
            //return View("Index", new string[] {
            //    $"Price Total: {priceFilterTotal:C2}",
            //    $"Name Total: {nameFilterTotal:C2}" });

            //return View(repo.Products.Where(p => p?.Price < 50));
            return View(repository.Products);
        }

        public IActionResult AddProduct()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            //repo.AddProduct(p);
            return RedirectToAction("Index");
        }

        public async Task<ViewResult> DoTask()
        {
            long? length = await MyAsyncMethods.GetPageLength();
            return View(new string[] { $"Length: {length}" });
        }

        public ViewResult One()
        {
            ViewBag.StockLevel = 2;
            return View(new Product
            {
                ProductId = 1,
                Name = "Kayak",
                Description = "A boat for one person",
                Category = "Watersports",
                Price = 275M
            });
        }

        public ViewResult Many()
        {
            return View(SimpleRepository.SharedRepository.Products.Where(p => p.Price < 50));
        }
    }
}

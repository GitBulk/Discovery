using AzureCoreOne.Models.ProBook;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AzureCoreOne.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository repo;
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            this.repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(this.repo.Products.Select(s => s.Category).Distinct().OrderBy(s => s));
        }
    }
}

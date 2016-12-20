using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    public class VuewController : Controller
    {
        // GET: /<controller>/
        public ViewResult OrderForm()
        {
            return View();
        }

        public ViewResult NavigationMenu()
        {
            return View();
        }

        public ViewResult InlineEditor()
        {
            return View();
        }

        public ViewResult InstantSearch()
        {
            return View();
        }

        public ViewResult SwitchableGrid()
        {
            return View();
        }

        public ViewResult Event()
        {
            return View();
        }
    }
}

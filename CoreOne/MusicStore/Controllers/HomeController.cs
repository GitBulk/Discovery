using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MusicStore.Models;
using MediatR;
using MusicStore.Features;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        //
        // GET: /Home/
        public async Task<IActionResult> Index()
        {
            var albums = new List<Album>();
            albums = await mediator.Send(new HomeIndexRequest());
            return View(albums);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult StatusCodePage()
        {
            return View("~/Views/Shared/StatusCodePage.cshtml");
        }

        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        private Task<List<Album>> GetTopSellingAlbumsAsync(MusicStoreContext dbContext, int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return dbContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
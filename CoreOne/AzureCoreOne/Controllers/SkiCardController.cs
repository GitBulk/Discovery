using AzureCoreOne.AppContexts;
using AzureCoreOne.Models.Indentities;
using AzureCoreOne.Models.Parsley;
using AzureCoreOne.ViewModels.Parsley;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tam.Core.Controllers;
using Tam.Core.Utilities;

namespace AzureCoreOne.Controllers
{
    [Authorize]
    public class SkiCardController : BaseController
    {
        private readonly TamContext context;

        private readonly UserManager<ApplicationUser> UserManager;

        public SkiCardController(TamContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.UserManager = userManager;
        }

        public async Task<ViewResult> Index()
        {
            string userId = this.UserManager.GetUserId(this.User);
            var skicardsViewModels = await context.SkiCards
                                            .Where(a => a.ApplicationUserId == userId).
                                            Select(s => new SkiCardListViewModel
                                            {
                                                Id = s.Id,
                                                CardHolderName = s.CardHolderFirstName + " " + s.CardHolderLastName
                                            }).ToListAsync();
            return View(skicardsViewModels);
        }

        public async Task<ViewResult> Create()
        {
            string userId = this.UserManager.GetUserId(this.User);
            var currentUser = await this.UserManager.FindByIdAsync(userId);

            var viewModel = new CreateSkiCardViewModel
            {
                CardHolderPhoneNumber = currentUser.PhoneNumber
            };
            // if this is the user's first card, auto-populate the name properties since
            // this card is most likely for that user. Otherwisr assume the card is for a
            var hasExistingSkiCards = this.context.SkiCards.AnyOrNull(s => s.ApplicationUserId == userId);
            if (!hasExistingSkiCards)
            {
                viewModel.CardHolderFirstName = currentUser.FirstName;
                viewModel.CardHolderLastName = currentUser.LastName;
            }
            return View(viewModel);
        }


        public async Task<ViewResult> Create(CreateSkiCardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = this.UserManager.GetUserId(this.User);
                var skiCard = new SkiCard
                {
                    ApplicationUserId = userId,
                    CreatedDate = DateTime.UtcNow
                };
            }
        }
    }
}

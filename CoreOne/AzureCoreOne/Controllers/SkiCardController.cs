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


        public async Task<ActionResult> Create(CreateSkiCardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = this.UserManager.GetUserId(this.User);
                var skiCard = new SkiCard
                {
                    ApplicationUserId = userId,
                    CreatedDate = DateTimeHelper.GetCurrentSystemDate(),
                    CardHolderFirstName = viewModel.CardHolderFirstName,
                    CardHolderBirthDate = viewModel.CardHolderBirthDate,
                    CardHolderLastName = viewModel.CardHolderLastName,
                    CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber
                };
                this.context.SkiCards.Add(skiCard);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            string userId = this.UserManager.GetUserId(this.User);
            var skiCard = await this.context.SkiCards.Where(s => s.ApplicationUserId == userId && s.Id == id)
                .Select(s => new EditSkiCardViewModel
                {
                    Id = s.Id,
                    CardHolderBirthDate = s.CardHolderBirthDate,
                    CardHolderFirstName = s.CardHolderFirstName,
                    CardHolderLastName = s.CardHolderLastName
                }).SingleOrDefaultAsync();
            if (skiCard == null)
            {
                return NotFound();
            }
            return View(skiCard);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSkiCardViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = this.UserManager.GetUserId(this.User);
                var skiCard = await this.context.SkiCards.SingleOrDefaultAsync(s => s.ApplicationUserId == userId && s.Id == viewModel.Id);
                if (skiCard == null)
                {
                    return NotFound();
                }
                skiCard.CardHolderBirthDate = viewModel.CardHolderBirthDate.Value.Date;
                skiCard.CardHolderFirstName = viewModel.CardHolderFirstName;
                skiCard.CardHolderLastName = viewModel.CardHolderLastName;
                skiCard.CardHolderPhoneNumber = viewModel.CardHolderPhoneNumber;
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}

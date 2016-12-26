using AzureCoreOne.AppContexts;
using AzureCoreOne.ViewModels.Parsley;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tam.Core.Utilities;

namespace AzureCoreOne.Models.Parsley
{
    public class AddSkiPassOnPurchasedCompleted : INotificationHandler<PurchaseCompleted>
    {
        private readonly TamContext context;
        private readonly IMediator bus;

        public AddSkiPassOnPurchasedCompleted(TamContext context, IMediator bus)
        {
            this.context = context;
            this.bus = bus;
        }

        public void Handle(PurchaseCompleted notification)
        {
            var newPasses = new List<Pass>();
            foreach (var item in notification.Passes)
            {
                var pass = new Pass
                {
                    CardId = item.CardId,
                    CreatedDate = DateTimeHelper.GetCurrentSystemDate(),
                    PassTypeId = item.PassTypeId
                };
                newPasses.Add(pass);
            }
            this.context.Passes.AddRange(newPasses);
            this.context.SaveChanges();
            foreach (var item in newPasses)
            {
                var passAddedEvent = new PassAdded
                {
                    PassId = item.Id,
                    PassTypeId = item.PassTypeId,
                    CardId = item.CardId,
                    CreatedDate = item.CreatedDate
                };
                bus.Publish(passAddedEvent);
            }
        }
    }
}

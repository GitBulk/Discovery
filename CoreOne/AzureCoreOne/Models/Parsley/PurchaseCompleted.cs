using MediatR;
using System;
using System.Collections.Generic;

namespace AzureCoreOne.Models.Parsley
{
    public class PurchaseCompleted : INotification
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string TransactionId { get; set; }
        public decimal TotalCosr { get; set; }
        public List<PassPurchased> Passes { get; set; }
    }
}

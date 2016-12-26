using MediatR;
using System;

namespace AzureCoreOne.Models.Parsley
{
    public class PassAdded : INotification
    {
        public int PassId { get; set; }
        public int PassTypeId { get; set; }
        public int CardId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

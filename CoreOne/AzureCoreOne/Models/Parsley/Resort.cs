using System;

namespace AzureCoreOne.Models.Parsley
{
    public class Resort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailingAddress { get; set; }
        public DateTime OperatingSince { get; set; }
    }
}

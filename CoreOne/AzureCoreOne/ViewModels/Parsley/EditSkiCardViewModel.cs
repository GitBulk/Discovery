using System;

namespace AzureCoreOne.ViewModels.Parsley
{
    public class EditSkiCardViewModel
    {
        public int Id { get; set; }
        public string CardHolderFirstName { get; set; }
        public string CardHolderLastName { get; set; }
        public DateTime? CardHolderBirthDate { get; set; }
        public string CardHolderPhoneNumber { get; set; }
    }
}

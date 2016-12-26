using System;
using System.ComponentModel.DataAnnotations;

namespace AzureCoreOne.Models.Parsley
{
    public class SkiCard
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(70)]
        public string CardHolderFirstName { get; set; }

        [Required]
        [MaxLength(70)]
        public string CardHolderLastName { get; set; }
        public DateTime CardHolderBirthDate { get; set; }

        public string CardHolderPhoneNumber { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AzureCoreOne.Models.Indentities
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(70)]
        public string FirstName { get; set; }

        [MaxLength(70)]
        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
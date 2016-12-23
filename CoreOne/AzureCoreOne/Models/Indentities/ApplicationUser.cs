using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.Models.Indentities
{
    public class ApplicationUser : IdentityUser
    {
        public string Description { get; set; }

    }
}
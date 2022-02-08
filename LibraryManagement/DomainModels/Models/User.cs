using System;
using Microsoft.AspNetCore.Identity;

namespace DomainModels.Models
{
    public class User : IdentityUser
    {
        public DateTime StartDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

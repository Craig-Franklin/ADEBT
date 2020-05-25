using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADEBT.Api.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public List<Debt> Debts { get; set; }
    }
}

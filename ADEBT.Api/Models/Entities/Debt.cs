using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADEBT.Api.Models.Entities
{
    public class Debt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal InitialAmount { get; set; }
        public User User { get; set; }
        
    }
}

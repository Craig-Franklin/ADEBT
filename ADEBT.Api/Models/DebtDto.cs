using System.ComponentModel.DataAnnotations;

namespace ADEBT.Api.Models
{
    public class DebtDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal InitialAmount { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace CRUDAPI.Models
{
    public class Produto
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }

    }
}
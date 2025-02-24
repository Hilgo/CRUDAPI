using System.Runtime.ConstrainedExecution;

namespace CRUDAPI.Models
{
    public class Produto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

    }
}
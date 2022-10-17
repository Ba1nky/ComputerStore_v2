using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        [Required(ErrorMessage = "Please enter a person")]
        public string Person { get; set; } = String.Empty;

        [Required(ErrorMessage = "Please enter an Address")]
        public string Address { get; set; } = String.Empty;

        public DateTime Date { get; set; } = DateTime.Now;


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

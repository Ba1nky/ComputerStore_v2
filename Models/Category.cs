using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerStore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Name { get; set; } = String.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}

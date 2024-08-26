using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cloth_corner.Models
{
    public class CartDetails
    {
        [Key]
        public required int CartDetailsId { get; set; }

        [ForeignKey("Products")]
        public required int ProductId { get; set; }
        public Products? Products { get; set; }

        public required int Quantity { get; set; }

        [Precision(18, 2)]
        public required decimal Price { get; set; }

        [ForeignKey("Cart")]
        public required int CartId { get; set; }
        public Cart? Cart { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Commerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage="Quantity is required.")]
        [Range(0, Double.PositiveInfinity)]
        public int Quantity { get; set; }

        public int UserId { get; set; }
        public User Customer { get; set; }

        public int ProductId { get; set; }
        public Product OrderedProduct { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

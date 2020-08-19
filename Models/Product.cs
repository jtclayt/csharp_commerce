using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage="Name is required.")]
        [MinLength(2, ErrorMessage="Name must be 2 or more characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage="Image URL is required.")]
        [Url]
        [DataType(DataType.ImageUrl)]
        [Display(Name="Image URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage="Description is requried.")]
        [MinLength(5, ErrorMessage="Description must be 5 or more characters.")]
        [Display(Name="Description")]
        public string Desc { get; set; }

        [Required(ErrorMessage="Quantity is required.")]
        [Range(0, Double.PositiveInfinity)]
        public int? Quantity { get; set;}

        public int UserId { get; set; }
        public User Creator { get; set; }

        public List<Order> Orders { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

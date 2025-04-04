using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsAvailable { get; set; } = true;
    }
} 
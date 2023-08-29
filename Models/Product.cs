using Bangazon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Product
    {
        
        [Key]
        public int Id { get; set; }
        public List<User> Users { get; set; } 
        [ForeignKey("UserId")]
        public string SellerId { get; set; }
       
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public bool InStock { get; set; }
        public string? ImageUrl { get; set; }
        public List<Ordered_Products> ordered_Products { get; set; }
        public ProductType ProductType { get; set; }
    }
}

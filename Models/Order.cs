using Bangazon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public List<User> Users { get; set; }
        [ForeignKey("UserId")]
        public string? CustomerId { get; set; }

        public string? PaymentType { get; set; }

        public DateTime? DatePlaced { get; set; }
        public DateTime? DateShipped { get; set; }
        public List<Ordered_Products> ordered_Products { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

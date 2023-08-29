using Bangazon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    public class UserPaymentType
    {
        [Required]
        public int Id { get; set; }
        [Key]
        public string? UserId { get; set; }
        public int PaymentId { get; set; }
       
    }
}

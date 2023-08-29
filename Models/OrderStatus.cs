using Bangazon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
        public enum OrderStatus
        {
            Pending = 1,
            Shipped, 
            Complete
        }

   
}

using Bangazon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
        public enum PaymentType
        {
            CreditCard = 1,
            PayPal = 2,
            Check = 3
        }
}

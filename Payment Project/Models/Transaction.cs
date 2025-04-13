using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Project.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }


        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Project.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }


        public string Type { get; set; }

        public string Details { get; set; }

        public bool IsDefault { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Project.Models
{
    [Index(nameof(UserName),IsUnique =true),Index(nameof(Email),IsUnique =true)]
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; }
    }
}

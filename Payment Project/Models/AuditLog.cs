using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Project.Models
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }
        public string Action { get; set; }

        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}

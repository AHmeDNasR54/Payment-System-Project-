using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Payment_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Project
{
    public class ApplicationDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-VCCOGGV;Initial Catalog = Payment_System_Project;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // config user properties
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

            // config PaymentMethode properties
            modelBuilder.Entity<PaymentMethod>().Property(p => p.IsDefault).HasDefaultValue(false);

            // config Transaction properties
            modelBuilder.Entity<Transaction>().Property(t=> t.CreatedAt).HasDefaultValueSql("GETDATE()");

            // config Audit properties
            modelBuilder.Entity<AuditLog>().Property(a => a.TimeStamp).HasDefaultValueSql("GETDATE()");

            //change Cascade between the transaction and payment to avoid sql cycle casscae error
            modelBuilder.Entity<Transaction>()
                        .HasOne(t => t.PaymentMethod)
                        .WithMany(pm => pm.Transactions)
                        .HasForeignKey(t => t.PaymentMethodId)
                        .OnDelete(DeleteBehavior.NoAction); // Disable cascade here
        }
        public DbSet<User> Users { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public   DbSet<AuditLog> AuditLog { get; set; }
    }
}

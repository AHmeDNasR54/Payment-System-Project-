using Microsoft.EntityFrameworkCore;
using Payment_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Project
{
    public class PaymentSystem
    {
        private readonly ApplicationDBContext _context;

        public PaymentSystem(ApplicationDBContext _context)
        {
            this._context = _context;
        }

        // User Management
        public User RegisterUser(string username, string email, string password)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserName == username);
            if (user != null)
            {
                Console.WriteLine("This username is already exist , Sign up again with different one ");
            }
            else
            {
                 user = new User
                {
                    Email = email,
                    UserName = username,
                    Password = password,
                    CreatedAt = DateTime.Now,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                Console.WriteLine("User registered!");

                // log action
                user = _context.Users.FirstOrDefault(u => u.UserName == username);
                LogAction(user.UserId, "User Registered");
            }
            return user; 
        }

        public User Login(string username, string password)
        {
            var user=_context.Users.FirstOrDefault(u=>u.UserName == username && u.Password==password);
            if (user != null)
            {
                LogAction(user.UserId, "User Logged In ");

            }
            return user;
        }

        public User UpdateUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {

                while (true)
                {
                    Console.Write("New username :"); var newUsername = Console.ReadLine();
                    Console.Write("New password :"); var newPassword = Console.ReadLine();
                    Console.Write("New Email:"); var newEmail= Console.ReadLine();

                    var newuser = _context.Users.FirstOrDefault(u => u.UserName == newUsername);
                    if (newuser != null)
                    {
                        Console.WriteLine("This username is already exist , enter a new one agian");

                    }
                    else
                    {
                        user.UserName = newUsername;
                        user.Password = newPassword;
                        user.Email = newEmail;
                        _context.Users.Update(user);
                        _context.SaveChanges();
                        LogAction(user.UserId, "User Update his profile");
                        
                        break;
                    }
                }
            }
            
            return user;
        }

        // Payment Methods 

        public void AddPaymentMethod(int userId, string type, string details, bool isDefault)
        {
            var paymentMethod = new PaymentMethod
            {
                UserId = userId,
                Type = type,
                Details = details,
                IsDefault = isDefault
            };

            if (isDefault)
            {
                var currentDefault = _context.PaymentMethods
                    .FirstOrDefault(pm => pm.UserId == userId && pm.IsDefault);
                if (currentDefault != null)
                    currentDefault.IsDefault = false;
            }

            _context.PaymentMethods.Add(paymentMethod);
            _context.SaveChanges();
            LogAction(userId, $"Payment Method Added: {type}");
        }
        public void SetDefaultPaymentMethod(int userId,int paymentId)
        {
            var payment = _context.PaymentMethods.FirstOrDefault(pm => pm.UserId == userId && pm.PaymentMethodId==paymentId);
            if (payment != null)
            {
                payment.IsDefault = true;
                _context.PaymentMethods.Update(payment);
                _context.SaveChanges();
                LogAction(userId, $"Set the payment : {payment.Type}  Default");

            }
            var currentDefault = _context.PaymentMethods
                .FirstOrDefault(pm=>pm.UserId==userId&&pm.IsDefault);
            if (currentDefault != null)
                currentDefault.IsDefault = false;
                 _context.SaveChanges();

        }

        // Transaction 
        public void CreateTransaction(int userId, int paymentMethodId, decimal amount)
        {
            var paymentMethod = _context.PaymentMethods.Find(paymentMethodId);
            if (paymentMethod == null || paymentMethod.UserId != userId)
                throw new Exception("Invalid payment method");


            var transaction = new Transaction
            {
                UserId = userId,
                PaymentMethodId = paymentMethodId,
                Amount = amount,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            LogAction(userId, $"Transaction Created: {amount}");
        }
        public void UpdateTransactionStatus(int transactionId, string status)
        {
            var transaction = _context.Transactions.Find(transactionId);
            if (transaction != null)
            {
                transaction.Status = status;
                _context.SaveChanges();
                LogAction(transaction.UserId, $"Transaction Updated to {status}");
            }
            else
            {
                Console.WriteLine("Failed transaction Id");
            }
        }
        
        //AuditLog
        public void LogAction(int userId,string action)
        {
            var audit = new AuditLog
            {
                UserId = userId,
                Action = action,
                TimeStamp = DateTime.Now,
            };
            _context.AuditLog.Add(audit);
            _context.SaveChanges();
        }

        //Reporting 
        // Reporting
        public List<Transaction> GetUserTransactions(int userId)
        {
            return _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.PaymentMethod) // Include PaymentMethod data
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
        }

        public List<AuditLog> GetUserAuditLogs(int userId)
        {
            return _context.AuditLog
                .Where(al => al.UserId == al.UserId)
                .OrderByDescending(al => al.TimeStamp)
                .ToList();
        }
        public List<PaymentMethod> GetUserPayments(int userId)
        {
            return _context.PaymentMethods.Where(pm=>pm.UserId == userId).ToList();
        }

    }
}

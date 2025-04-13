using Microsoft.Extensions.Options;
using Payment_Project.Models;

namespace Payment_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var _context = new ApplicationDBContext();
            var system = new PaymentSystem(_context);

            User currentUser = null;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Payment System Console App");
                if (currentUser == null)
                {
                    Console.WriteLine("1. Register");
                    Console.WriteLine("2. Login");
                }
                else
                {
                    Console.WriteLine($"Logged in as: {currentUser.UserName}");
                    Console.WriteLine("1. Add Payment Method");
                    Console.WriteLine("2. Create Transaction");
                    Console.WriteLine("3. Update Transaction Status");
                    Console.WriteLine("4. View Transactions");
                    Console.WriteLine("5. View PaymentMethods");
                    Console.WriteLine("6. View Audit Logs");
                    Console.WriteLine("7. Update User Data");
                    Console.WriteLine("8. Logout");
                }
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                try
                {
                    if (currentUser == null)
                    {
                        switch (choice)
                        {
                            case "1":
                                Console.Write("Username: ");
                                var username = Console.ReadLine();
                                Console.Write("Email: ");
                                var email = Console.ReadLine();
                                Console.Write("Password: ");
                                var password = Console.ReadLine();
                                currentUser = system.RegisterUser(username, email, password);
                                break;
                            case "2":
                                Console.Write("UserName: ");
                                username = Console.ReadLine();
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                                currentUser = system.Login(username, password);
                                Console.WriteLine(currentUser != null ? "Login successful!" : "Login failed.");
                                break;
                            case "0":
                                return;
                        }
                    }
                    else
                    {
                        switch (choice)
                        {
                            case "1":
                                Console.Write("Type (e.g., CreditCard): ");
                                var type = Console.ReadLine();
                                Console.Write("Details (e.g., card number): ");
                                var details = Console.ReadLine();
                                Console.Write("Is Default? (y/n): ");
                                var isDefault = Console.ReadLine().ToLower() == "y";
                                system.AddPaymentMethod(currentUser.UserId, type, details, isDefault);
                                Console.WriteLine("Payment method added!");
                                break;
                            case "2":
                                Console.Write("Payment Method ID: ");
                                var pmId = int.Parse(Console.ReadLine());
                                Console.Write("Amount: ");
                                var amount = decimal.Parse(Console.ReadLine());
                                system.CreateTransaction(currentUser.UserId, pmId, amount);
                                Console.WriteLine("Transaction created!");
                                break;
                            case "3":
                                Console.Write("Transaction ID: ");
                                var tId = int.Parse(Console.ReadLine());
                                Console.Write("New Status (e.g., Completed): ");
                                var status = Console.ReadLine();
                                system.UpdateTransactionStatus(tId, status);
                                Console.WriteLine("Transaction updated!");
                                break;
                            case "4":
                                var transactions = system.GetUserTransactions(currentUser.UserId);
                                foreach (var t in transactions)
                                    Console.WriteLine($"ID: {t.TransactionId}, Amount: {t.Amount}, Status: {t.Status}, " +
                                                      $"Payment Type: {t.PaymentMethod.Type}, Date: {t.CreatedAt}");
                                break;
                            case "5": 
                                var paymentMethods = system.GetUserPayments(currentUser.UserId);
                                foreach (var pm in paymentMethods)
                                    Console.WriteLine($"ID: {pm.PaymentMethodId}, Type: {pm.Type}, Details: {pm.Details}, Default: {pm.IsDefault}");
                                break;
                            case "6":
                                var logs = system.GetUserAuditLogs(currentUser.UserId);
                                foreach (var log in logs)
                                    Console.WriteLine($"Action: {log.Action}, Time: {log.TimeStamp}");
                                break;
                            case "7":
                                Console.Write("UserName: ");
                                var username = Console.ReadLine();
                                Console.Write("Password: ");
                                var password = Console.ReadLine();
                              var Updated=system.UpdateUser(username, password);
                                Console.WriteLine( Updated!= null ? "Updaet successful!" : "Updated failed.");
                                break;
                            case "8":
                                currentUser = null;
                                Console.WriteLine("Logged out.");
                                break;
                            case "0":
                                return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
    


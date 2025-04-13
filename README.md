# Payment System Project 

## **Project Overview**
The Payment System will:
- Allow users to register and manage their accounts.
- Support multiple payment methods (e.g., credit card, bank transfer, wallet).
- Handle transactions and maintain an audit log.
- Generate reports for users and admins.

---

## **Entities and Database Schema**
### **1. User**
| Column Name     | Data Type       | Constraints                   |
|-----------------|-----------------|-------------------------------|
| UserId          | int             | Primary Key, Auto Increment   |
| Username        | string          | Unique, Required              |
| Email           | string          | Unique, Required              |
| PasswordHash    | string          | Required                      |
| CreatedAt       | DateTime        | Required, Default Now()       |

### **2. PaymentMethod**
| Column Name     | Data Type       | Constraints                   |
|-----------------|-----------------|-------------------------------|
| PaymentMethodId | int             | Primary Key, Auto Increment   |
| UserId          | int             | Foreign Key (User)            |
| Type            | string          | Required (e.g., CreditCard)   |
| Details         | string          | Required                      |
| IsDefault       | bool            | Default False                 |

### **3. Transaction**
| Column Name     | Data Type       | Constraints                   |
|-----------------|-----------------|-------------------------------|
| TransactionId   | int             | Primary Key, Auto Increment   |
| UserId          | int             | Foreign Key (User)            |
| PaymentMethodId | int             | Foreign Key (PaymentMethod)   |
| Amount          | decimal(18,2)   | Required                      |
| Status          | string          | Required (e.g., Completed)    |
| CreatedAt       | DateTime        | Required, Default Now()       |

### **4. AuditLog**
| Column Name     | Data Type       | Constraints                   |
|-----------------|-----------------|-------------------------------|
| LogId           | int             | Primary Key, Auto Increment   |
| Action          | string          | Required                      |
| UserId          | int             | Foreign Key (User)            |
| Timestamp       | DateTime        | Required, Default Now()       |

---

## **Features**

### **1. User Management**
- **Register User**: Create a new user account.
- **Login**: Authenticate users using their email and password.
- **Update Profile**: Allow users to change their username or password.

### **2. Payment Method Management**
- Add, edit, or delete payment methods.
- Set a default payment method.

### **3. Transactions**
- Initiate a payment by selecting a payment method and entering an amount.
- Update the transaction status (e.g., pending, completed, failed).
- Validate payment amounts to ensure sufficient funds or limits.

### **4. Audit Logging**
- Record actions such as user login, payment method changes, and transactions.

### **5. Reporting**
- Generate reports for users:
  - Transaction history (date range, status filter).
  - Total spending by category (e.g., wallet, credit card).
- Generate admin reports:
  - Transactions by all users.
  - Failed transactions.

---

## **Technology Stack**
### **Backend**
- **C#** with ASP.NET Cor
- **Entity Framework Core** for database operations.
- **LINQ** for data queries.

### **Database**
- **SQL Server** as the database.
- Use **Migrations** for schema management.


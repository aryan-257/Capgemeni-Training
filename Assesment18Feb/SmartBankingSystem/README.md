# Smart Banking System

## Overview
A comprehensive C# console application for managing bank accounts with support for Savings, Current, and Loan accounts.

## Features Implemented

### OOP Concepts
- Abstract base class `BankAccount` with inheritance
- Three derived classes: `SavingsAccount`, `CurrentAccount`, `LoanAccount`
- Polymorphism in interest calculation
- Encapsulation with protected balance property

### Account Types

#### Savings Account
- Minimum balance requirement (default: 1000)
- Interest rate: 4% per annum
- Cannot withdraw below minimum balance
- Throws `MinimumBalanceException` on violation

#### Current Account
- Overdraft facility (default: 5000)
- Interest rate: 2% per annum
- Can withdraw beyond balance up to overdraft limit
- Throws `InsufficientBalanceException` when overdraft exceeded

#### Loan Account
- Represents loan with negative balance
- Cannot deposit (throws `InvalidTransactionException`)
- Use `Repay()` method for loan repayment
- Interest charged on outstanding amount
- Cannot withdraw

### Custom Exceptions
- `InsufficientBalanceException` - Withdrawal exceeds available balance
- `MinimumBalanceException` - Minimum balance requirement violated
- `InvalidTransactionException` - Invalid operations (e.g., deposit to loan account)

### Features
- Transaction history tracking for all accounts
- Money transfer between accounts
- Interest calculation (polymorphic)
- Comprehensive LINQ queries
- Menu-driven console interface

### LINQ Queries Implemented
1. Get accounts with balance > 50,000
2. Calculate total bank balance
3. Get top 3 highest balance accounts
4. Group accounts by type
5. Find customers whose name starts with specific letter

### Business Rules
- Withdrawal cannot exceed balance (except Current Account with overdraft)
- Loan Account cannot accept deposits
- Minimum balance enforced for Savings Account
- All transactions logged in history
- Transfer validation and rollback on failure

## How to Run
```bash
dotnet build SmartBankingSystem.sln
dotnet run --project SmartBankingSystem
```

## Sample Data
The system loads 7 sample accounts on startup:
- 3 Savings Accounts (including customers starting with 'R')
- 2 Current Accounts
- 2 Loan Accounts

## Technologies Used
- .NET 10.0
- C# with OOP principles
- LINQ for queries
- Custom exception handling
- Collections (List<T>)

## Architecture
```
SmartBankingSystem/
├── Models/
│   ├── BankAccount.cs (Abstract)
│   ├── SavingsAccount.cs
│   ├── CurrentAccount.cs
│   └── LoanAccount.cs
├── Services/
│   └── BankingService.cs
├── Exceptions/
│   ├── InsufficientBalanceException.cs
│   ├── MinimumBalanceException.cs
│   └── InvalidTransactionException.cs
└── Program.cs
```

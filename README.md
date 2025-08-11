# Finance Management System

A comprehensive C# application demonstrating modern software engineering principles including interfaces, records, sealed classes, and inheritance control for financial transaction processing.

## Features

### Core Components

1. **Transaction Record**: Immutable data structure representing financial transactions
2. **Interface-based Processing**: Modular payment processors using the `ITransactionProcessor` interface
3. **Account Management**: Base `Account` class with specialized `SavingsAccount` (sealed class)
4. **Data Integrity**: Built-in validation and insufficient funds checking

### Architecture Highlights

- **Records**: `Transaction` record ensures immutability of financial data
- **Interfaces**: `ITransactionProcessor` enables polymorphic behavior
- **Sealed Classes**: `SavingsAccount` prevents further inheritance
- **Protected Members**: Controlled access to account balance
- **Virtual Methods**: Extensible transaction processing

## Class Structure

```
FinanceSystem/
├── Transaction.cs (Record)
├── ITransactionProcessor.cs (Interface)
├── BankTransferProcessor.cs
├── MobileMoneyProcessor.cs
├── CryptoWalletProcessor.cs
├── Account.cs (Base Class)
├── SavingsAccount.cs (Sealed Class)
├── FinanceApp.cs (Main Application Logic)
└── Program.cs (Entry Point)
```

## How to Run

1. Open terminal in the project directory
2. Run: `dotnet run`

## Example Output

The application demonstrates:
- Transaction processing with different payment methods
- Balance updates with validation
- Insufficient funds handling
- Transaction history tracking

## Design Patterns Used

- **Strategy Pattern**: Different transaction processors
- **Template Method**: Virtual methods in base Account class
- **Immutable Objects**: Transaction records
- **Encapsulation**: Protected setters and private fields

# Integrated Management System

_A comprehensive C# console application demonstrating modern software engineering principles across multiple business domains._

This project showcases an enterprise-grade management system, integrating several specialized subsystems. Built using modern C# (targeting .NET 8), it leverages key design patterns, advanced language features, and robust data structures to provide a scalable and maintainable foundation for business applications.

---

## System Overview

The Integrated Management System consists of the following core modules:

1. **Banking System**: Financial transaction processing supporting multiple payment methods.
2. **Healthcare System**: Patient and prescription management.
3. **Warehouse System**: Inventory tracking and management for various product types.
4. **Grading System**: Student records and performance calculations.
5. **Inventory Records System**: Generic inventory management with JSON-based persistence.

---

## Key Features

### Banking System
- Multi-processor transaction handling (Mobile Money, Bank Transfer, Crypto)
- Account management with validation
- Transaction history & category-based reporting
- Immutable transaction records

### Healthcare System
- Patient record management
- Prescription tracking with validation
- Medical history reporting & appointment scheduling

### Warehouse System
- Multi-type inventory management
- Electronic and grocery item specialization
- Inventory search, filtering, and stock level monitoring

### Grading System
- Student record management
- Grade calculation with statistical analysis
- Performance reporting
- Data persistence via file I/O

### Inventory Records System
- Generic inventory tracking with type constraints
- JSON-based data persistence
- CRUD operations for inventory items
- Audit logging

---

## Technical Highlights

- **Object-Oriented Design**: Inheritance, polymorphism, encapsulation
- **Modern C# Features**: Records, interfaces, generics, LINQ, nullable reference types
- **Design Patterns**: Strategy, Repository, Template Method, Factory, Command, Singleton
- **Error Handling**: Custom exceptions & graceful degradation
- **Data Structures**: Optimized collections for varied use cases
- **Persistence**: File I/O & JSON serialization

---

## Project Architecture

```
FinanceSystem/
├── Program.cs
│
├── BankingSystem/
│   ├── Account.cs
│   ├── SavingsAccount.cs
│   ├── Transaction.cs
│   ├── ITransactionProcessor.cs
│   ├── BankTransferProcessor.cs
│   ├── MobileMoneyProcessor.cs
│   ├── CryptoWalletProcessor.cs
│   └── FinanceApp.cs
│
├── HealthSystem/
│   ├── Patient.cs
│   ├── Prescription.cs
│   └── HealthSystemApp.cs
│
├── WarehouseSystem/
│   ├── IInventoryItem.cs
│   ├── ElectronicItem.cs
│   ├── GroceryItem.cs
│   ├── InventoryRepository.cs
│   ├── WareHouseManager.cs
│   ├── WareHouseManagerEnhanced.cs
│   └── WareHouseManagerInteractive.cs
│
├── GradingSystem/
│   ├── Student.cs
│   ├── StudentResultProcessor.cs
│   ├── GradingExceptions.cs
│   └── GradingSystemApp.cs
│
└── InventoryRecordSystem/
    ├── IInventoryEntity.cs
    ├── InventoryItem.cs
    ├── InventoryLogger.cs
    ├── InventoryApp.cs
    └── InventoryRecordSystem.cs
```

---

## Design Patterns Utilized

- **Strategy Pattern**: Transaction processors in Banking System.
- **Repository Pattern**: Generic inventory repositories.
- **Template Method**: Virtual methods in base classes.
- **Factory Method**: Creation of specialized inventory items.
- **Command Pattern**: Menu-driven command execution.
- **Singleton Pattern**: System manager instances.

---

## Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or newer
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) with the C# extension

### Building & Running

1. **Clone the repository:**
   ```sh
   git clone https://github.com/dr-akoto/dcit318-assignment3-11311652.git
   ```
2. **Navigate to the project directory:**
   ```sh
   cd dcit318-assignment3-11311652
   ```
3. **Build and run the application:**
   ```sh
   dotnet run
   ```
4. **Follow the interactive menu to explore different subsystems.**

---

## Usage Examples

- **Banking System**: Process transactions, view account history, generate reports.
- **Healthcare System**: Manage patient records, prescriptions, and appointments.
- **Warehouse System**: Track multi-type inventory, manage stock levels, report generation.
- **Grading System**: Student record management, grade calculations, performance analysis.
- **Inventory Records System**: Audit inventory operations, import/export data via JSON.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- DCIT 318 course instructors & teaching assistants
- University of Ghana, Department of Computer Science  
- Student ID: **11311652**

---

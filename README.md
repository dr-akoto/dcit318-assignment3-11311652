# Integrated Management System
##A comprehensive C# console application demonstrating modern software engineering principles across multiple business domains. The system incorporates various design patterns, data structures, and C# language features to provide a robust foundation for enterprise management.
### System OverviewThis integrated management system consists of five specialized subsystems:1. **Banking System**: Financial transaction processing with multiple payment methods2. **Healthcare System**: Patient and prescription management3. **Warehouse System**: Inventory tracking and management4. **Grading System**: Student records and grade processing5. **Inventory Records System**: Generic inventory tracking with JSON persistence## Core Features#### Banking System- Multi-processor transaction handling (Mobile Money, Bank Transfer, Crypto)- Account management with validation- Transaction history with category-based reporting- Immutable transaction records for data integrity#### Healthcare System- Patient record management- Prescription tracking with validation- Medical history reporting- Appointment scheduling### Warehouse System- Multi-type inventory management- Electronic and grocery item specialization- Inventory search and filtering- Stock level monitoring### Grading System- Student record management- Grade calculation with statistical analysis- Performance reporting- Data persistence with file I/O#### Inventory Records System- Generic inventory tracking with type constraints- JSON-based data persistence- CRUD operations for inventory items- Audit logging## Technical Highlights- **Object-Oriented Design**: Inheritance, polymorphism, encapsulation- **Modern C# Features**: Records, interfaces, generics, LINQ, nullable reference types- **Design Patterns**: Strategy, Repository, Template Method, Factory- **Error Handling**: Custom exceptions with graceful degradation- **Data Structures**: Optimized collections for different use cases- **Persistence**: File I/O and JSON serialization## Architecture

```
FinanceSystem/
├── Program.cs (Main Entry Point)
│
├── BankingSystem/
│   ├── Account.cs (Base Class)
│   ├── SavingsAccount.cs (Sealed Class)
│   ├── Transaction.cs (Record)
│   ├── ITransactionProcessor.cs (Interface)
│   ├── BankTransferProcessor.cs
│   ├── MobileMoneyProcessor.cs
│   ├── CryptoWalletProcessor.cs
│   └── FinanceApp.cs (Banking System Logic)
│
├── HealthSystem/
│   ├── Patient.cs
│   ├── Prescription.cs
│   └── HealthSystemApp.cs (Healthcare System Logic)
│
├── WarehouseSystem/
│   ├── IInventoryItem.cs (Interface)
│   ├── ElectronicItem.cs
│   ├── GroceryItem.cs
│   ├── InventoryRepository.cs (Generic Repository)
│   ├── WareHouseManager.cs
│   ├── WareHouseManagerEnhanced.cs
│   └── WareHouseManagerInteractive.cs
│
├── GradingSystem/
│   ├── Student.cs
│   ├── StudentResultProcessor.cs
│   ├── GradingExceptions.cs
│   └── GradingSystemApp.cs (Grading System Logic)
│
└── InventoryRecordSystem/
    ├── IInventoryEntity.cs (Interface)
    ├── InventoryItem.cs (Record)
    ├── InventoryLogger.cs (Generic Logger)
    ├── InventoryApp.cs
    └── InventoryRecordSystem.cs (Inventory System Logic)
```

## Design Patterns Implemented

- **Strategy Pattern**: Different transaction processors in Banking System
- **Repository Pattern**: Generic inventory repositories with type constraints
- **Template Method**: Virtual methods in base classes with specialized implementations
- **Factory Method**: Creation of specialized inventory items
- **Command Pattern**: Menu-driven command execution
- **Singleton**: System manager instances

## Getting Started

### Prerequisites
- .NET SDK 8.0 or higher
- Visual Studio 2022 or VS Code with C# extension

### Running the Application

1. Clone the repository:
   ```
   git clone https://github.com/dr-akoto/dcit318-assignment3-11311652.git
   ```

2. Navigate to the project directory:
   ```
   cd dcit318-assignment3-11311652
   ```

3. Build and run the application:
   ```
   dotnet run
   ```

4. Use the interactive menu to navigate between systems

## Usage Examples

### Banking System
- Process financial transactions through multiple payment channels
- Track spending by category
- Generate transaction reports

### Healthcare System
- Manage patient records and medical history
- Track prescriptions and appointments
- Generate health reports

### Warehouse System
- Track inventory levels across multiple product types
- Generate inventory reports
- Manage stock replenishment

### Grading System
- Manage student records and grades
- Calculate performance metrics
- Generate grade reports

### Inventory Records System
- Generic inventory management with JSON persistence
- Track inventory items with audit logging
- Import/export inventory data

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- DCIT 318 course instructors and teaching assistants
- University of Ghana, Department of Computer Science
- Student ID: 11311652

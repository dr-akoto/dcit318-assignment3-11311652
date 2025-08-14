using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceSystem.WarehouseSystem
{
    /// <summary>
    /// Interactive warehouse manager with enhanced validation and table formatting
    /// </summary>
    public class WareHouseManagerEnhanced
    {
        /// <summary>
        /// Repository for managing electronic items
        /// </summary>
        private InventoryRepository<ElectronicItem> _electronics;

        /// <summary>
        /// Repository for managing grocery items
        /// </summary>
        private InventoryRepository<GroceryItem> _groceries;

        /// <summary>
        /// Initializes a new instance of the WareHouseManagerEnhanced
        /// </summary>
        public WareHouseManagerEnhanced()
        {
            _electronics = new InventoryRepository<ElectronicItem>();
            _groceries = new InventoryRepository<GroceryItem>();
        }

        /// <summary>
        /// Main execution method that runs the interactive warehouse management system
        /// </summary>
        public void Run()
        {
            Console.WriteLine("=== Warehouse Inventory Management System ===\n");

            // Initialize with some sample data
            SeedData();

            bool continueProcessing = true;

            while (continueProcessing)
            {
                try
                {
                    int operationType = GetOperationType();

                    switch (operationType)
                    {
                        case 0:
                            continueProcessing = false;
                            Console.WriteLine("Thank you for using the Warehouse Management System!");
                            break;

                        case 1:
                            AddElectronicItem();
                            break;

                        case 2:
                            AddGroceryItem();
                            break;

                        case 3:
                            ViewAllElectronics();
                            break;

                        case 4:
                            ViewAllGroceries();
                            break;

                        case 5:
                            UpdateElectronicQuantity();
                            break;

                        case 6:
                            UpdateGroceryQuantity();
                            break;

                        case 7:
                            RemoveElectronicItem();
                            break;

                        case 8:
                            RemoveGroceryItem();
                            break;

                        case 9:
                            SearchElectronicItem();
                            break;

                        case 10:
                            SearchGroceryItem();
                            break;

                        case 11:
                            ViewSystemSummary();
                            break;

                        case 12:
                            DemonstrateExceptionHandling();
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ An unexpected error occurred: {ex.Message}\n");
                    Console.WriteLine("Please try again.\n");
                }
            }
        }

        /// <summary>
        /// Displays the main menu and gets user choice
        /// </summary>
        private int GetOperationType()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              Warehouse Management System                     ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║  1. Add Electronic Item      │  7. Remove Electronic Item   ║");
            Console.WriteLine("║  2. Add Grocery Item          │  8. Remove Grocery Item      ║");
            Console.WriteLine("║  3. View All Electronics      │  9. Search Electronic Item   ║");
            Console.WriteLine("║  4. View All Groceries        │ 10. Search Grocery Item      ║");
            Console.WriteLine("║  5. Update Electronic Stock   │ 11. View System Summary      ║");
            Console.WriteLine("║  6. Update Grocery Stock      │ 12. Demo Exception Handling  ║");
            Console.WriteLine("║  0. Exit                      │                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.Write("Enter your choice (0-12): ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 0 && choice <= 12)
                {
                    Console.WriteLine();
                    return choice;
                }

                Console.Write("❌ Invalid input. Please enter a number between 0 and 12: ");
            }
        }

        /// <summary>
        /// Gets a valid unique ID with real-time validation
        /// </summary>
        private int GetValidId<T>(InventoryRepository<T> repo, string itemType) where T : IInventoryItem
        {
            Console.Write($"Enter {itemType} ID: ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    try
                    {
                        // Check if ID already exists
                        var existingItem = repo.GetItemById(id);
                        Console.WriteLine($"❌ ERROR: {itemType} with ID {id} already exists!");
                        Console.WriteLine($"   Existing item: {existingItem.Name}");
                        Console.Write($"Please enter a different {itemType} ID: ");
                        continue;
                    }
                    catch (ItemNotFoundException)
                    {
                        // ID doesn't exist, which is what we want
                        return id;
                    }
                }

                Console.Write("❌ Invalid ID. Please enter a valid number: ");
            }
        }

        /// <summary>
        /// Gets a valid existing ID for updates/removal
        /// </summary>
        private int GetValidExistingId<T>(InventoryRepository<T> repo, string itemType) where T : IInventoryItem
        {
            Console.Write($"Enter {itemType} ID: ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    try
                    {
                        // Check if ID exists
                        repo.GetItemById(id);
                        return id;
                    }
                    catch (ItemNotFoundException)
                    {
                        Console.WriteLine($"❌ ERROR: {itemType} with ID {id} not found!");
                        Console.Write($"Please enter a valid {itemType} ID: ");
                        continue;
                    }
                }

                Console.Write("❌ Invalid ID. Please enter a valid number: ");
            }
        }

        /// <summary>
        /// Gets a valid non-empty string input
        /// </summary>
        private string GetValidStringInput(string prompt, string fieldName)
        {
            Console.Write(prompt);

            while (true)
            {
                string input = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }

                Console.Write($"❌ {fieldName} cannot be empty. Please enter a valid {fieldName.ToLower()}: ");
            }
        }

        /// <summary>
        /// Gets a valid positive integer
        /// </summary>
        private int GetValidPositiveInteger(string prompt)
        {
            Console.Write(prompt);

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value >= 0)
                {
                    return value;
                }

                Console.Write("❌ Invalid input. Please enter a non-negative number: ");
            }
        }

        /// <summary>
        /// Gets a valid date
        /// </summary>
        private DateTime GetValidDate(string prompt)
        {
            Console.Write(prompt);

            while (true)
            {
                string input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    return DateTime.Now.AddDays(30); // Default to 30 days from now
                }

                if (DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }

                Console.Write("❌ Invalid date format. Please enter a valid date (YYYY-MM-DD) or press Enter for default: ");
            }
        }

        /// <summary>
        /// Adds a new electronic item with enhanced validation
        /// </summary>
        private void AddElectronicItem()
        {
            Console.WriteLine("=== Add New Electronic Item ===");

            try
            {
                // Get and validate ID first
                int id = GetValidId(_electronics, "Electronic Item");

                // Get other details
                string name = GetValidStringInput("Enter Item Name: ", "Item name");
                int quantity = GetValidPositiveInteger("Enter Initial Quantity: ");
                string brand = GetValidStringInput("Enter Brand: ", "Brand");
                int warrantyMonths = GetValidPositiveInteger("Enter Warranty Months: ");

                var newItem = new ElectronicItem(id, name, quantity, brand, warrantyMonths);
                _electronics.AddItem(newItem);

                Console.WriteLine("\n✓ SUCCESS: Electronic item added successfully!");
                PrintElectronicItemTable(new List<ElectronicItem> { newItem }, "ITEM ADDED");

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Adds a new grocery item with enhanced validation
        /// </summary>
        private void AddGroceryItem()
        {
            Console.WriteLine("=== Add New Grocery Item ===");

            try
            {
                // Get and validate ID first
                int id = GetValidId(_groceries, "Grocery Item");

                // Get other details
                string name = GetValidStringInput("Enter Item Name: ", "Item name");
                int quantity = GetValidPositiveInteger("Enter Initial Quantity: ");
                DateTime expiryDate = GetValidDate("Enter Expiry Date (YYYY-MM-DD) or press Enter for default (30 days): ");

                var newItem = new GroceryItem(id, name, quantity, expiryDate);
                _groceries.AddItem(newItem);

                Console.WriteLine("\n✓ SUCCESS: Grocery item added successfully!");
                PrintGroceryItemTable(new List<GroceryItem> { newItem }, "ITEM ADDED");

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Views all electronic items in table format
        /// </summary>
        private void ViewAllElectronics()
        {
            Console.WriteLine("=== All Electronic Items ===");

            var items = _electronics.GetAllItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No electronic items found in the system.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            PrintElectronicItemTable(items, "ALL ELECTRONIC ITEMS");
            Console.WriteLine($"\nTotal Electronic Items: {items.Count}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Views all grocery items in table format
        /// </summary>
        private void ViewAllGroceries()
        {
            Console.WriteLine("=== All Grocery Items ===");

            var items = _groceries.GetAllItems();

            if (items.Count == 0)
            {
                Console.WriteLine("No grocery items found in the system.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
                return;
            }

            PrintGroceryItemTable(items, "ALL GROCERY ITEMS");
            Console.WriteLine($"\nTotal Grocery Items: {items.Count}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Updates electronic item quantity with validation
        /// </summary>
        private void UpdateElectronicQuantity()
        {
            Console.WriteLine("=== Update Electronic Item Quantity ===");

            try
            {
                int id = GetValidExistingId(_electronics, "Electronic Item");
                var item = _electronics.GetItemById(id);

                Console.WriteLine($"\nCurrent item: {item.Name} (ID: {id}) - Current Quantity: {item.Quantity}");
                int newQuantity = GetValidPositiveInteger("Enter new quantity: ");

                int oldQuantity = item.Quantity;
                _electronics.UpdateQuantity(id, newQuantity);

                Console.WriteLine("\n✓ SUCCESS: Electronic item quantity updated successfully!");
                Console.WriteLine("┌─────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│                        QUANTITY UPDATED                        │");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────┤");
                Console.WriteLine($"│ Item: {item.Name,-20} (ID: {id,-10})                    │");
                Console.WriteLine($"│ Old Quantity: {oldQuantity,-15} → New Quantity: {newQuantity,-10}    │");
                Console.WriteLine("└─────────────────────────────────────────────────────────────────┘");

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Updates grocery item quantity with validation
        /// </summary>
        private void UpdateGroceryQuantity()
        {
            Console.WriteLine("=== Update Grocery Item Quantity ===");

            try
            {
                int id = GetValidExistingId(_groceries, "Grocery Item");
                var item = _groceries.GetItemById(id);

                Console.WriteLine($"\nCurrent item: {item.Name} (ID: {id}) - Current Quantity: {item.Quantity}");
                int newQuantity = GetValidPositiveInteger("Enter new quantity: ");

                int oldQuantity = item.Quantity;
                _groceries.UpdateQuantity(id, newQuantity);

                Console.WriteLine("\n✓ SUCCESS: Grocery item quantity updated successfully!");
                Console.WriteLine("┌─────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│                        QUANTITY UPDATED                        │");
                Console.WriteLine("├─────────────────────────────────────────────────────────────────┤");
                Console.WriteLine($"│ Item: {item.Name,-20} (ID: {id,-10})                    │");
                Console.WriteLine($"│ Old Quantity: {oldQuantity,-15} → New Quantity: {newQuantity,-10}    │");
                Console.WriteLine("└─────────────────────────────────────────────────────────────────┘");

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Removes an electronic item with confirmation
        /// </summary>
        private void RemoveElectronicItem()
        {
            Console.WriteLine("=== Remove Electronic Item ===");

            try
            {
                int id = GetValidExistingId(_electronics, "Electronic Item");
                var item = _electronics.GetItemById(id);

                Console.WriteLine($"\nItem to remove: {item.Name} (ID: {id})");
                Console.Write("Are you sure you want to remove this item? (y/N): ");
                string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (confirmation == "y" || confirmation == "yes")
                {
                    _electronics.RemoveItem(id);

                    Console.WriteLine("\n✓ SUCCESS: Electronic item removed successfully!");
                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────┐");
                    Console.WriteLine("│                         ITEM REMOVED                           │");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine($"│ Removed: {item.Name,-20} (ID: {id,-10})                │");
                    Console.WriteLine("└─────────────────────────────────────────────────────────────────┘");
                }
                else
                {
                    Console.WriteLine("\nOperation cancelled.");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Removes a grocery item with confirmation
        /// </summary>
        private void RemoveGroceryItem()
        {
            Console.WriteLine("=== Remove Grocery Item ===");

            try
            {
                int id = GetValidExistingId(_groceries, "Grocery Item");
                var item = _groceries.GetItemById(id);

                Console.WriteLine($"\nItem to remove: {item.Name} (ID: {id})");
                Console.Write("Are you sure you want to remove this item? (y/N): ");
                string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (confirmation == "y" || confirmation == "yes")
                {
                    _groceries.RemoveItem(id);

                    Console.WriteLine("\n✓ SUCCESS: Grocery item removed successfully!");
                    Console.WriteLine("┌─────────────────────────────────────────────────────────────────┐");
                    Console.WriteLine("│                         ITEM REMOVED                           │");
                    Console.WriteLine("├─────────────────────────────────────────────────────────────────┤");
                    Console.WriteLine($"│ Removed: {item.Name,-20} (ID: {id,-10})                │");
                    Console.WriteLine("└─────────────────────────────────────────────────────────────────┘");
                }
                else
                {
                    Console.WriteLine("\nOperation cancelled.");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Searches for an electronic item by ID
        /// </summary>
        private void SearchElectronicItem()
        {
            Console.WriteLine("=== Search Electronic Item ===");

            try
            {
                int id = GetValidExistingId(_electronics, "Electronic Item");
                var item = _electronics.GetItemById(id);

                Console.WriteLine("\n✓ Item found:");
                PrintElectronicItemTable(new List<ElectronicItem> { item }, "SEARCH RESULT");

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Searches for a grocery item by ID
        /// </summary>
        private void SearchGroceryItem()
        {
            Console.WriteLine("=== Search Grocery Item ===");

            try
            {
                int id = GetValidExistingId(_groceries, "Grocery Item");
                var item = _groceries.GetItemById(id);

                Console.WriteLine("\n✓ Item found:");
                PrintGroceryItemTable(new List<GroceryItem> { item }, "SEARCH RESULT");

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Displays system summary in table format
        /// </summary>
        private void ViewSystemSummary()
        {
            Console.WriteLine("=== System Summary ===");

            var electronics = _electronics.GetAllItems();
            var groceries = _groceries.GetAllItems();

            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      SYSTEM SUMMARY                         ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Total Electronic Items: {electronics.Count,-10}                      ║");
            Console.WriteLine($"║ Total Grocery Items:    {groceries.Count,-10}                      ║");
            Console.WriteLine($"║ Total Items in System:  {electronics.Count + groceries.Count,-10}                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"║ Electronic Stock Value: {electronics.Sum(e => e.Quantity),-10}                      ║");
            Console.WriteLine($"║ Grocery Stock Value:    {groceries.Sum(g => g.Quantity),-10}                      ║");
            Console.WriteLine($"║ Total Stock Value:      {electronics.Sum(e => e.Quantity) + groceries.Sum(g => g.Quantity),-10}                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates exception handling scenarios
        /// </summary>
        private void DemonstrateExceptionHandling()
        {
            Console.WriteLine("=== Exception Handling Demonstration ===");

            Console.WriteLine("\n1. Testing DuplicateItemException:");
            try
            {
                var duplicateItem = new ElectronicItem(1, "Duplicate Item", 10, "TestBrand", 12);
                _electronics.AddItem(duplicateItem);
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"   ❌ Caught: {ex.Message}");
            }

            Console.WriteLine("\n2. Testing ItemNotFoundException:");
            try
            {
                _electronics.GetItemById(999);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"   ❌ Caught: {ex.Message}");
            }

            Console.WriteLine("\n3. Testing InvalidQuantityException:");
            try
            {
                _electronics.UpdateQuantity(1, -5);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"   ❌ Caught: {ex.Message}");
            }

            Console.WriteLine("\n4. Testing removal of non-existent item:");
            try
            {
                _electronics.RemoveItem(999);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"   ❌ Caught: {ex.Message}");
            }

            Console.WriteLine("\n✓ All exception handling scenarios demonstrated successfully!");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Prints electronic items in a formatted table
        /// </summary>
        private void PrintElectronicItemTable(List<ElectronicItem> items, string title)
        {
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{title.PadLeft((80 + title.Length) / 2).PadRight(78)}║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ ID   │ Name                 │ Quantity │ Brand            │ Warranty (Months) ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");

            foreach (var item in items.OrderBy(i => i.Id))
            {
                Console.WriteLine($"║ {item.Id,-4} │ {item.Name,-20} │ {item.Quantity,-8} │ {item.Brand,-16} │ {item.WarrantyMonths,-18} ║");
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Prints grocery items in a formatted table
        /// </summary>
        private void PrintGroceryItemTable(List<GroceryItem> items, string title)
        {
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║{title.PadLeft((80 + title.Length) / 2).PadRight(78)}║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ ID   │ Name                 │ Quantity │ Expiry Date                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");

            foreach (var item in items.OrderBy(i => i.Id))
            {
                Console.WriteLine($"║ {item.Id,-4} │ {item.Name,-20} │ {item.Quantity,-8} │ {item.ExpiryDate:yyyy-MM-dd}                         ║");
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
        }

        /// <summary>
        /// Seeds the inventory with sample data
        /// </summary>
        private void SeedData()
        {
            try
            {
                // Add sample electronic items
                _electronics.AddItem(new ElectronicItem(1, "Laptop", 5, "Dell", 24));
                _electronics.AddItem(new ElectronicItem(2, "Mouse", 15, "Logitech", 12));
                _electronics.AddItem(new ElectronicItem(3, "Keyboard", 10, "Corsair", 18));

                // Add sample grocery items
                _groceries.AddItem(new GroceryItem(1, "Milk", 20, DateTime.Now.AddDays(7)));
                _groceries.AddItem(new GroceryItem(2, "Bread", 25, DateTime.Now.AddDays(3)));
                _groceries.AddItem(new GroceryItem(3, "Apples", 30, DateTime.Now.AddDays(10)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }
}

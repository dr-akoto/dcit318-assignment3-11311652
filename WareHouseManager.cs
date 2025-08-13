using System;
using System.Collections.Generic;

namespace FinanceSystem
{
    /// <summary>
    /// Main warehouse manager that handles both electronic and grocery inventory
    /// </summary>
    public class WareHouseManager
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
        /// Initializes a new instance of the WareHouseManager
        /// </summary>
        public WareHouseManager()
        {
            _electronics = new InventoryRepository<ElectronicItem>();
            _groceries = new InventoryRepository<GroceryItem>();
        }

        /// <summary>
        /// Seeds the inventory with sample data
        /// </summary>
        public void SeedData()
        {
            Console.WriteLine("=== Seeding Warehouse Data ===");

            try
            {
                // Add sample electronic items
                var electronics = new[]
                {
                    new ElectronicItem(1, "iPhone 15", 50, "Apple", 12),
                    new ElectronicItem(2, "Samsung Galaxy S24", 30, "Samsung", 24),
                    new ElectronicItem(3, "Dell XPS 13", 15, "Dell", 36)
                };

                foreach (var item in electronics)
                {
                    _electronics.AddItem(item);
                    Console.WriteLine($"Added: {item}");
                }

                // Add sample grocery items
                var groceries = new[]
                {
                    new GroceryItem(101, "Organic Bananas", 100, DateTime.Now.AddDays(7)),
                    new GroceryItem(102, "Fresh Milk", 75, DateTime.Now.AddDays(5)),
                    new GroceryItem(103, "Whole Grain Bread", 50, DateTime.Now.AddDays(3))
                };

                foreach (var item in groceries)
                {
                    _groceries.AddItem(item);
                    Console.WriteLine($"Added: {item}");
                }

                Console.WriteLine("Warehouse data seeded successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}\n");
            }
        }

        /// <summary>
        /// Generic method to print all items in a repository
        /// </summary>
        /// <typeparam name="T">Type that implements IInventoryItem</typeparam>
        /// <param name="repo">Repository to print items from</param>
        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items found in this inventory.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"  {item}");
            }
            Console.WriteLine($"Total items: {items.Count}\n");
        }

        /// <summary>
        /// Generic method to increase stock for an item
        /// </summary>
        /// <typeparam name="T">Type that implements IInventoryItem</typeparam>
        /// <param name="repo">Repository containing the item</param>
        /// <param name="id">ID of the item</param>
        /// <param name="quantity">Quantity to add</param>
        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                int newQuantity = item.Quantity + quantity;
                repo.UpdateQuantity(id, newQuantity);
                Console.WriteLine($"Successfully increased stock for item {id}. New quantity: {newQuantity}");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while increasing stock: {ex.Message}");
            }
        }

        /// <summary>
        /// Generic method to remove an item by ID
        /// </summary>
        /// <typeparam name="T">Type that implements IInventoryItem</typeparam>
        /// <param name="repo">Repository containing the item</param>
        /// <param name="id">ID of the item to remove</param>
        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id); // Get item details before removal for logging
                repo.RemoveItem(id);
                Console.WriteLine($"Successfully removed item: {item}");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while removing item: {ex.Message}");
            }
        }

        /// <summary>
        /// Demonstrates exception handling by attempting various operations that should fail
        /// </summary>
        public void DemonstrateExceptionHandling()
        {
            Console.WriteLine("=== Demonstrating Exception Handling ===\n");

            // 1. Try to add a duplicate item
            Console.WriteLine("1. Attempting to add duplicate electronic item (ID: 1):");
            try
            {
                var duplicateItem = new ElectronicItem(1, "Fake iPhone", 10, "Fake Apple", 6);
                _electronics.AddItem(duplicateItem);
                Console.WriteLine("Duplicate item added successfully (this shouldn't happen!)");
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"✓ Caught DuplicateItemException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            Console.WriteLine();

            // 2. Try to remove a non-existent item
            Console.WriteLine("2. Attempting to remove non-existent item (ID: 999):");
            RemoveItemById(_electronics, 999);
            Console.WriteLine();

            // 3. Try to update with invalid quantity
            Console.WriteLine("3. Attempting to update item quantity to negative value:");
            try
            {
                _groceries.UpdateQuantity(101, -10);
                Console.WriteLine("Negative quantity set successfully (this shouldn't happen!)");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"✓ Caught InvalidQuantityException: {ex.Message}");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"✓ Caught ItemNotFoundException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            Console.WriteLine();

            // 4. Try to get a non-existent item
            Console.WriteLine("4. Attempting to get non-existent item (ID: 888):");
            try
            {
                var item = _groceries.GetItemById(888);
                Console.WriteLine($"Found item: {item} (this shouldn't happen!)");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"✓ Caught ItemNotFoundException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Gets the electronics repository for external access
        /// </summary>
        public InventoryRepository<ElectronicItem> Electronics => _electronics;

        /// <summary>
        /// Gets the groceries repository for external access
        /// </summary>
        public InventoryRepository<GroceryItem> Groceries => _groceries;

        /// <summary>
        /// Runs the complete warehouse management demonstration
        /// </summary>
        public void Run()
        {
            Console.WriteLine("=== Warehouse Inventory Management System ===\n");

            // Seed data
            SeedData();

            // Print all grocery items
            Console.WriteLine("=== All Grocery Items ===");
            PrintAllItems(_groceries);

            // Print all electronic items
            Console.WriteLine("=== All Electronic Items ===");
            PrintAllItems(_electronics);

            // Demonstrate exception handling
            DemonstrateExceptionHandling();

            // Demonstrate successful operations
            Console.WriteLine("=== Demonstrating Successful Operations ===\n");

            Console.WriteLine("1. Increasing stock for iPhone 15 (ID: 1) by 25 units:");
            IncreaseStock(_electronics, 1, 25);
            Console.WriteLine();

            Console.WriteLine("2. Updating quantity for Organic Bananas (ID: 101) to 150:");
            try
            {
                _groceries.UpdateQuantity(101, 150);
                var item = _groceries.GetItemById(101);
                Console.WriteLine($"✓ Successfully updated: {item}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("3. Current inventory summary:");
            Console.WriteLine($"   Electronics: {_electronics.Count} items");
            Console.WriteLine($"   Groceries: {_groceries.Count} items");
            Console.WriteLine();
        }
    }
}

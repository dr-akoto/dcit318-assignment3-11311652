namespace FinanceSystem.InventoryRecordSystem
{
    public class InventoryRecordSystem
    {
        private readonly InventoryLogger<InventoryItem> _logger;
        private bool _isRunning;

        public InventoryRecordSystem()
        {
            _logger = new InventoryLogger<InventoryItem>("inventory_data.json");
            _isRunning = true;
        }

        public static void RunInventorySystem()
        {
            var system = new InventoryRecordSystem();
            system.Run();
        }

        public void Run()
        {
            LoadExistingData();

            while (_isRunning)
            {
                DisplayMenu();
                ProcessUserChoice();
            }
        }

        private void LoadExistingData()
        {
            _logger.LoadFromFile();
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║      INVENTORY RECORDS SYSTEM         ║");
            Console.WriteLine("╠═══════════════════════════════════════╣");
            Console.WriteLine("║  1. Add New Item                      ║");
            Console.WriteLine("║  2. View All Items                    ║");
            Console.WriteLine("║  3. Save Changes                      ║");
            Console.WriteLine("║  4. Load Saved Data                   ║");
            Console.WriteLine("║  5. Add Sample Data                   ║");
            Console.WriteLine("║  0. Return to Main Menu               ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.Write("\nEnter your choice (0-5): ");
        }

        private void ProcessUserChoice()
        {
            var choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    AddNewItem();
                    break;
                case "2":
                    ViewAllItems();
                    break;
                case "3":
                    SaveChanges();
                    break;
                case "4":
                    LoadData();
                    break;
                case "5":
                    AddSampleData();
                    break;
                case "0":
                    ExitSystem();
                    break;
                default:
                    ShowInvalidChoice();
                    break;
            }
        }

        private void AddNewItem()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Inventory Item ===\n");

            try
            {
                Console.Write("Enter Item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    throw new ArgumentException("Invalid ID format. Please enter a number.");
                }

                Console.Write("Enter Item Name: ");
                string name = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("Item name cannot be empty.");
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    throw new ArgumentException("Invalid quantity format. Please enter a number.");
                }

                var item = new InventoryItem(id, name, quantity, DateTime.Now);
                _logger.Add(item);

                Console.WriteLine("\n✅ Item added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error: {ex.Message}");
            }

            PressAnyKeyToContinue();
        }

        private void ViewAllItems()
        {
            Console.Clear();
            Console.WriteLine("=== Current Inventory Items ===\n");

            var items = _logger.GetAll();
            if (!items.Any())
            {
                Console.WriteLine("No items in inventory.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}");
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Quantity: {item.Quantity}");
                    Console.WriteLine($"Date Added: {item.DateAdded:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine(new string('-', 30));
                }
            }

            PressAnyKeyToContinue();
        }

        private void SaveChanges()
        {
            Console.Clear();
            Console.WriteLine("=== Saving Inventory Data ===\n");

            _logger.SaveToFile();
            Console.WriteLine("✅ Data saved successfully!");

            PressAnyKeyToContinue();
        }

        private void LoadData()
        {
            Console.Clear();
            Console.WriteLine("=== Loading Inventory Data ===\n");

            _logger.LoadFromFile();
            Console.WriteLine("✅ Data loaded successfully!");

            PressAnyKeyToContinue();
        }

        private void AddSampleData()
        {
            Console.Clear();
            Console.WriteLine("=== Adding Sample Data ===\n");

            _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now));
            _logger.Add(new InventoryItem(2, "Mouse", 20, DateTime.Now));
            _logger.Add(new InventoryItem(3, "Keyboard", 15, DateTime.Now));
            _logger.Add(new InventoryItem(4, "Monitor", 8, DateTime.Now));
            _logger.Add(new InventoryItem(5, "Headphones", 25, DateTime.Now));

            Console.WriteLine("✅ Sample data added successfully!");

            PressAnyKeyToContinue();
        }

        private void ExitSystem()
        {
            Console.Clear();
            Console.WriteLine("=== Exiting Inventory System ===\n");
            Console.WriteLine("Would you like to save changes before exiting? (Y/N)");

            var response = Console.ReadLine()?.Trim().ToUpper() ?? "N";
            if (response == "Y")
            {
                _logger.SaveToFile();
                Console.WriteLine("\n✅ Changes saved successfully!");
            }

            _isRunning = false;
        }

        private void ShowInvalidChoice()
        {
            Console.WriteLine("\n❌ Invalid choice. Please enter a number between 0 and 5.");
            PressAnyKeyToContinue();
        }

        private void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
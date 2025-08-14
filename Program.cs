using FinanceSystem;

namespace FinanceSystem
{
    /// <summary>
    /// Main entry point for all management systems
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            bool continueRunning = true;

            while (continueRunning)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                     MAIN SYSTEM MENU                        ║");
                    Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║  1. Finance Management System                                ║");
                    Console.WriteLine("║  2. Healthcare Management System                             ║");
                    Console.WriteLine("║  3. Warehouse Inventory Management System                    ║");
                    Console.WriteLine("║  4. Student Grading System                                   ║");
                    Console.WriteLine("║  5. Inventory Records System                                 ║");
                    Console.WriteLine("║  6. Run All Systems                                          ║");
                    Console.WriteLine("║  0. Exit                                                     ║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                    Console.Write("Select an option (0-6): ");

                    var choice = Console.ReadLine()?.Trim() ?? "";

                    switch (choice)
                    {
                        case "1":
                            RunFinanceSystem();
                            break;
                        case "2":
                            RunHealthcareSystem();
                            break;
                        case "3":
                            RunWarehouseSystem();
                            break;
                        case "4":
                            RunGradingSystem();
                            break;
                        case "5":
                            RunInventoryRecordSystem();
                            break;
                        case "6":
                            RunAllSystems();
                            break;
                        case "0":
                            Console.WriteLine("👋 Goodbye! Thank you for using our management systems.");
                            continueRunning = false;
                            break;
                        default:
                            Console.WriteLine("❌ Invalid choice. Please enter a number between 0 and 6.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ An error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Runs the Finance Management System
        /// </summary>
        static void RunFinanceSystem()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("STARTING FINANCE MANAGEMENT SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");

            var financeApp = new FinanceApp();
            financeApp.Run();
        }

        /// <summary>
        /// Runs the Healthcare Management System
        /// </summary>
        static void RunHealthcareSystem()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("STARTING HEALTHCARE MANAGEMENT SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");

            // Instantiate HealthSystemApp
            var healthApp = new HealthSystemApp();

            // Run the interactive healthcare system
            healthApp.Run();
        }

        /// <summary>
        /// Runs the Warehouse Inventory Management System
        /// </summary>
        static void RunWarehouseSystem()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("STARTING WAREHOUSE INVENTORY MANAGEMENT SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");

            // Instantiate WareHouseManagerEnhanced
            var warehouseManager = new WareHouseManagerEnhanced();

            // Run the interactive warehouse system
            warehouseManager.Run();
        }

        /// <summary>
        /// Runs the Student Grading System
        /// </summary>
        static void RunGradingSystem()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("STARTING STUDENT GRADING SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");

            var gradingApp = new GradingSystemApp();
            gradingApp.Run();
        }

        /// <summary>
        /// Runs all systems sequentially
        /// </summary>
        static void RunAllSystems()
        {
            Console.WriteLine("🚀 Running all management systems...\n");

            RunHealthcareSystem();
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("SWITCHING TO FINANCE SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");
            RunFinanceSystem();
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("SWITCHING TO WAREHOUSE SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");
            RunWarehouseSystem();
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("SWITCHING TO GRADING SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");
            RunGradingSystem();
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("SWITCHING TO INVENTORY RECORDS SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");
            RunInventoryRecordSystem();
        }

        /// <summary>
        /// Runs the Inventory Records System
        /// </summary>
        static void RunInventoryRecordSystem()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("STARTING INVENTORY RECORDS SYSTEM");
            Console.WriteLine(new string('=', 50) + "\n");

            InventoryRecordSystem.RunInventorySystem();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

using FinanceSystem;

namespace FinanceSystem
{
    /// <summary>
    /// Main entry point for the Finance Management System and Healthcare System
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== SYSTEM SELECTION MENU ===");
                Console.WriteLine("1. Finance Management System");
                Console.WriteLine("2. Healthcare Management System");
                Console.WriteLine("3. Run Both Systems");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option (0-3): ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunFinanceSystem();
                        break;
                    case "2":
                        RunHealthcareSystem();
                        break;
                    case "3":
                        RunHealthcareSystem();
                        Console.WriteLine("\n" + new string('=', 50));
                        Console.WriteLine("SWITCHING TO FINANCE SYSTEM");
                        Console.WriteLine(new string('=', 50) + "\n");
                        RunFinanceSystem();
                        break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Running Healthcare System as default.");
                        RunHealthcareSystem();
                        break;
                }

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
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
    }
}

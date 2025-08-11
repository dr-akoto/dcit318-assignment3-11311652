using FinanceSystem;

namespace FinanceSystem
{
    /// <summary>
    /// Main entry point for the Finance Management System
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create and run the finance application
                var financeApp = new FinanceApp();
                financeApp.Run();

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}

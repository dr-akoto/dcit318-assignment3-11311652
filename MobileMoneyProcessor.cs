namespace FinanceSystem
{
    /// <summary>
    /// Concrete implementation for mobile money transaction processing
    /// </summary>
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[MOBILE MONEY] Processing transaction of ${transaction.Amount:F2} for {transaction.Category}");
            Console.WriteLine($"Transaction ID: {transaction.Id}, Date: {transaction.Date:yyyy-MM-dd}");
            Console.WriteLine("Mobile money transfer completed successfully.\n");
        }
    }
}

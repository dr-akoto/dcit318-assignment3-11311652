namespace FinanceSystem
{
    /// <summary>
    /// Concrete implementation for bank transfer transaction processing
    /// </summary>
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[BANK TRANSFER] Processing transaction of ${transaction.Amount:F2} for {transaction.Category}");
            Console.WriteLine($"Transaction ID: {transaction.Id}, Date: {transaction.Date:yyyy-MM-dd}");
            Console.WriteLine("Bank transfer processing completed successfully.\n");
        }
    }
}

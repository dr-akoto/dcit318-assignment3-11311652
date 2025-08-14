namespace FinanceSystem.BankingSystem
{
    /// <summary>
    /// Concrete implementation for crypto wallet transaction processing
    /// </summary>
    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[CRYPTO WALLET] Processing transaction of ${transaction.Amount:F2} for {transaction.Category}");
            Console.WriteLine($"Transaction ID: {transaction.Id}, Date: {transaction.Date:yyyy-MM-dd}");
            Console.WriteLine("Cryptocurrency transaction completed successfully.\n");
        }
    }
}

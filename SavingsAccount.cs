namespace FinanceSystem
{
    /// <summary>
    /// Sealed class representing a specialized savings account
    /// Inherits from Account and provides additional validation logic
    /// </summary>
    public sealed class SavingsAccount : Account
    {
        /// <summary>
        /// Initializes a new savings account
        /// </summary>
        /// <param name="accountNumber">The account identifier</param>
        /// <param name="initialBalance">The starting balance</param>
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        /// <summary>
        /// Overridden method to apply transactions with insufficient funds validation
        /// </summary>
        /// <param name="transaction">The transaction to apply</param>
        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine($"Insufficient funds in savings account {AccountNumber}. " +
                                $"Transaction amount: ${transaction.Amount:F2}, Available balance: ${Balance:F2}");
                return;
            }

            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied to savings account {AccountNumber}. " +
                            $"Amount deducted: ${transaction.Amount:F2}, Updated balance: ${Balance:F2}");
        }
    }
}

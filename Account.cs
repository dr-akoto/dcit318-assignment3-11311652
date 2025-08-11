namespace FinanceSystem
{
    /// <summary>
    /// Base class representing a general financial account
    /// </summary>
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        /// <summary>
        /// Initializes a new account with account number and initial balance
        /// </summary>
        /// <param name="accountNumber">The account identifier</param>
        /// <param name="initialBalance">The starting balance</param>
        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        /// <summary>
        /// Virtual method to apply a transaction to the account
        /// Deducts the transaction amount from the balance
        /// </summary>
        /// <param name="transaction">The transaction to apply</param>
        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied to account {AccountNumber}. New balance: ${Balance:F2}");
        }

        /// <summary>
        /// Virtual method to deposit money into the account
        /// </summary>
        /// <param name="amount">The amount to deposit</param>
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }

            Balance += amount;
            Console.WriteLine($"${amount:F2} deposited to account {AccountNumber}. New balance: ${Balance:F2}");
        }

        /// <summary>
        /// Method to check and display current balance
        /// </summary>
        public void CheckBalance()
        {
            Console.WriteLine($"Account {AccountNumber} current balance: ${Balance:F2}");
        }
    }
}

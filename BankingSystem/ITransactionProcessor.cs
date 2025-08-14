namespace FinanceSystem.BankingSystem
{
    /// <summary>
    /// Interface defining payment behavior for transaction processing
    /// </summary>
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }
}

namespace FinanceSystem
{
    /// <summary>
    /// Record type representing immutable financial transaction data
    /// </summary>
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);
}

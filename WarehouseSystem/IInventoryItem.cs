namespace FinanceSystem.WarehouseSystem
{
    /// <summary>
    /// Marker interface for all inventory items in the warehouse system
    /// </summary>
    public interface IInventoryItem
    {
        /// <summary>
        /// Unique identifier for the inventory item
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Name of the inventory item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Current quantity in stock
        /// </summary>
        int Quantity { get; set; }
    }
}

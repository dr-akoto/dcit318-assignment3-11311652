using System;

namespace FinanceSystem.WarehouseSystem
{
    /// <summary>
    /// Represents a grocery item in the warehouse inventory
    /// </summary>
    public class GroceryItem : IInventoryItem
    {
        /// <summary>
        /// Unique identifier for the grocery item
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Name of the grocery item
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Current quantity in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Expiry date of the grocery item
        /// </summary>
        public DateTime ExpiryDate { get; }

        /// <summary>
        /// Initializes a new grocery item with all required properties
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <param name="name">Name of the item</param>
        /// <param name="quantity">Initial quantity</param>
        /// <param name="expiryDate">Expiry date</param>
        public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }

        /// <summary>
        /// Returns a string representation of the grocery item
        /// </summary>
        public override string ToString()
        {
            return $"Grocery Item [ID: {Id}, Name: {Name}, Quantity: {Quantity}, Expiry: {ExpiryDate:yyyy-MM-dd}]";
        }
    }
}

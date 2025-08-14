using System;

namespace FinanceSystem.WarehouseSystem
{
    /// <summary>
    /// Represents an electronic item in the warehouse inventory
    /// </summary>
    public class ElectronicItem : IInventoryItem
    {
        /// <summary>
        /// Unique identifier for the electronic item
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Name of the electronic item
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Current quantity in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Brand of the electronic item
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Warranty period in months
        /// </summary>
        public int WarrantyMonths { get; }

        /// <summary>
        /// Initializes a new electronic item with all required properties
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <param name="name">Name of the item</param>
        /// <param name="quantity">Initial quantity</param>
        /// <param name="brand">Brand name</param>
        /// <param name="warrantyMonths">Warranty period in months</param>
        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }

        /// <summary>
        /// Returns a string representation of the electronic item
        /// </summary>
        public override string ToString()
        {
            return $"Electronic Item [ID: {Id}, Name: {Name}, Quantity: {Quantity}, Brand: {Brand}, Warranty: {WarrantyMonths} months]";
        }
    }
}

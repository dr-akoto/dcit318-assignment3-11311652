using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceSystem
{
    /// <summary>
    /// Generic repository for managing inventory items with type safety and constraint enforcement
    /// </summary>
    /// <typeparam name="T">Type that implements IInventoryItem</typeparam>
    public class InventoryRepository<T> where T : IInventoryItem
    {
        /// <summary>
        /// Dictionary storing inventory items with item ID as the key
        /// </summary>
        private Dictionary<int, T> _items;

        /// <summary>
        /// Initializes a new instance of the InventoryRepository
        /// </summary>
        public InventoryRepository()
        {
            _items = new Dictionary<int, T>();
        }

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <exception cref="DuplicateItemException">Thrown when an item with the same ID already exists</exception>
        /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
        public void AddItem(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null");

            if (_items.ContainsKey(item.Id))
                throw new DuplicateItemException($"Item with ID {item.Id} already exists in the inventory.");

            _items[item.Id] = item;
        }

        /// <summary>
        /// Retrieves an item by its ID
        /// </summary>
        /// <param name="id">ID of the item to retrieve</param>
        /// <returns>The item with the specified ID</returns>
        /// <exception cref="ItemNotFoundException">Thrown when no item with the specified ID is found</exception>
        public T GetItemById(int id)
        {
            if (!_items.TryGetValue(id, out T? item))
                throw new ItemNotFoundException($"Item with ID {id} not found in the inventory.");

            return item;
        }

        /// <summary>
        /// Removes an item from the inventory
        /// </summary>
        /// <param name="id">ID of the item to remove</param>
        /// <exception cref="ItemNotFoundException">Thrown when no item with the specified ID is found</exception>
        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ItemNotFoundException($"Item with ID {id} not found in the inventory.");

            _items.Remove(id);
        }

        /// <summary>
        /// Gets all items in the inventory
        /// </summary>
        /// <returns>List of all items</returns>
        public List<T> GetAllItems()
        {
            return _items.Values.ToList();
        }

        /// <summary>
        /// Updates the quantity of an item
        /// </summary>
        /// <param name="id">ID of the item to update</param>
        /// <param name="newQuantity">New quantity value</param>
        /// <exception cref="ItemNotFoundException">Thrown when no item with the specified ID is found</exception>
        /// <exception cref="InvalidQuantityException">Thrown when the new quantity is negative</exception>
        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidQuantityException($"Quantity cannot be negative. Attempted to set quantity to {newQuantity}.");

            if (!_items.TryGetValue(id, out T? item))
                throw new ItemNotFoundException($"Item with ID {id} not found in the inventory.");

            item.Quantity = newQuantity;
        }

        /// <summary>
        /// Gets the total count of items in the inventory
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Checks if an item with the specified ID exists
        /// </summary>
        /// <param name="id">ID to check</param>
        /// <returns>True if item exists, false otherwise</returns>
        public bool ContainsItem(int id)
        {
            return _items.ContainsKey(id);
        }
    }
}

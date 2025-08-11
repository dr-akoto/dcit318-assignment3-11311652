using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceSystem
{
    /// <summary>
    /// Generic repository class for entity storage and retrieval
    /// Provides type-safe operations for any entity type T
    /// </summary>
    /// <typeparam name="T">The entity type to manage</typeparam>
    public class Repository<T>
    {
        /// <summary>
        /// Internal storage for entities using a generic List
        /// </summary>
        private List<T> items;

        /// <summary>
        /// Initializes a new instance of the Repository with an empty list
        /// </summary>
        public Repository()
        {
            items = new List<T>();
        }

        /// <summary>
        /// Adds an item to the repository
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Add(T item)
        {
            if (item != null)
            {
                items.Add(item);
            }
        }

        /// <summary>
        /// Retrieves all items from the repository
        /// </summary>
        /// <returns>A list of all items</returns>
        public List<T> GetAll()
        {
            return new List<T>(items); // Return a copy to prevent external modification
        }

        /// <summary>
        /// Gets the first item that matches the specified predicate
        /// </summary>
        /// <param name="predicate">Function to test each item</param>
        /// <returns>The first matching item or null if no match found</returns>
        public T? GetById(Func<T, bool> predicate)
        {
            return items.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Removes the first item that matches the specified predicate
        /// </summary>
        /// <param name="predicate">Function to test each item</param>
        /// <returns>True if an item was removed, false otherwise</returns>
        public bool Remove(Func<T, bool> predicate)
        {
            var itemToRemove = items.FirstOrDefault(predicate);
            if (itemToRemove != null)
            {
                return items.Remove(itemToRemove);
            }
            return false;
        }

        /// <summary>
        /// Gets the count of items in the repository
        /// </summary>
        public int Count => items.Count;
    }
}

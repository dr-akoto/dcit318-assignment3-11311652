using System;

namespace FinanceSystem
{
    /// <summary>
    /// Custom exception thrown when attempting to add an item with an ID that already exists
    /// </summary>
    public class DuplicateItemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of DuplicateItemException
        /// </summary>
        public DuplicateItemException() : base("Item with this ID already exists in the inventory.") { }

        /// <summary>
        /// Initializes a new instance of DuplicateItemException with a custom message
        /// </summary>
        /// <param name="message">Custom error message</param>
        public DuplicateItemException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of DuplicateItemException with a custom message and inner exception
        /// </summary>
        /// <param name="message">Custom error message</param>
        /// <param name="innerException">Inner exception</param>
        public DuplicateItemException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Custom exception thrown when attempting to access an item that doesn't exist
    /// </summary>
    public class ItemNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of ItemNotFoundException
        /// </summary>
        public ItemNotFoundException() : base("Item not found in the inventory.") { }

        /// <summary>
        /// Initializes a new instance of ItemNotFoundException with a custom message
        /// </summary>
        /// <param name="message">Custom error message</param>
        public ItemNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of ItemNotFoundException with a custom message and inner exception
        /// </summary>
        /// <param name="message">Custom error message</param>
        /// <param name="innerException">Inner exception</param>
        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Custom exception thrown when attempting to set an invalid quantity
    /// </summary>
    public class InvalidQuantityException : Exception
    {
        /// <summary>
        /// Initializes a new instance of InvalidQuantityException
        /// </summary>
        public InvalidQuantityException() : base("Quantity cannot be negative.") { }

        /// <summary>
        /// Initializes a new instance of InvalidQuantityException with a custom message
        /// </summary>
        /// <param name="message">Custom error message</param>
        public InvalidQuantityException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of InvalidQuantityException with a custom message and inner exception
        /// </summary>
        /// <param name="message">Custom error message</param>
        /// <param name="innerException">Inner exception</param>
        public InvalidQuantityException(string message, Exception innerException) : base(message, innerException) { }
    }
}

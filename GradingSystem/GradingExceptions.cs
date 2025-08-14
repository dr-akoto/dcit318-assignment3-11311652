namespace FinanceSystem.GradingSystem
{
    /// <summary>
    /// Exception thrown when a score cannot be converted to an integer
    /// </summary>
    public class InvalidScoreFormatException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidScoreFormatException class
        /// </summary>
        public InvalidScoreFormatException()
            : base("The score format is invalid and cannot be converted to an integer.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidScoreFormatException class with a specified error message
        /// </summary>
        /// <param name="message">The error message</param>
        public InvalidScoreFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidScoreFormatException class with a specified error message and inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public InvalidScoreFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Exception thrown when a required field is missing from the input data
    /// </summary>
    public class MissingFieldException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingFieldException class
        /// </summary>
        public MissingFieldException()
            : base("One or more required fields are missing from the input data.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingFieldException class with a specified error message
        /// </summary>
        /// <param name="message">The error message</param>
        public MissingFieldException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MissingFieldException class with a specified error message and inner exception
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception</param>
        public MissingFieldException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

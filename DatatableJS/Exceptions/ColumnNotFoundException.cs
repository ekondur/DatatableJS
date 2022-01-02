using System;

namespace DatatableJS.Exceptions
{
    /// <summary>
    /// Exception thrown when a column is not found.
    /// </summary>
    public class ColumnNotFoundException : Exception
    {
        /// <summary>
        /// Create a new exception of type ColumnNotFoundException.
        /// </summary>
        public ColumnNotFoundException()
        {
        }

        /// <summary>
        /// Create a new exception of type ColumnNotFoundException.
        /// </summary>
        /// <param name="message"></param>
        public ColumnNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Create a new exception of type ColumnNotFoundException.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ColumnNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

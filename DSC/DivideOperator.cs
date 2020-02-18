using System;
namespace DSC
{
    /// <summary>
    /// Performs a division
    /// </summary>
    public class DivideOperator : IOperator
    {
        /// <summary>
        /// Performs the division. Will throw a DivideByZeroException when a 
        /// division by zero is to be performed.
        /// </summary>
        /// <returns>The result of the operation (lhs / rhs).</returns>
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            if (rhs == 0)
                throw new DivideByZeroException();

            return Math.Round(lhs / rhs, 10);
        }
    }
}
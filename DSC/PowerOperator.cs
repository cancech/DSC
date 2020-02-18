using System;
namespace DSC
{
    /// <summary>
    /// Performs a power operation
    /// </summary>
    public class PowerOperator : IOperator
    {

        /// <summary>
        /// Performs the power operation.
        /// </summary>
        /// <returns>The result of the operation (lhs ^ rhs).</returns>
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return (decimal)Math.Round(Math.Pow((double)lhs, (double)rhs), 10);
        }
    }
}
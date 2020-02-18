namespace DSC
{
    /// <summary>
    /// Performs a subtraction
    /// </summary>
    public class MinusOperator : IOperator
    {

        /// <summary>
        /// Performs the subtraction.
        /// </summary>
        /// <returns>The result of the operation (lhs - rhs).</returns>
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return lhs - rhs;
        }
    }
}

namespace DSC
{
    /// <summary>
    /// Performs a multiplication
    /// </summary>
    public class MultiplyOperator : IOperator
    {

        /// <summary>
        /// Performs the multiplication.
        /// </summary>
        /// <returns>The result of the operation (lhs * rhs).</returns>
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return lhs * rhs;
        }
    }
}

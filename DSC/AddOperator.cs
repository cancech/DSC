namespace DSC
{
    /// <summary>
    /// Performs an addition
    /// </summary>
    public class AddOperator : IOperator
    {

        /// <summary>
        /// Performs the addition.
        /// </summary>
        /// <returns>The result of the operation (lhs + rhs).</returns>
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return lhs + rhs;
        }
    }
}

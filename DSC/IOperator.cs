namespace DSC
{
    /// <summary> 
    /// Interface for the operators that can perform numeric operations
    /// within the calculator.
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// Performs the operation.
        /// </summary>
        /// <returns>The result of the operation.</returns>
        decimal PerformOperation(decimal lhs, decimal rhs);
    }
}

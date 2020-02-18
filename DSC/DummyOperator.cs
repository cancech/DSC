namespace DSC
{
    /// <summary>
    /// Dummy operation to be used as a "null operation" when the user hasn't
    /// specified any operation yet. It will just simply return the current value
    /// </summary>
    public class DummyOperator : IOperator
    {
        /// <summary>
        /// No operation is performed, simply returns the input number.
        /// </summary>
        /// <returns>The input number (rhs).</returns>
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            // When no operation has been specified yet, the LHS in the calculator
            // is as of yet unset (i.e.: 0). Whatever value is currelty input
            // by the user will appear as the RHS
            return rhs;
        }
    }
}

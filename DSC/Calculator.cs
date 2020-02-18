using System;

namespace DSC
{
    /// <summary>
    /// The calculator core. Does very little in of itself, however ties all of
    /// the logic and the operations together. Tracks user inputs (specifically
    /// numbers and operations). 
    /// </summary>
    public class Calculator
    {
        ///<summary> The number currently input by the user </summary>
        public virtual InputNumber Number { get; }
        /// <summary> 
        /// The number to be used for the left hand side of the numeric operation.
        /// </summary>
        private decimal lhs = 0;
        /// <summary>
        /// The numeric operation which is scheduled to be performed.
        /// </summary>
        private IOperator op = new DummyOperator();

        public Calculator() => Number = new InputNumber();

        /// <summary>
        /// Specify what type of operation is to be performed.
        /// </summary>
        /// <param name="op">Operation the calculator is to perform</param>
        public virtual void SetOperator(IOperator op)
        {
            // If there is a chaining of operations happening, trigger the operation first
            PerformOperation();

            this.op = op;
            // Prepare for the input of the rhs
            lhs = Number.ValueDecimal();
            Number.OverrideValue(lhs);
        }

        /// <summary>
        /// Perform the operation on the LHS and RHS numbers
        /// </summary>
        public virtual void PerformOperation()
        {
            try
            {
                // LHS is presently stored, and RHS is the number currently in the buffer
                Number.OverrideValue(op.PerformOperation(lhs, Number.ValueDecimal()));
            }
            catch (Exception)
            {
                // If there was an exception with the input, the calculation failed
                Number.MarkNaN();
            }

            op = new DummyOperator();
        }

        /// <summary>
        /// Clear the inputs and wipe the slate. Return to the original state.
        /// </summary>
        public virtual void Clear()
        {
            Number.Clear();
            op = new DummyOperator();
        }
    }
}

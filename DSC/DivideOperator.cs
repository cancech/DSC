using System;
namespace DSC
{
    public class DivideOperator : IOperator
    {

        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            if (rhs == 0)
                throw new DivideByZeroException();

            return Math.Round(lhs / rhs, 10);
        }
    }
}
using System;
namespace DSC
{
    public class DivideOperator : IOperator
    {

        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            if (rhs == 0)
                return 0; // TODO should do something a bit better with this

            return Math.Round(lhs / rhs, 10);
        }
    }
}
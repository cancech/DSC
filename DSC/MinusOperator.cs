using System;
namespace DSC
{
    public class MinusOperator : IOperator
    {

        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return lhs - rhs;
        }
    }
}

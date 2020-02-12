using System;
namespace DSC
{
    public class DummyOperator : IOperator
    {
        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return rhs;
        }
    }
}

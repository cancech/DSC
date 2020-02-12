using System;
namespace DSC
{
    public class PowerOperator : IOperator
    {

        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return (decimal)Math.Round(Math.Pow((double)lhs, (double)rhs), 10);
        }
    }
}
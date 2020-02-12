namespace DSC
{
    public class MultiplyOperator : IOperator
    {

        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return lhs * rhs;
        }
    }
}

namespace DSC
{
    public class AddOperator : IOperator
    {

        public decimal PerformOperation(decimal lhs, decimal rhs)
        {
            return lhs + rhs;
        }
    }
}

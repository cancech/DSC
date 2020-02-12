using System;
namespace DSC
{
    public interface IOperator
    {
        decimal PerformOperation(decimal lhs, decimal rhs);
    }
}

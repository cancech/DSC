using System;

namespace DSC
{
    public class Calculator
    {
        public virtual InputNumber Number { get; }

        private decimal lhs = 0;

        private IOperator op = new DummyOperator();

        public Calculator() => Number = new InputNumber();

        public virtual void SetOperator(IOperator op)
        {
            // If there is a chaining of operations happening, trigger the operation first
            PerformOperation();

            this.op = op;
            lhs = Number.ValueDecimal();
            Number.Clear();
        }

        public virtual void PerformOperation()
        {
            try
            {
                Number.OverrideValue(op.PerformOperation(lhs, Number.ValueDecimal()));
            }
            catch (Exception)
            {
                Number.MarkNaN();
            }


            op = new DummyOperator();
        }

        public virtual void Clear()
        {
            Number.Clear();
            op = new DummyOperator();
        }
    }
}

namespace DSC
{
    public class OperationCommandProcessor : ICommandProcessor
    {
        private readonly Calculator calculator;

        public OperationCommandProcessor(Calculator calc)
        {
            calculator = calc;
        }

        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Plus, Command.Minus, Command.Multiply,
                Command.Divide, Command.Power };
        }

        public void ProcessCommand(CalcModel model, Command toProcess)
        {
            switch (toProcess)
            {
                case Command.Plus:
                    calculator.SetOperator(new AddOperator());
                    break;
                case Command.Minus:
                    calculator.SetOperator(new MinusOperator());
                    break;
                case Command.Multiply:
                    calculator.SetOperator(new MultiplyOperator());
                    break;
                case Command.Divide:
                    calculator.SetOperator(new DivideOperator());
                    break;
                case Command.Power:
                    calculator.SetOperator(new PowerOperator());
                    break;
            }
        }
    }
}

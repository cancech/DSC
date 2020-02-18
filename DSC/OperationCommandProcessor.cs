namespace DSC
{
    /// <summary>
    /// Command Process for when the user triggers an operation (presses
    /// the +/-/*/'/'/^ button).
    /// </summary>
    public class OperationCommandProcessor : ICommandProcessor
    {
        /// <summary>
        /// The calculator instance we are working with
        /// </summary>
        private readonly Calculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DSC.ClearCommandProcessor"/> class.
        /// </summary>
        /// <param name="calc">The Calculator the operation is to be performed on</param>
        public OperationCommandProcessor(Calculator calc)
        {
            calculator = calc;
        }

        /// <summary>
        /// Provide an array of commands the processor responds to
        /// </summary>
        /// <returns>Array of Commands</returns>
        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Plus, Command.Minus, Command.Multiply,
                Command.Divide, Command.Power };
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="model">Model that triggered the command</param>
        /// <param name="toProcess">Command to process</param>
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

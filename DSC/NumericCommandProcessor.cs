namespace DSC
{
    /// <summary>
    /// Command Process for when the user triggers a numeric input command (presses
    /// a number or number manipulation button - number manipulation meaning delete,
    /// decimal, or sign inversion button).
    /// </summary>
    public class NumericCommandProcessor : ICommandProcessor
    {
        /// <summary>
        /// The calculator instance we are working with
        /// </summary>
        private readonly Calculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DSC.NumericCommandProcessor"/> class.
        /// </summary>
        /// <param name="calc">The Calculator the operation is to be performed on</param>
        public NumericCommandProcessor(Calculator calc)
        {
            this.calculator = calc;
        }

        /// <summary>
        /// Provide an array of commands the processor responds to
        /// </summary>
        /// <returns>Array of Commands</returns>
        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Input0, Command.Input1, Command.Input2,
                Command.Input3, Command.Input4, Command.Input5, Command.Input6,
                Command.Input7, Command.Input8, Command.Input9, Command.Delete,
                    Command.Decimal, Command.InvertSign };
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="model">Model that triggered the command</param>
        /// <param name="toProcess">Command to process</param>
        public void ProcessCommand(CalcModel model, Command toProcess)
        {
            if (toProcess.IsNumeric())
                calculator.Number.AppendDigit((int)toProcess);
            else if (Command.Delete == toProcess)
                calculator.Number.DeleteDigit();
            else if (Command.Decimal == toProcess)
                calculator.Number.AppendDecimalPoint();
            else if (Command.InvertSign == toProcess)
                calculator.Number.InvertSign();

            model.UpdateDisplay(calculator.Number.ValueString());
        }
    }
}

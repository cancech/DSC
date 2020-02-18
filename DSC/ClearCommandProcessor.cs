namespace DSC
{
    /// <summary>
    /// Command Process for when the user triggers a Clear operation (presses
    /// the Clear button).
    /// </summary>
    public class ClearCommandProcessor : ICommandProcessor
    {
        /// <summary>
        /// The calculator instance we are working with
        /// </summary>
        private readonly Calculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DSC.ClearCommandProcessor"/> class.
        /// </summary>
        /// <param name="calc">The Calculator the operation is to be performed on</param>
        public ClearCommandProcessor(Calculator calc)
        {
            calculator = calc;
        }

        /// <summary>
        /// Provide an array of commands the processor responds to
        /// </summary>
        /// <returns>Array of Commands</returns>
        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Clear };
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="model">Model that triggered the command</param>
        /// <param name="toProcess">Command to process</param>
        public void ProcessCommand(CalcModel model, Command toProcess)
        {
            calculator.Clear();
            model.UpdateDisplay(calculator.Number.ValueString());
        }
    }
}

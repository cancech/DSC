namespace DSC
{
    /// <summary>
    /// Command Process for when the user triggers an Equals operation (presses
    /// the Equals button).
    /// </summary>
    public class EqualsCommandProcessor : ICommandProcessor
    {
        /// <summary>
        /// The calculator instance we are working with
        /// </summary>
        private readonly Calculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DSC.EqualsCommandProcessor"/> class.
        /// </summary>
        /// <param name="calc">The Calculator the operation is to be performed on</param>
        public EqualsCommandProcessor(Calculator calc)
        {
            calculator = calc;
        }

        /// <summary>
        /// Provide an array of commands the processor responds to
        /// </summary>
        /// <returns>Array of Commands</returns>
        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Equals };
        }

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="model">Model that triggered the command</param>
        /// <param name="toProcess">Command to process</param>
        public void ProcessCommand(CalcModel model, Command toProcess)
        {
            calculator.PerformOperation();
            model.UpdateDisplay(calculator.Number.ValueString());
        }
    }
}

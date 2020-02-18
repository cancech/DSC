namespace DSC
{
    /// <summary>
    /// Interface for the handlers which are to process commands from the user
    /// </summary>
    public interface ICommandProcessor
    {
        /// <summary>
        /// Provide an array of commands the processor responds to
        /// </summary>
        /// <returns>Array of Commands</returns>
        Command[] CommandsToProcess();

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="model">Model that triggered the command</param>
        /// <param name="toProcess">Command to process</param>
        void ProcessCommand(CalcModel model, Command toProcess);
    }
}

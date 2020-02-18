using System.Collections.Generic;

namespace DSC
{

    /// <summary>
    /// Manager for the processing of commands triggered by the user. It will
    /// direct the command to whichever processor is registered to process the
    /// command. Note: only one processor can handle any single command.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// The model from which user commands will be received.
        /// </summary>
        private readonly CalcModel calcModel;
        /// <summary>
        /// Mapping of Commands to the processor responsible for handling the command.
        /// </summary>
        private readonly Dictionary<Command, ICommandProcessor> commands = new Dictionary<Command, ICommandProcessor>();

        /// <summary>
        /// Creates a new manager for the specified model.
        /// </summary>
        /// <param name="calcModel">Calculator model from where the commands are
        /// to be received.</param>
        public CommandManager(CalcModel calcModel)
        {
            this.calcModel = calcModel;
            calcModel.InputCommand += ProcessInputCommand;
        }

        /// <summary>
        /// Register a new command processor.
        /// </summary>
        /// <param name="processor">Processor to be registered.</param>
        public virtual void RegisterProcessor(ICommandProcessor processor)
        {
            foreach (Command c in processor.CommandsToProcess())
            {
                commands.Add(c, processor);
            }
        }

        /// <summary>
        /// Process the command from the user.
        /// </summary>
        /// <param name="m">Model which triggered the command</param>
        /// <param name="c">Command that was triggered</param>
        public virtual void ProcessInputCommand(CalcModel m, Command c)
        {
            if (commands.ContainsKey(c))
                commands[c].ProcessCommand(m, c);
        }
    }
}

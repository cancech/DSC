using System.Collections.Generic;

namespace DSC
{
    public class CommandManager
    {
        private readonly CalcModel calcModel;
        private readonly Dictionary<Command, ICommandProcessor> commands = new Dictionary<Command, ICommandProcessor>();

        public CommandManager(CalcModel calcModel)
        {
            this.calcModel = calcModel;
            calcModel.InputCommand += ProcessInput;
        }

        public void RegisterCommand(ICommandProcessor processor)
        {
            foreach (Command c in processor.CommandsToProcess())
            {
                commands.Add(c, processor);
            }
        }

        public void ProcessInput(CalcModel m, Command c)
        {
            if (commands.ContainsKey(c))
                commands[c].ProcessCommand(m, c);
        }
    }
}

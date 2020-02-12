using System;
namespace DSC
{
    public interface ICommandProcessor
    {
        Command[] CommandsToProcess();

        void ProcessCommand(CalcModel model, Command toProcess);
    }
}

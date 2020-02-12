using System;
namespace DSC
{
    public class ClearCommandProcessor : ICommandProcessor
    {
        private readonly Calculator calculator;

        public ClearCommandProcessor(Calculator calc)
        {
            calculator = calc;
        }

        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Clear };
        }

        public void ProcessCommand(CalcModel model, Command toProcess)
        {
            calculator.Clear();
            model.UpdateDisplay(calculator.Number.ValueString());
        }
    }
}

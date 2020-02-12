using System;
namespace DSC
{
    public class EqualsCommandProcessor : ICommandProcessor
    {
        private readonly Calculator calculator;

        public EqualsCommandProcessor(Calculator calc)
        {
            calculator = calc;
        }

        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Equals };
        }

        public void ProcessCommand(CalcModel model, Command toProcess)
        {
            calculator.PerformOperation();
            model.UpdateDisplay(calculator.Number.ValueString());
        }
    }
}

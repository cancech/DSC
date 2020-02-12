namespace DSC
{
    public class NumericCommandProcessor : ICommandProcessor
    {
        private readonly Calculator calculator;

        public NumericCommandProcessor(Calculator calculator)
        {
            this.calculator = calculator;
        }

        public Command[] CommandsToProcess()
        {
            return new Command[] { Command.Input0, Command.Input1, Command.Input2,
                Command.Input3, Command.Input4, Command.Input5, Command.Input6,
                Command.Input7, Command.Input8, Command.Input9, Command.Delete,
                    Command.Decimal, Command.InvertSign };
        }

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

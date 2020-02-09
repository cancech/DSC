namespace DSC
{
    public class Calculator
    {
        private readonly CalcModel calcModel;

        public Calculator(CalcModel calcModel)
        {
            this.calcModel = calcModel;
            calcModel.InputCommand += ProcessInput;
        }

        private void ProcessInput(CalcModel m, Command c)
        {
            calcModel.UpdateDisplay(c.ToString());
        }
    }
}

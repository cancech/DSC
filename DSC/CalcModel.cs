namespace DSC
{
    public class CalcModel
    {

        public CalcModel()
        {
            // Add dummy listeners to prevent crashes
            Display += (m, s) => { };
            InputCommand += (m, c) => { };
        }

        public delegate void DisplayHandler(CalcModel m, string s);
        public event DisplayHandler Display;
        public void UpdateDisplay(string s) => Display(this, s);

        public delegate void UserInput(CalcModel m, Command c);
        public event UserInput InputCommand;
        public void TriggerInput(Command c) => InputCommand(this, c);
    }
}

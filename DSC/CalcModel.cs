namespace DSC
{
    /// <summary>
    /// Model for interfacing between the UI and the logic of the calculator.
    /// This allows for a clear separation between the two.
    /// </summary>
    public class CalcModel
    {

        public CalcModel()
        {
            // Add dummy listeners to prevent crashes
            Display += (m, s) => { };
            InputCommand += (m, c) => { };
        }

        /// <summary>
        /// The delegate which is to be triggered when the calculator display
        /// is to be updated.
        /// </summary>
        /// <param name="m">CalcModel which is triggering the update</param>
        /// <param name="s">string which is to be displayed on the display</param>
        public delegate void DisplayHandler(CalcModel m, string s);

        /// <summary>
        /// Event responsible for updating the display.
        /// </summary>
        public event DisplayHandler Display;

        /// <summary>
        /// Update the display with the specified text.
        /// </summary>
        /// <param name="s">The text which is to be shown on the display</param>
        public virtual void UpdateDisplay(string s) => Display(this, s);


        /// <summary>
        /// The delegate which is to be triggered when the user interacts with
        /// the UI and presses a button.
        /// </summary>
        /// <param name="m">CalcModel triggered by the user action</param>
        /// <param name="c">Command that the user triggered</param>
        public delegate void UserInput(CalcModel m, Command c);

        /// <summary>
        /// Event responsible for propagating the user command.
        /// </summary>
        public event UserInput InputCommand;

        /// <summary>
        /// Trigger the model to perform the specified user command.
        /// </summary>
        /// <param name="c">Command that the user wants to perform</param>
        public void TriggerInput(Command c) => InputCommand(this, c);
    }
}

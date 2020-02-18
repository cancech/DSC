using Gtk;

namespace DSC
{
    /// <summary>
    /// The main entry point for the calculator. Performs the necessary assembly
    /// to ensure that the program as a whole will work.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Creates a new Program instance
        /// </summary>
        /// <param name="model">Model to use to facilitate the interaction between 
        /// the UI and the logic</param>
        public Program(CalcModel model)
        {
            // The calculator core
            Calculator cal = new Calculator();
            RegisterCommandProcessors(cal, model);

        }

        /// <summary>
        /// Registers the command processors.
        /// </summary>
        /// <param name="cal">Calulator the processors are to manipulate.</param>
        /// <param name="model">Model for the communication between the UI and logic</param>
        private void RegisterCommandProcessors(Calculator cal, CalcModel model)
        {
            CommandManager manager = new CommandManager(model);
            manager.RegisterProcessor(new NumericCommandProcessor(cal));
            manager.RegisterProcessor(new OperationCommandProcessor(cal));
            manager.RegisterProcessor(new EqualsCommandProcessor(cal));
            manager.RegisterProcessor(new ClearCommandProcessor(cal));
        }

        /// <summary>
        /// The entry point of the program, creates the calculator logic and UI
        /// and creates the interface between the two halfs.
        /// </summary>
        public static void Main(string[] args)
        {
            // The model which is to be shared
            CalcModel calcModel = new CalcModel();
            Program program = new Program(calcModel);

            // Create and display the UI
            Application.Init();
            MainWindow win = new MainWindow(calcModel);
            win.Show();
            Application.Run();
        }
    }
}

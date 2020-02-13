using Gtk;

namespace DSC
{
    public class Program
    {

        public Program(CalcModel model)
        {
            // The calculator core
            Calculator cal = new Calculator();
            RegisterCommandProcessors(cal, model);

        }

        private void RegisterCommandProcessors(Calculator cal, CalcModel model)
        {
            CommandManager manager = new CommandManager(model);
            manager.RegisterCommand(new NumericCommandProcessor(cal));
            manager.RegisterCommand(new OperationCommandProcessor(cal));
            manager.RegisterCommand(new EqualsCommandProcessor(cal));
            manager.RegisterCommand(new ClearCommandProcessor(cal));
        }

        public static void Main(string[] args)
        {
            // The model which is to be shared
            CalcModel calcModel = new CalcModel();
            Program program = new Program(calcModel);

            Application.Init();
            MainWindow win = new MainWindow(calcModel);
            win.Show();
            Application.Run();
        }
    }
}

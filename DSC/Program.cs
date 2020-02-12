using Gtk;

namespace DSC
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // The model which is to be shared
            CalcModel calcModel = new CalcModel();

            // The calculator core
            Calculator cal = new Calculator();
            RegisterCommandProcessors(cal, calcModel);

            Application.Init();
            MainWindow win = new MainWindow(calcModel);
            win.Show();
            Application.Run();
        }

        private static void RegisterCommandProcessors(Calculator cal, CalcModel model)
        {
            CommandManager manager = new CommandManager(model);
            manager.RegisterCommand(new NumericCommandProcessor(cal));
            manager.RegisterCommand(new OperationCommandProcessor(cal));
            manager.RegisterCommand(new EqualsCommandProcessor(cal));
        }
    }
}

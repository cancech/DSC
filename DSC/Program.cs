using Gtk;

namespace DSC
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CalcModel calcModel = new CalcModel();
            Calculator cal = new Calculator(calcModel);

            Application.Init();
            MainWindow win = new MainWindow(calcModel);
            win.Show();
            Application.Run();
        }
    }
}

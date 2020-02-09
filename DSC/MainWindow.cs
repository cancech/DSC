using System;
using System.Collections.Generic;
using DSC;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    private readonly CalcModel calcModel;
    private Dictionary<Button, Command> commandDict = new Dictionary<Button, Command>();

    public MainWindow(CalcModel calcModel) : base(Gtk.WindowType.Toplevel)
    {
        this.calcModel = calcModel;

        Build();
        CustomizeGui();
        RegisterListeners();
        BuildCommandDictionary();
    }

    private void CustomizeGui()
    {
        // Update the LAF of the output label
        Pango.FontDescription fontDesc = new Pango.FontDescription
        {
            Size = Convert.ToInt32(24 * Pango.Scale.PangoScale)
        };
        outputLabel.ModifyFont(fontDesc);

        btnTable.Foreach(CustomizeBtn);
    }

    private void CustomizeBtn(Widget widget)
    {
        // Only care about the buttons
        if (!(widget is Button))
            return;

        Button btn = (Button)widget;
        btn.HasFocus = false;
        btn.FocusOnClick = false;
        btn.CanFocus = false;
    }

    private void RegisterListeners()
    {
        calcModel.Display += UpdateDisplay;
    }

    private void UpdateDisplay(CalcModel m, string s)
    {
        outputLabel.Text = s;
    }

    private void BuildCommandDictionary()
    {
        commandDict.Add(BtnClear, Command.Clear);
        commandDict.Add(BtnDel, Command.Delete);
        commandDict.Add(BtnPwr, Command.Power);
        commandDict.Add(BtnDivide, Command.Divide);
        commandDict.Add(BtnMultiply, Command.Multiply);
        commandDict.Add(BtnMinus, Command.Minus);
        commandDict.Add(BtnPlus, Command.Plus);
        commandDict.Add(BtnEquals, Command.Equals);
        commandDict.Add(BtnSign, Command.InvertSign);
        commandDict.Add(BtnDecimalPt, Command.Decimal);
        commandDict.Add(Btn1, Command.Input1);
        commandDict.Add(Btn2, Command.Input2);
        commandDict.Add(Btn3, Command.Input3);
        commandDict.Add(Btn4, Command.Input4);
        commandDict.Add(Btn5, Command.Input5);
        commandDict.Add(Btn6, Command.Input6);
        commandDict.Add(Btn7, Command.Input7);
        commandDict.Add(Btn8, Command.Input8);
        commandDict.Add(Btn9, Command.Input9);
        commandDict.Add(BtnZero, Command.Input0);
    }

    protected void OnBtnClicked(object sender, EventArgs e)
    {
        calcModel.TriggerInput(commandDict[(Button)sender]);
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}

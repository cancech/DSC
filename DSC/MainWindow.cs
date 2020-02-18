using System;
using System.Collections.Generic;
using DSC;
using Gtk;

/// <summary>
/// The non-generated portion of the MainWindow
/// </summary>
public partial class MainWindow : Gtk.Window
{
    /// <summary>
    /// The model with which to interface with the logic.
    /// </summary>
    private readonly CalcModel calcModel;
    /// <summary>
    /// Mapping of buttons to the command that they trigger.
    /// </summary>
    private Dictionary<Button, Command> commandDict = new Dictionary<Button, Command>();

    /// <summary>
    /// Create the UI and initialized the event handlers.
    /// </summary>
    /// <param name="calcModel">Calculate model.</param>
    public MainWindow(CalcModel calcModel) : base(Gtk.WindowType.Toplevel)
    {
        this.calcModel = calcModel;

        Build();
        CustomizeGui();
        RegisterListeners();
        BuildCommandDictionary();
    }

    /// <summary>
    /// Perform customizations of the UI elements above and beyond what the UI
    /// Designer allows for.
    /// </summary>
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

    /// <summary>
    /// Customize the LAF and behavior of the specified button.
    /// </summary>
    /// <param name="widget">Widget representing the button to customize.</param>
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

    /// <summary>
    /// Register the event listeners for updates from the logic
    /// </summary>
    private void RegisterListeners()
    {
        calcModel.Display += UpdateDisplay;
    }

    /// <summary>
    /// Updates the calculator display with the values disctated by the logic.
    /// </summary>
    /// <param name="m">Model which triggered the update</param>
    /// <param name="s">string which is to be displayed to the user</param>
    private void UpdateDisplay(CalcModel m, string s)
    {
        outputLabel.Text = s;
    }

    /// <summary>
    /// Build the dictionaty of button to command mappings.
    /// </summary>
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

    /// <summary>
    /// The event which is triggered when the user clicks on a button.
    /// </summary>
    /// <param name="sender">The widget object which triggered the event.</param>
    /// <param name="e">The arguments of the event</param>
    protected void OnBtnClicked(object sender, EventArgs e)
    {
        calcModel.TriggerInput(commandDict[(Button)sender]);
    }

    /// <summary>
    /// Destroys the UI.
    /// </summary>
    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}

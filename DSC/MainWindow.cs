using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        Pango.FontDescription fontDesc = new Pango.FontDescription
        {
            Size = Convert.ToInt32(24 * Pango.Scale.PangoScale)
        };
        outputLabel.ModifyFont(fontDesc);
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}

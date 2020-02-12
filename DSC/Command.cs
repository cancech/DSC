

using System.Collections.Generic;

namespace DSC
{
    public enum Command
    {
        Input0,
        Input1,
        Input2,
        Input3,
        Input4,
        Input5,
        Input6,
        Input7,
        Input8,
        Input9,
        Clear,
        Delete,
        Power,
        Divide,
        Multiply,
        Minus,
        Plus,
        Equals,
        Decimal,
        InvertSign
    };

    public static class Extensions
    {
        public static bool IsNumeric(this Command command)
        {
            return Command.Input0 <= command && command <= Command.Input9;
        }
    }
}

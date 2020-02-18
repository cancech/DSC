namespace DSC
{
    /// <summary>
    /// Enumeration of all commands available to the user
    /// </summary>
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
        /// <summary>
        /// Check if the command corresponds to a numeric input
        /// </summary>
        /// <returns><c>true</c>, if the command corresponds to a number input
        /// by the user</returns>
        /// <param name="command">Command to check</param>
        public static bool IsNumeric(this Command command)
        {
            return Command.Input0 <= command && command <= Command.Input9;
        }
    }
}

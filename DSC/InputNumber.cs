using System;

namespace DSC
{
    public class InputNumber
    {
        private string input;
        private bool isDecimal;
        private bool isPositive;
        private int signFactor;

        public InputNumber()
        {
            // Clear will reset all values to their expected initial state
            Clear();
        }

        public void AppendDigit(int digit)
        {
            //Don't append leading 0's
            if (digit == 0 && IsEmpty())
                return;

            input += digit;
        }

        public void AppendDecimalPoint()
        {
            // Ignore subsequent decimal inputs. Only care about the first one
            if (isDecimal)
                return;

            input += ".";
            isDecimal = true;
        }

        public void InvertSign()
        {
            isPositive = !isPositive;
            signFactor = signFactor * -1;
        }

        public void DeleteDigit()
        {
            // Only delete if there is something to delete
            if (IsEmpty())
                return;

            String removed = input.Substring(input.Length - 1);
            input = input.Remove(input.Length - 1);

            // Check if the element removed is the decimal point
            if (removed.Equals("."))
                isDecimal = false;
        }

        public void Clear()
        {
            input = "";
            isDecimal = false;
            isPositive = true;
            signFactor = 1;
        }

        public string ValueString()
        {
            string toReturn = input;

            // Massage the resulting string
            if (IsEmpty())
                toReturn = "0";
            else if (input.StartsWith(".", StringComparison.Ordinal))
                toReturn = 0 + toReturn;
            if (!isPositive)
                toReturn = "-" + toReturn;

            return toReturn;
        }

        public bool IsDecimal()
        {
            return isDecimal;
        }

        public int ValueInt()
        {
            if (isDecimal || IsEmpty())
                return 0;

            return Int32.Parse(input) * signFactor;
        }

        public double ValueDouble()
        {
            if (!isDecimal || IsEmpty() || input.Equals("."))
                return 0.0;

            return Double.Parse(input) * signFactor;
        }

        private bool IsEmpty()
        {
            return input.Equals("");
        }
    }
}

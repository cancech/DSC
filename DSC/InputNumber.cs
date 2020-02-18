using System;

namespace DSC
{
    /// <summary>
    /// Class which assembles and updates the number input by the user.
    /// </summary>
    public class InputNumber
    {
        /// <summary>
        /// The current input from the user.
        /// </summary>
        private string input;
        /// <summary>
        /// Flag for whether or not a decimal number has been input (i.e.: the 
        /// decimal point has been included within the number).
        /// </summary>
        private bool isDecimal;
        /// <summary>
        /// Flag for whether or not this is a positive number.
        /// </summary>
        private bool isPositive;
        /// <summary>
        /// Flag for whether the input is temporarily overridden. A temporarily
        /// overridden number be cleared as soon as any form of numerical input
        /// is performed, but will retain the value of the override until then.
        /// </summary>
        private bool isTempOverride;
        /// <summary>
        /// Flag for whether or not the input number is invalid (not a number).
        /// </summary>
        private bool isNan;

        // Clear will reset all values to their expected initial state
        public InputNumber() => Clear();

        /// <summary>
        /// Reset the number fo a positive 0, with no decimal point.
        /// </summary>
        public virtual void Clear()
        {
            input = "";
            isDecimal = false;
            isPositive = true;
            isTempOverride = false;
            isNan = false;
        }

        /// <summary>
        /// Appends the specified digit to the right of the number (number is
        /// input left to right.
        /// </summary>
        /// <param name="digit">Digit to append to the input number</param>
        public virtual void AppendDigit(int digit)
        {
            CheckForOverride();

            //Don't append leading 0's
            if (digit == 0 && IsEmpty())
                return;

            input += digit;
        }

        /// <summary>
        /// Append a decimal point to the (right of) the number. Note only a single
        /// decimal point can be in a number, so only the first will be applied.
        /// Any subsequent decimal point input will only be accepted if the original
        /// decimal point is deletec by the user (or the whole number cleared).
        /// </summary>
        public virtual void AppendDecimalPoint()
        {
            CheckForOverride();

            // Ignore subsequent decimal inputs. Only care about the first one
            if (isDecimal)
                return;

            input += ".";
            isDecimal = true;
        }

        /// <summary>
        /// Inver the sign of the number (positive to negative and vice versa).
        /// </summary>
        public virtual void InvertSign()
        {
            CheckForOverride();

            isPositive = !isPositive;
        }

        /// <summary>
        /// Delete the right most digit (or decimal point if that is the right most
        /// portion of the number).
        /// </summary>
        public virtual void DeleteDigit()
        {
            CheckForOverride();

            // Only delete if there is something to delete
            if (IsEmpty())
                return;

            String removed = input.Substring(input.Length - 1);
            input = input.Remove(input.Length - 1);

            // Check if the element removed is the decimal point
            if (removed.Equals("."))
                isDecimal = false;
        }

        /// <summary>
        /// Mark the number as "not a number". Numerically a NaN will have the value
        /// of 0, however it will display "NaN" to the user.
        /// </summary>
        public virtual void MarkNaN()
        {
            Clear();
            isTempOverride = true;
            isNan = true;
        }

        /// <summary>
        /// Override the input with the specified value. This override will remain
        /// in place until the user performs any numeric update, at which point
        /// the overridden value will be cleared away.
        /// </summary>
        /// <param name="value">Value to override the input with</param>
        public virtual void OverrideValue(decimal value)
        {
            Clear();
            isTempOverride = true;

            if (value == 0)
            {
                // Special case if the result is exactly 0
                input = "0";
                return;
            }

            if (value < 0)
            {
                isPositive = false;
            }

            input = Math.Abs(value).ToString();
            isDecimal = input.Contains(".");
        }

        /// <summary>
        /// Retrieve the value of the input number as a string.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public virtual string ValueString()
        {
            // NaN is a special case
            if (isNan)
                return "NaN";

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

        /// <summary>
        /// Check whether or not the input value is decimal (i.e.: not an integer).
        /// </summary>
        /// <returns><c>true</c>, if the number comtains a decimal point.</returns>
        public virtual bool IsDecimal()
        {
            return isDecimal;
        }

        /// <summary>
        /// Retrieve the numerical value of the number as a decimal.
        /// </summary>
        /// <returns>The value as a decimal.</returns>
        public virtual decimal ValueDecimal()
        {
            if (IsEmpty() || input.Equals(".") || isNan)
                return 0.0M;

            // Retrieve the value and ensure it has the proper sign
            decimal toReturn = decimal.Parse(input);
            return isPositive ? toReturn : toReturn * -1;
        }

        /// <summary>
        /// Check whether the user has not yet input any digits for the number.
        /// </summary>
        /// <returns><c>true</c>, no digit has been entered yet by the user.</returns>
        private bool IsEmpty()
        {
            return input.Equals("");
        }

        /// <summary>
        /// Check whether or not a value override is in place. If it is, the
        /// override will be cleared away.
        /// </summary>
        private void CheckForOverride()
        {
            if (!isTempOverride)
                return;

            Clear();
            isTempOverride = false;
        }
    }
}

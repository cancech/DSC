﻿using System;

namespace DSC
{
    public class InputNumber
    {
        private string input;
        private bool isDecimal;
        private bool isPositive;
        private int signFactor;
        private bool isTempOverride;

        // Clear will reset all values to their expected initial state
        public InputNumber() => Clear();

        public virtual void AppendDigit(int digit)
        {
            CheckForOverride();

            //Don't append leading 0's
            if (digit == 0 && IsEmpty())
                return;

            input += digit;
        }

        public virtual void AppendDecimalPoint()
        {
            CheckForOverride();

            // Ignore subsequent decimal inputs. Only care about the first one
            if (isDecimal)
                return;

            input += ".";
            isDecimal = true;
        }

        public virtual void InvertSign()
        {
            CheckForOverride();

            isPositive = !isPositive;
            signFactor = signFactor * -1;
        }

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

        public virtual void OverrideValue(decimal value)
        {
            isTempOverride = true;

            if (value == 0)
            {
                // Special case if the result is exactly 0
                input = "0";
                isPositive = true;
                signFactor = 1;
                return;
            }

            if (value < 0)
            {
                isPositive = false;
                signFactor = -1;
            }
            else
            {
                isPositive = true;
                signFactor = 1;
            }

            input = Math.Abs(value).ToString();
            isDecimal = input.Contains(".");
        }

        public virtual void Clear()
        {
            input = "";
            isDecimal = false;
            isPositive = true;
            isTempOverride = false;
            signFactor = 1;
        }

        public virtual string ValueString()
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

        public virtual bool IsDecimal()
        {
            return isDecimal;
        }

        public virtual decimal ValueDecimal()
        {
            if (IsEmpty() || input.Equals("."))
                return 0.0M;

            return Decimal.Parse(input) * signFactor;
        }

        private bool IsEmpty()
        {
            return input.Equals("");
        }

        private void CheckForOverride()
        {
            if (!isTempOverride)
                return;

            Clear();
            isTempOverride = false;
        }
    }
}

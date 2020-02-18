using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture()]
    public class InputNumberTest
    {

        private InputNumber input;


        [SetUp]
        public void Setup()
        {
            input = new InputNumber();
        }

        [Test]
        public void TestBaseNumber()
        {
            AssertValue("0", 0.0M, false);
        }

        [Test]
        public void TestLeadingZeros()
        {
            // First try just the one
            input.AppendDigit(0);
            AssertValue("0", 0.0M, false);

            // Now try a bunch
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            input.AppendDigit(0);
            AssertValue("0", 0.0M, false);
        }

        [Test]
        public void TestIntInput()
        {
            input.AppendDigit(1);
            AssertValue("1", 1.0M, false);
            input.AppendDigit(2);
            AssertValue("12", 12.0M, false);
            input.AppendDigit(3);
            AssertValue("123", 123.0M, false);
            input.AppendDigit(4);
            AssertValue("1234", 1234.0M, false);
            input.AppendDigit(5);
            AssertValue("12345", 12345.0M, false);
        }

        [Test]
        public void TestFollowingZeros()
        {
            input.AppendDigit(9);
            AssertValue("9", 9.0M, false);
            input.AppendDigit(0);
            AssertValue("90", 90.0M, false);
            input.AppendDigit(0);
            AssertValue("900", 900.0M, false);
            input.AppendDigit(0);
            AssertValue("9000", 9000.0M, false);
            input.AppendDigit(0);
            AssertValue("90000", 90000.0M, false);
        }

        [Test]
        public void TestDeleteInt()
        {
            // Check that nothing happens if were delete when nothing is present
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);

            // Add one digit and delete it
            input.AppendDigit(6);
            AssertValue("6", 6.0M, false);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
            // Still nothing happens if we delete "past" all digits
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);

            // Now add/delete a bunch of things
            input.AppendDigit(7);
            AssertValue("7", 7.0M, false);
            input.AppendDigit(8);
            AssertValue("78", 78.0M, false);
            input.AppendDigit(9);
            AssertValue("789", 789.0M, false);
            input.DeleteDigit();
            AssertValue("78", 78.0M, false);
            input.AppendDigit(0);
            AssertValue("780", 780.0M, false);
            input.DeleteDigit();
            AssertValue("78", 78.0M, false);
            input.AppendDigit(4);
            AssertValue("784", 784.0M, false);
            input.AppendDigit(1);
            AssertValue("7841", 7841.0M, false);
            input.DeleteDigit();
            AssertValue("784", 784.0M, false);
            input.DeleteDigit();
            AssertValue("78", 78.0M, false);
            input.DeleteDigit();
            AssertValue("7", 7.0M, false);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
        }

        [Test]
        public void TestDecimal()
        {
            input.AppendDigit(5);
            AssertValue("5", 5.0M, false);
            input.AppendDecimalPoint();
            AssertValue("5.", 5.0M, true);
            input.AppendDigit(5);
            AssertValue("5.5", 5.5M, true);
            input.AppendDigit(5);
            AssertValue("5.55", 5.55M, true);
            input.AppendDigit(5);
            AssertValue("5.555", 5.555M, true);
        }

        [Test]
        public void TestDeleteDecimal()
        {
            // Input some number
            input.AppendDigit(8);
            AssertValue("8", 8.0M, false);
            input.AppendDecimalPoint();
            AssertValue("8.", 8.0M, true);
            input.AppendDigit(1);
            AssertValue("8.1", 8.1M, true);
            input.AppendDigit(6);
            AssertValue("8.16", 8.16M, true);

            // Now delete!
            input.DeleteDigit();
            AssertValue("8.1", 8.1M, true);
            input.DeleteDigit();
            AssertValue("8.", 8.0M, true);
            input.DeleteDigit();
            AssertValue("8", 8.0M, false);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
        }

        [Test]
        public void TestClear()
        {
            // Get some integer input in place
            TestIntInput();
            AssertValue("12345", 12345.0M, false);
            // Clear it
            input.Clear();
            AssertValue("0", 0.0M, false);

            // Get some decimal in place
            TestDecimal();
            AssertValue("5.555", 5.555M, true);
            // Clear it
            input.Clear();
            AssertValue("0", 0.0M, false);
        }

        [Test]
        public void TestInvertSignNoInput()
        {
            AssertValue("0", 0.0M, false);
            input.InvertSign();
            AssertValue("-0", 0.0M, false);
            input.InvertSign();
            AssertValue("0", 0.0M, false);
        }

        [Test]
        public void TestInvertSignInt()
        {
            input.InvertSign();
            AssertValue("-0", 0.0M, false);
            input.AppendDigit(7);
            AssertValue("-7", -7.0M, false);
            input.AppendDigit(3);
            AssertValue("-73", -73.0M, false);
            input.InvertSign();
            AssertValue("73", 73.0M, false);
        }

        [Test]
        public void TestClearNegativeInt()
        {
            // Get some integer input in place
            TestIntInput();
            AssertValue("12345", 12345.0M, false);
            input.InvertSign();
            AssertValue("-12345", -12345.0M, false);
            // Clear it
            input.Clear();
            AssertValue("0", 0.0M, false);

            // Make sure that it "stays" positive
            input.AppendDigit(9);
            AssertValue("9", 9.0M, false);
        }

        [Test]
        public void TestClearNegativeDecimal()
        {
            // Get some decimal in place
            TestDecimal();
            AssertValue("5.555", 5.555M, true);
            input.InvertSign();
            AssertValue("-5.555", -5.555M, true);

            // Clear it
            input.Clear();
            AssertValue("0", 0.0M, false);

            // Make sure that it "stays" positive
            input.AppendDigit(8);
            AssertValue("8", 8.0M, false);
        }

        [Test]
        public void TestFirstInputDecimalPoint()
        {
            // Number can start with just the decimal point
            input.AppendDecimalPoint();
            AssertValue("0.", 0.0M, true);
            input.AppendDigit(4);
            AssertValue("0.4", 0.4M, true);

            // Can delete everything properly
            input.DeleteDigit();
            AssertValue("0.", 0.0M, true);
            input.DeleteDigit();
            AssertValue("0", 0.0M, false);
        }

        [TestCase(123, false)]
        [TestCase(-38.48, true)]
        public void TestOverrideValue(decimal val, bool isDecimal)
        {
            input.OverrideValue(val);
            AssertValue(val.ToString(), val, isDecimal);
        }

        [Test]
        public void TestOverrideValueNumberInput()
        {
            TestOverrideValue(574.594M, true);
            input.AppendDigit(7);

            AssertValue("7", 7M, false);
        }

        [Test]
        public void TestOverrideValueDecimalInput()
        {
            TestOverrideValue(847M, false);
            input.AppendDecimalPoint();

            AssertValue("0.", 0M, true);
        }

        [Test]
        public void TestOverrideNegValueInvertSign()
        {
            TestOverrideValue(-295M, false);
            input.InvertSign();

            AssertValue("-0", 0M, false);
        }

        [Test]
        public void TestOverridePosValueInvertSign()
        {
            TestOverrideValue(9847.20M, true);
            input.InvertSign();

            AssertValue("-0", 0M, false);
        }

        [Test]
        public void TestOverrideValueClear()
        {
            TestOverrideValue(-4903, false);
            input.Clear();
            AssertValue("0", 0M, false);

            // Make sure that the override flag has cleared and can now manipulate the number
            input.AppendDigit(8);
            AssertValue("8", 8M, false);
        }

        [Test]
        public void TestOverrideValueDeleteDigit()
        {
            TestOverrideValue(5830.483M, true);
            input.DeleteDigit();

            AssertValue("0", 0M, false);
        }

        [Test]
        public void TestOverrideValueZero()
        {
            input.OverrideValue(0M);
            AssertValue("0", 0M, false);
            input.OverrideValue(0.0M);
            AssertValue("0", 0M, false);
            input.OverrideValue(0.0000000M);
            AssertValue("0", 0M, false);
        }

        [Test]
        public void TestMarkNan()
        {
            // First make the number NaN
            input.MarkNaN();
            AssertValue("NaN", 0M, false);
        }

        [Test]
        public void TestClearNaN()
        {
            TestMarkNan();

            // Then make sure that it can be cleared
            input.Clear();
            AssertValue("0", 0M, false);
        }

        [Test]
        public void TestNumberFromNaN()
        {
            TestMarkNan();
            TestIntInput();
        }

        [Test]
        public void TestOverrideFromNaN()
        {
            TestMarkNan();
            TestOverrideValueNumberInput();
        }

        /// <summary>
        /// Verify that the number is properly updated as per expectations
        /// </summary>
        /// <param name="txt">The expected string value of the number</param>
        /// <param name="decVal">The expected decimal value of the number</param>
        /// <param name="isDecimal">The expected decimal state of the number</param>
        private void AssertValue(string txt, decimal decVal, bool isDecimal)
        {
            Assert.AreEqual(txt, input.ValueString());
            Assert.AreEqual(decVal, input.ValueDecimal());
            Assert.AreEqual(isDecimal, input.IsDecimal());
        }
    }
}
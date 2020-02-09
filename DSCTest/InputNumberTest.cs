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
            AssertValue("0", 0, 0.0, false);
        }

        [Test]
        public void TestLeadingZeros()
        {
            // First try just the one
            input.AppendDigit(0);
            AssertValue("0", 0, 0.0, false);

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
            AssertValue("0", 0, 0.0, false);
        }

        [Test]
        public void TestIntInput()
        {
            input.AppendDigit(1);
            AssertValue("1", 1, 0.0, false);
            input.AppendDigit(2);
            AssertValue("12", 12, 0.0, false);
            input.AppendDigit(3);
            AssertValue("123", 123, 0.0, false);
            input.AppendDigit(4);
            AssertValue("1234", 1234, 0.0, false);
            input.AppendDigit(5);
            AssertValue("12345", 12345, 0.0, false);
        }

        [Test]
        public void TestFollowingZeros()
        {
            input.AppendDigit(9);
            AssertValue("9", 9, 0.0, false);
            input.AppendDigit(0);
            AssertValue("90", 90, 0.0, false);
            input.AppendDigit(0);
            AssertValue("900", 900, 0.0, false);
            input.AppendDigit(0);
            AssertValue("9000", 9000, 0.0, false);
            input.AppendDigit(0);
            AssertValue("90000", 90000, 0.0, false);
        }

        [Test]
        public void TestDeleteInt()
        {
            // Check that nothing happens if were delete when nothing is present
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);

            // Add one digit and delete it
            input.AppendDigit(6);
            AssertValue("6", 6, 0.0, false);
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);
            // Still nothing happens if we delete "past" all digits
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);

            // Now add/delete a bunch of things
            input.AppendDigit(7);
            AssertValue("7", 7, 0.0, false);
            input.AppendDigit(8);
            AssertValue("78", 78, 0.0, false);
            input.AppendDigit(9);
            AssertValue("789", 789, 0.0, false);
            input.DeleteDigit();
            AssertValue("78", 78, 0.0, false);
            input.AppendDigit(0);
            AssertValue("780", 780, 0.0, false);
            input.DeleteDigit();
            AssertValue("78", 78, 0.0, false);
            input.AppendDigit(4);
            AssertValue("784", 784, 0.0, false);
            input.AppendDigit(1);
            AssertValue("7841", 7841, 0.0, false);
            input.DeleteDigit();
            AssertValue("784", 784, 0.0, false);
            input.DeleteDigit();
            AssertValue("78", 78, 0.0, false);
            input.DeleteDigit();
            AssertValue("7", 7, 0.0, false);
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);
        }

        [Test]
        public void TestDecimal()
        {
            input.AppendDigit(5);
            AssertValue("5", 5, 0.0, false);
            input.AppendDecimalPoint();
            AssertValue("5.", 0, 5.0, true);
            input.AppendDigit(5);
            AssertValue("5.5", 0, 5.5, true);
            input.AppendDigit(5);
            AssertValue("5.55", 0, 5.55, true);
            input.AppendDigit(5);
            AssertValue("5.555", 0, 5.555, true);
        }

        [Test]
        public void TestDeleteDecimal()
        {
            // Input some number
            input.AppendDigit(8);
            AssertValue("8", 8, 0, false);
            input.AppendDecimalPoint();
            AssertValue("8.", 0, 8.0, true);
            input.AppendDigit(1);
            AssertValue("8.1", 0, 8.1, true);
            input.AppendDigit(6);
            AssertValue("8.16", 0, 8.16, true);

            // Now delete!
            input.DeleteDigit();
            AssertValue("8.1", 0, 8.1, true);
            input.DeleteDigit();
            AssertValue("8.", 0, 8.0, true);
            input.DeleteDigit();
            AssertValue("8", 8, 0, false);
            input.DeleteDigit();
            AssertValue("0", 0, 0, false);
        }

        [Test]
        public void TestClear()
        {
            // Get some integer input in place
            TestIntInput();
            AssertValue("12345", 12345, 0.0, false);
            // Clear it
            input.Clear();
            AssertValue("0", 0, 0.0, false);

            // Get some decimal in place
            TestDecimal();
            AssertValue("5.555", 0, 5.555, true);
            // Clear it
            input.Clear();
            AssertValue("0", 0, 0.0, false);
        }

        [Test]
        public void TestInvertSignNoInput()
        {
            AssertValue("0", 0, 0.0, false);
            input.InvertSign();
            AssertValue("-0", 0, 0.0, false);
            input.InvertSign();
            AssertValue("0", 0, 0.0, false);
        }

        [Test]
        public void TestInvertSignInt()
        {
            input.InvertSign();
            AssertValue("-0", 0, 0.0, false);
            input.AppendDigit(7);
            AssertValue("-7", -7, 0.0, false);
            input.AppendDigit(3);
            AssertValue("-73", -73, 0.0, false);
            input.InvertSign();
            AssertValue("73", 73, 0.0, false);
        }

        [Test]
        public void TestClearNegativeInt()
        {
            // Get some integer input in place
            TestIntInput();
            AssertValue("12345", 12345, 0.0, false);
            input.InvertSign();
            AssertValue("-12345", -12345, 0.0, false);
            // Clear it
            input.Clear();
            AssertValue("0", 0, 0.0, false);

            // Make sure that it "stays" positive
            input.AppendDigit(9);
            AssertValue("9", 9, 0.0, false);
        }

        [Test]
        public void TestClearNegativeDecimal()
        {
            // Get some decimal in place
            TestDecimal();
            AssertValue("5.555", 0, 5.555, true);
            input.InvertSign();
            AssertValue("-5.555", 0, -5.555, true);

            // Clear it
            input.Clear();
            AssertValue("0", 0, 0.0, false);

            // Make sure that it "stays" positive
            input.AppendDigit(8);
            AssertValue("8", 8, 0.0, false);
        }

        [Test]
        public void TestFirstInputDecimalPoint()
        {
            // Number can start with just the decimal point
            input.AppendDecimalPoint();
            AssertValue("0.", 0, 0.0, true);
            input.AppendDigit(4);
            AssertValue("0.4", 0, 0.4, true);

            // Can delete everything properly
            input.DeleteDigit();
            AssertValue("0.", 0, 0.0, true);
            input.DeleteDigit();
            AssertValue("0", 0, 0.0, false);
        }

        private void AssertValue(string txt, int intVal, double dblVal, bool isDecimal)
        {
            Assert.AreEqual(txt, input.ValueString());
            Assert.AreEqual(intVal, input.ValueInt());
            Assert.AreEqual(dblVal, input.ValueDouble());
            Assert.AreEqual(isDecimal, input.IsDecimal());
        }
    }
}
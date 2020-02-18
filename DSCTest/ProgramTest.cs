using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class ProgramTest
    {
        private Program program;
        private CalcModel model;

        private string lastUpdate;
        private int timesTxtUpdated;

        private int expectedUpdates;

        [SetUp]
        public void Setup()
        {
            model = new CalcModel();
            program = new Program(model);

            lastUpdate = null;
            timesTxtUpdated = 0;
            expectedUpdates = 0;
            model.Display += TextUpdated;
        }

        [Test]
        public void TestSimpleAddition()
        {
            // Input first number
            PerformAndAssertAction(Command.Input1, "1", true);
            PerformAndAssertAction(Command.Input2, "12", true);
            PerformAndAssertAction(Command.Input3, "123", true);

            // Choose the operation
            PerformAndAssertAction(Command.Plus, "123", false);

            // Input the second number
            PerformAndAssertAction(Command.Input3, "3", true);
            PerformAndAssertAction(Command.Input2, "32", true);
            PerformAndAssertAction(Command.Input1, "321", true);

            // Finish the operation
            PerformAndAssertAction(Command.Equals, "444", true);
        }

        [Test]
        public void TestSimpleSubtraction()
        {
            // Input first number
            PerformAndAssertAction(Command.Input4, "4", true);
            PerformAndAssertAction(Command.Decimal, "4.", true);
            PerformAndAssertAction(Command.Input9, "4.9", true);

            // Choose the operation
            PerformAndAssertAction(Command.Minus, "4.9", false);

            // Input the second number
            PerformAndAssertAction(Command.Input1, "1", true);
            PerformAndAssertAction(Command.Input0, "10", true);

            // Finish the operation
            PerformAndAssertAction(Command.Equals, "-5.1", true);
        }

        [Test]
        public void TestSimpleMultiplication()
        {
            // Input first number
            PerformAndAssertAction(Command.Input7, "7", true);
            PerformAndAssertAction(Command.Input0, "70", true);
            PerformAndAssertAction(Command.Decimal, "70.", true);
            PerformAndAssertAction(Command.Input4, "70.4", true);
            PerformAndAssertAction(Command.Input8, "70.48", true);
            PerformAndAssertAction(Command.Delete, "70.4", true);

            // Choose the operation
            PerformAndAssertAction(Command.Multiply, "70.4", false);

            // Input second number
            PerformAndAssertAction(Command.Input2, "2", true);
            PerformAndAssertAction(Command.InvertSign, "-2", true);

            // Finish the operation
            PerformAndAssertAction(Command.Equals, "-140.8", true);
        }

        [Test]
        public void TestSimpleDivision()
        {
            // Input first number
            PerformAndAssertAction(Command.Input8, "8", true);
            PerformAndAssertAction(Command.Input7, "87", true);

            // Choose the operation
            PerformAndAssertAction(Command.Divide, "87", false);

            // Input second number
            PerformAndAssertAction(Command.InvertSign, "-0", true);
            PerformAndAssertAction(Command.Input3, "-3", true);

            // Finish the operation
            PerformAndAssertAction(Command.Equals, "-29", true);
        }

        [Test]
        public void TestSimplePower()
        {
            // Input the first number
            PerformAndAssertAction(Command.Input2, "2", true);
            PerformAndAssertAction(Command.Input5, "25", true);

            // Choose the operation
            PerformAndAssertAction(Command.Power, "25", false);

            // Input the second number
            PerformAndAssertAction(Command.Decimal, "0.", true);
            PerformAndAssertAction(Command.Input5, "0.5", true);

            // Finish the operation
            PerformAndAssertAction(Command.Equals, "5", true);
        }

        [Test]
        public void TestStackingOperations()
        {
            TestSimpleDivision();
            TestSimpleSubtraction();
            TestSimplePower();
            TestSimpleAddition();
            TestSimpleMultiplication();
            TestSimpleAddition();
            TestSimplePower();
            TestSimpleDivision();
            TestSimpleSubtraction();
            TestSimpleMultiplication();
        }

        [Test]
        public void TestIntegerNumberAndEquals()
        {
            PerformAndAssertAction(Command.Input7, "7", true);
            PerformAndAssertAction(Command.Input9, "79", true);
            PerformAndAssertAction(Command.Equals, "79", true);
        }

        [Test]
        public void TestDecimalNumberAndEquals()
        {
            PerformAndAssertAction(Command.Input8, "8", true);
            PerformAndAssertAction(Command.Decimal, "8.", true);
            PerformAndAssertAction(Command.Input4, "8.4", true);
            PerformAndAssertAction(Command.Input4, "8.44", true);
            PerformAndAssertAction(Command.Input4, "8.444", true);
            PerformAndAssertAction(Command.InvertSign, "-8.444", true);
            PerformAndAssertAction(Command.Equals, "-8.444", true);
        }

        [Test]
        public void TestJustEquals()
        {
            // Just hit equals straight away without any numbers input
            PerformAndAssertAction(Command.Equals, "0", true);

            // Enter 0 and hit equals
            PerformAndAssertAction(Command.Input0, "0", true);
            PerformAndAssertAction(Command.Decimal, "0.", true);
            PerformAndAssertAction(Command.Input0, "0.0", true);
            PerformAndAssertAction(Command.Input0, "0.00", true);
            PerformAndAssertAction(Command.Input0, "0.000", true);
            PerformAndAssertAction(Command.Equals, "0", true);

            // Enter -0 and hit equals
            PerformAndAssertAction(Command.InvertSign, "-0", true);
            PerformAndAssertAction(Command.Equals, "0", true);
        }

        [Test]
        public void TestClear()
        {
            // Clear right away
            PerformAndAssertAction(Command.Clear, "0", true);

            // Input a number first
            PerformAndAssertAction(Command.Input5, "5", true);
            PerformAndAssertAction(Command.InvertSign, "-5", true);
            PerformAndAssertAction(Command.Decimal, "-5.", true);
            PerformAndAssertAction(Command.Input2, "-5.2", true);
            PerformAndAssertAction(Command.Clear, "0", true);

            // Perform most of an operation
            PerformAndAssertAction(Command.Input1, "1", true);
            PerformAndAssertAction(Command.Plus, "1", false);
            PerformAndAssertAction(Command.Input2, "2", true);
            PerformAndAssertAction(Command.Clear, "0", true);
            PerformAndAssertAction(Command.Equals, "0", true);
        }

        [Test]
        public void TestChainedOperations()
        {
            PerformAndAssertAction(Command.Input5, "5", true);
            PerformAndAssertAction(Command.Plus, "5", false);
            PerformAndAssertAction(Command.Input2, "2", true);
            PerformAndAssertAction(Command.Minus, "2", false);
            PerformAndAssertAction(Command.Input3, "3", true);
            PerformAndAssertAction(Command.Multiply, "3", false);
            PerformAndAssertAction(Command.Input4, "4", true);
            PerformAndAssertAction(Command.Equals, "16", true);
        }

        [Test]
        public void TestDivideByZero()
        {
            PerformAndAssertAction(Command.Input5, "5", true);
            PerformAndAssertAction(Command.Decimal, "5.", true);
            PerformAndAssertAction(Command.Input3, "5.3", true);
            PerformAndAssertAction(Command.Divide, "5.3", false);
            PerformAndAssertAction(Command.Input0, "0", true);
            PerformAndAssertAction(Command.Equals, "NaN", true);

        }

        [Test]
        public void TestOperationSelectedBackToBack()
        {
            PerformAndAssertAction(Command.Input6, "6", true);
            PerformAndAssertAction(Command.Multiply, "6", false);
            PerformAndAssertAction(Command.Minus, "6", false);
            PerformAndAssertAction(Command.Input3, "3", true);
            PerformAndAssertAction(Command.Equals, "33", true);
        }

        /// <summary>
        /// The event handler to use for testing to receive updates for when UI
        /// updates are being triggered/performed.
        /// </summary>
        /// <param name="m">Model which is triggering the update</param>
        /// <param name="txt">string which is to be displayed</param>
        private void TextUpdated(CalcModel m, string txt)
        {
            Assert.AreEqual(model, m);
            timesTxtUpdated++;
            lastUpdate = txt;
        }

        /// <summary>
        /// Triggers the specified command and verifies that the expected result
        /// is received from the logic.
        /// </summary>
        /// <param name="toPerform">Command which is to be triggered</param>
        /// <param name="expected">string which is expected to be seen on the calculator display</param>
        /// <param name="updateExpected"><c>true</c> the command is expected to trigger a UI update</param>
        private void PerformAndAssertAction(Command toPerform, string expected, bool updateExpected)
        {
            model.TriggerInput(toPerform);
            if (updateExpected)
                expectedUpdates++;

            Assert.AreEqual(expected, lastUpdate);
            Assert.AreEqual(expectedUpdates, timesTxtUpdated);
        }
    }
}

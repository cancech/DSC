using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture()]
    public class CalcModelTest
    {
        private CalcModel model;
        private string lastDisplayUpdate;
        private int timesDisplayUpdated;
        private Command? lastCommand;
        private int timesCommandUpdate;

        [SetUp]
        public void Setup()
        {
            model = new CalcModel();
            lastDisplayUpdate = "";
            timesDisplayUpdated = 0;
            lastCommand = null;
            timesCommandUpdate = 0;
        }

        [Test()]
        public void UpdateDisplayWithoutRegisteredListenerNoCrash()
        {
            try
            {
                model.UpdateDisplay("This is some text");
            } catch (System.Exception)
            {
                Assert.Fail("No exception should be thrown");
            }

            AssertDisplayResults("", 0);
            AssertCommandResults(null, 0);
        }

        [Test()]
        public void UpdateCommandWithoutRegisteredListenerNoCrash()
        {
            try
            {
                model.TriggerInput(Command.Clear);
            }
            catch (System.Exception)
            {
                Assert.Fail("No exception should be thrown");
            }

            AssertDisplayResults("", 0);
            AssertCommandResults(null, 0);
        }

        [Test]
        public void JustDisplayListenerRegistered()
        {
            model.Display += TestDisplayUpdate;
            model.UpdateDisplay("This is some text");
            model.TriggerInput(Command.Clear);

            AssertDisplayResults("This is some text", 1);
            AssertCommandResults(null, 0);
        }

        [Test]
        public void JustCommandListenerRegistered()
        {
            model.InputCommand += TestCommandTriggered;
            model.UpdateDisplay("This is some text");
            model.TriggerInput(Command.Input2);

            AssertDisplayResults("", 0);
            AssertCommandResults(Command.Input2, 1);
        }

        [Test]
        public void BothListenersRegistered()
        {
            model.InputCommand += TestCommandTriggered;
            model.Display += TestDisplayUpdate;

            model.UpdateDisplay("This is some more text");
            AssertDisplayResults("This is some more text", 1);
            AssertCommandResults(null, 0);

            model.TriggerInput(Command.Delete);
            AssertDisplayResults("This is some more text", 1);
            AssertCommandResults(Command.Delete, 1);
        }

        [Test]
        public void ChainOfEvents()
        {
            model.InputCommand += TestCommandTriggered;
            model.Display += TestDisplayUpdate;

            model.TriggerInput(Command.Input4);
            AssertDisplayResults("", 0);
            AssertCommandResults(Command.Input4, 1);

            model.TriggerInput(Command.Clear);
            AssertDisplayResults("", 0);
            AssertCommandResults(Command.Clear, 2);

            model.UpdateDisplay("abc123");
            AssertDisplayResults("abc123", 1);
            AssertCommandResults(Command.Clear, 2);

            model.TriggerInput(Command.Multiply);
            AssertDisplayResults("abc123", 1);
            AssertCommandResults(Command.Multiply, 3);

            model.UpdateDisplay("qwerty");
            AssertDisplayResults("qwerty", 2);
            AssertCommandResults(Command.Multiply, 3);

            model.UpdateDisplay("asdfgh");
            AssertDisplayResults("asdfgh", 3);
            AssertCommandResults(Command.Multiply, 3);
        }

        /*
         * Verify that the results for updating the display match expectations.
         */
        private void AssertDisplayResults(string expectedDisplay, int timesCalled)
        {
            Assert.AreEqual(expectedDisplay, lastDisplayUpdate);
            Assert.AreEqual(timesCalled, timesDisplayUpdated);
        }

        /*
         * Verify that the reuslts of command input matches expectations.
         */
        private void AssertCommandResults(Command? expectedCommand, int timesCalled)
        {
            Assert.AreEqual(expectedCommand, lastCommand);
            Assert.AreEqual(timesCalled, timesCommandUpdate);
        }

        /*
         * The listener to registered for the purpose of testing the display 
         * update propagation.        
         */
        private void TestDisplayUpdate(CalcModel m, string s)
        {
            Assert.AreSame(model, m);
            lastDisplayUpdate = s;
            timesDisplayUpdated++;
        }

        /*
         * Listener to register for the purpose of testing the command propagation
         * mechanism.
         */        
        private void TestCommandTriggered(CalcModel m, Command c)
        {
            Assert.AreSame(model, m);
            lastCommand = c;
            timesCommandUpdate++;
        }
    }
}

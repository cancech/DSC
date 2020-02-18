using NUnit.Framework;
using Moq;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class EqualsCommandProcessorTest
    {
        private EqualsCommandProcessor proc;

        private Mock<CalcModel> model;
        private Mock<Calculator> calc;
        private Mock<InputNumber> number;


        [SetUp]
        public void Setup()
        {
            model = new Mock<CalcModel>();
            calc = new Mock<Calculator>();
            number = new Mock<InputNumber>();

            calc.SetupGet(calc => calc.Number).Returns(number.Object);

            proc = new EqualsCommandProcessor(calc.Object);
            // Clear is called during construction
            number.Verify(n => n.Clear());
            VerifyAllChecked();
        }

        [Test]
        public void TestCommandsToProcess()
        {
            Assert.AreEqual(new Command[] { Command.Equals }, proc.CommandsToProcess());
        }

        [Test]
        public void TestProcessCommand()
        {
            proc.ProcessCommand(model.Object, Command.Equals);
            calc.Verify(c => c.PerformOperation());
            model.Verify(m => m.UpdateDisplay(null));
            calc.Verify(c => c.Number);
            number.Verify(n => n.ValueString());

            VerifyAllChecked();
        }

        /// <summary>
        /// Verify that all mocked interaction has been accounted for.
        /// </summary>
        private void VerifyAllChecked()
        {
            model.VerifyNoOtherCalls();
            calc.VerifyNoOtherCalls();
        }
    }
}

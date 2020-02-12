using NUnit.Framework;
using Moq;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class ClearCommandProcessorTest
    {
        private ClearCommandProcessor proc;

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

            proc = new ClearCommandProcessor(calc.Object);
            // Clear is called during construction
            number.Verify(n => n.Clear());
            VerifyAllChecked();
        }

        [Test]
        public void TestCommandsToProcess()
        {
            Assert.AreEqual(new Command[] { Command.Clear }, proc.CommandsToProcess());
        }

        [Test]
        public void TestProcessCommand()
        {
            proc.ProcessCommand(model.Object, Command.Clear);
            calc.Verify(c => c.Clear());
            model.Verify(m => m.UpdateDisplay(null));
            calc.Verify(c => c.Number);
            number.Verify(n => n.ValueString());

            VerifyAllChecked();
        }

        private void VerifyAllChecked()
        {
            model.VerifyNoOtherCalls();
            calc.VerifyNoOtherCalls();
        }
    }
}

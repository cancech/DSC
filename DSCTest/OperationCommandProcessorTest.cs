using NUnit.Framework;
using Moq;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class OperationCommandProcessorTest
    {
        private OperationCommandProcessor proc;

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

            proc = new OperationCommandProcessor(calc.Object);
            // Clear is called during construction
            number.Verify(n => n.Clear());
            VerifyAllChecked();
        }

        [Test]
        public void TestCommandsToProcess()
        {
            Assert.AreEqual(new Command[] { Command.Plus, Command.Minus, Command.Multiply,
                Command.Divide, Command.Power }, proc.CommandsToProcess());
        }

        [Test]
        public void TestProcessPlusCommand()
        {
            proc.ProcessCommand(model.Object, Command.Plus);
            calc.Verify(c => c.SetOperator(It.IsAny<AddOperator>()));
            VerifyAllChecked();
        }

        [Test]
        public void TestProcessMinusCommand()
        {
            proc.ProcessCommand(model.Object, Command.Minus);
            calc.Verify(c => c.SetOperator(It.IsAny<MinusOperator>()));
            VerifyAllChecked();
        }

        [Test]
        public void TestProcessMultiplyCommand()
        {
            proc.ProcessCommand(model.Object, Command.Multiply);
            calc.Verify(c => c.SetOperator(It.IsAny<MultiplyOperator>()));
            VerifyAllChecked();
        }

        [Test]
        public void TestProcessDivideCommand()
        {
            proc.ProcessCommand(model.Object, Command.Divide);
            calc.Verify(c => c.SetOperator(It.IsAny<DivideOperator>()));
            VerifyAllChecked();
        }

        [Test]
        public void TestProcessPowerCommand()
        {
            proc.ProcessCommand(model.Object, Command.Power);
            calc.Verify(c => c.SetOperator(It.IsAny<PowerOperator>()));
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

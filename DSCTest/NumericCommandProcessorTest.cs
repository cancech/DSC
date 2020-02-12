using NUnit.Framework;
using Moq;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class NumericCommandProcessorTest
    {
        private NumericCommandProcessor processor;
        private Mock<Calculator> calculator;
        private Mock<CalcModel> model;
        private Mock<InputNumber> number;

        [SetUp]
        public void Setup()
        {
            calculator = new Mock<Calculator>();
            model = new Mock<CalcModel>();
            number = new Mock<InputNumber>();

            calculator.SetupGet(calc => calc.Number).Returns(number.Object);

            processor = new NumericCommandProcessor(calculator.Object);
            // Clear is called during construction
            number.Verify(n => n.Clear());
        }

        [Test]
        public void TestCommandsToProcess()
        {
            Assert.AreEqual(new Command[] { Command.Input0, Command.Input1, Command.Input2,
                Command.Input3, Command.Input4, Command.Input5, Command.Input6,
                Command.Input7, Command.Input8, Command.Input9, Command.Delete,
                    Command.Decimal, Command.InvertSign }, processor.CommandsToProcess());
        }

        [TestCase(Command.Input0)]
        [TestCase(Command.Input1)]
        [TestCase(Command.Input2)]
        [TestCase(Command.Input3)]
        [TestCase(Command.Input4)]
        [TestCase(Command.Input5)]
        [TestCase(Command.Input6)]
        [TestCase(Command.Input7)]
        [TestCase(Command.Input8)]
        [TestCase(Command.Input9)]
        public void TestNumericCommand(Command c)
        {
            number.Setup(n => n.ValueString()).Returns(c.ToString);

            processor.ProcessCommand(model.Object, c);
            calculator.Verify(calc => calc.Number, Times.Exactly(2));
            number.Verify(n => n.AppendDigit((int)c));
            model.Verify(m => m.UpdateDisplay(c.ToString()));
            number.Verify(n => n.ValueString());
            VerifyAllChecked();
        }

        [Test]
        public void TestDeleteCommand()
        {
            number.Setup(n => n.ValueString()).Returns("Delete Command!");

            processor.ProcessCommand(model.Object, Command.Delete);
            calculator.Verify(calc => calc.Number, Times.Exactly(2));
            number.Verify(n => n.DeleteDigit());
            model.Verify(m => m.UpdateDisplay("Delete Command!"));
            number.Verify(n => n.ValueString());
            VerifyAllChecked();
        }

        [Test]
        public void TestDecimalCommand()
        {
            number.Setup(n => n.ValueString()).Returns("Decimal Command!");

            processor.ProcessCommand(model.Object, Command.Decimal);
            calculator.Verify(calc => calc.Number, Times.Exactly(2));
            number.Verify(n => n.AppendDecimalPoint());
            model.Verify(m => m.UpdateDisplay("Decimal Command!"));
            number.Verify(n => n.ValueString());
            VerifyAllChecked();
        }

        [Test]
        public void TestInvertSignCommand()
        {
            number.Setup(n => n.ValueString()).Returns("Invert Sign Command!");

            processor.ProcessCommand(model.Object, Command.InvertSign);
            calculator.Verify(calc => calc.Number, Times.Exactly(2));
            number.Verify(n => n.InvertSign());
            model.Verify(m => m.UpdateDisplay("Invert Sign Command!"));
            number.Verify(n => n.ValueString());
            VerifyAllChecked();
        }

        [TestCase(Command.Clear)]
        [TestCase(Command.Divide)]
        [TestCase(Command.Equals)]
        [TestCase(Command.Minus)]
        [TestCase(Command.Multiply)]
        [TestCase(Command.Plus)]
        [TestCase(Command.Power)]
        public void TestUnsupportedCommands(Command c)
        {
            number.Setup(n => n.ValueString()).Returns(c.ToString);

            processor.ProcessCommand(model.Object, c);
            calculator.Verify(calc => calc.Number, Times.Once());
            model.Verify(m => m.UpdateDisplay(c.ToString()));
            number.Verify(n => n.ValueString());
            VerifyAllChecked();
        }

        private void VerifyAllChecked()
        {
            model.VerifyNoOtherCalls();
            calculator.VerifyNoOtherCalls();
            number.VerifyNoOtherCalls();
        }
    }
}

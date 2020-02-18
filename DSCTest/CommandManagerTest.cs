using NUnit.Framework;
using DSC;
using Moq;
using System;

namespace DSCTest
{
    [TestFixture]
    public class CommandManagerTest
    {
        private CommandManager manager;
        private Mock<CalcModel> model;
        private Mock<ICommandProcessor> processor;

        [SetUp]
        public void Setup()
        {
            model = new Mock<CalcModel>();
            processor = new Mock<ICommandProcessor>();

            manager = new CommandManager(model.Object);
            VerifyAllChecked();
        }

        [TestCase(new Command[] { Command.Clear })]
        [TestCase(new Command[] { Command.Input0, Command.Input2 })]
        [TestCase(new Command[] { Command.Decimal, Command.Input1 })]
        [TestCase(new Command[] { Command.InvertSign, Command.Plus, Command.Minus, Command.Multiply, Command.Divide })]
        public void TestRegisterProcessorSingleCommand(Command[] commands)
        {
            processor.Setup(p => p.CommandsToProcess()).Returns(() => commands);

            // Register the processor
            manager.RegisterProcessor(processor.Object);
            processor.Verify(v => v.CommandsToProcess());
            VerifyAllChecked();

            // Make sure that the processor is only triggered on the appropriate command
            foreach (Command c in Enum.GetValues(typeof(Command)))
            {
                if (Array.IndexOf(commands, c) > -1)
                    continue;

                manager.ProcessInputCommand(model.Object, c);
                VerifyAllChecked();
            }

            foreach (Command c in commands)
            {
                manager.ProcessInputCommand(model.Object, c);
                processor.Verify(v => v.ProcessCommand(model.Object, c));
                VerifyAllChecked();
            }

            VerifyAllChecked();
        }


        private void VerifyAllChecked()
        {
            model.VerifyNoOtherCalls();
            processor.VerifyNoOtherCalls();
        }
    }
}

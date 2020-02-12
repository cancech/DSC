using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class CommandTest
    {

        [Test]
        public void TestNumerics()
        {
            // Make sure that the actual numerics work
            Assert.AreEqual(true, Command.Input0.IsNumeric());
            Assert.AreEqual(0, (int)Command.Input0);
            Assert.AreEqual(true, Command.Input1.IsNumeric());
            Assert.AreEqual(1, (int)Command.Input1);
            Assert.AreEqual(true, Command.Input2.IsNumeric());
            Assert.AreEqual(2, (int)Command.Input2);
            Assert.AreEqual(true, Command.Input3.IsNumeric());
            Assert.AreEqual(3, (int)Command.Input3);
            Assert.AreEqual(true, Command.Input4.IsNumeric());
            Assert.AreEqual(4, (int)Command.Input4);
            Assert.AreEqual(true, Command.Input5.IsNumeric());
            Assert.AreEqual(5, (int)Command.Input5);
            Assert.AreEqual(true, Command.Input6.IsNumeric());
            Assert.AreEqual(6, (int)Command.Input6);
            Assert.AreEqual(true, Command.Input7.IsNumeric());
            Assert.AreEqual(7, (int)Command.Input7);
            Assert.AreEqual(true, Command.Input8.IsNumeric());
            Assert.AreEqual(8, (int)Command.Input8);
            Assert.AreEqual(true, Command.Input9.IsNumeric());
            Assert.AreEqual(9, (int)Command.Input9);

            // Make sure that the non-numerics work
            Assert.AreEqual(false, Command.Clear.IsNumeric());
            Assert.AreEqual(false, Command.Delete.IsNumeric());
            Assert.AreEqual(false, Command.Power.IsNumeric());
            Assert.AreEqual(false, Command.Divide.IsNumeric());
            Assert.AreEqual(false, Command.Multiply.IsNumeric());
            Assert.AreEqual(false, Command.Minus.IsNumeric());
            Assert.AreEqual(false, Command.Plus.IsNumeric());
            Assert.AreEqual(false, Command.Equals.IsNumeric());
            Assert.AreEqual(false, Command.Decimal.IsNumeric());
            Assert.AreEqual(false, Command.InvertSign.IsNumeric());
        }
    }
}

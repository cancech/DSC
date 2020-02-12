using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class PowerOperatorTest
    {
        [TestCase(1.0, 2, 1)]
        [TestCase(2222, 1, 2222)]
        [TestCase(1.235, 4.56, 2.618191006)]
        [TestCase(2, 2, 4)]
        [TestCase(10, 3, 1000)]
        [TestCase(9, 0.5, 3)]
        [TestCase(20583, 0, 1)]
        public void TestPower(decimal lhs, decimal rhs, decimal expected)
        {
            Assert.AreEqual(expected, new PowerOperator().PerformOperation(lhs, rhs));
        }

    }
}
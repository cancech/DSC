using NUnit.Framework;
using DSC;

namespace DSCTest
{
    [TestFixture]
    public class DicideOperatorTest
    {
        [TestCase(1.0, 2, 0.5)]
        [TestCase(2222, 100, 22.22)]
        [TestCase(1.235, 4.56, 0.2708333333)]
        [TestCase(0.89, -0.89, -1)]
        [TestCase(9999, 1, 9999)]
        [TestCase(-45.20, 702.903, -0.0643047476)]
        public void TestDivision(decimal lhs, decimal rhs, decimal expected)
        {
            Assert.AreEqual(expected, new DivideOperator().PerformOperation(lhs, rhs));
        }

        [Test]
        public void TestDivisionByZero()
        {
            Assert.AreEqual(0, new DivideOperator().PerformOperation(0M, 0M));
        }
    }
}